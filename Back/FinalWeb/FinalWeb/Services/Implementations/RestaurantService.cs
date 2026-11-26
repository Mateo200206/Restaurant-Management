using FinalWeb.Entities;
using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Repositories.Interfaces;
using FinalWeb.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace FinalWeb.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepository;
        private readonly PasswordHasher<Restaurant> _passwordHasher = new();
        public RestaurantService (IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public RestaurantDto? CreateRestaurant(CreateRestaurantDto dto)
        {
            Restaurant? restaurant = _restaurantRepository.GetByEmail(dto.Email);
            if (restaurant != null) 
            {
                return null;
            }
            Restaurant newRestaurant = new Restaurant()
            {
                Email = dto.Email,
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
            };
            newRestaurant.Password = _passwordHasher.HashPassword(newRestaurant, dto.Password);
            int newId = _restaurantRepository.CreateRestaurant(newRestaurant);

            RestaurantDto restaurantDto = new() 
            {
                Id = newId,
                Name = newRestaurant.Name,
                Description = newRestaurant.Description,
                ImageUrl = newRestaurant.ImageUrl,
            };
            return restaurantDto;
        }

        public List<RestaurantDto> GetAllRestaurants()
        {
            List<Restaurant> restaurants = _restaurantRepository.GetAllRestaurants();
            return restaurants
                .Select(restaurant => new RestaurantDto
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    ImageUrl = restaurant.ImageUrl,
                })
                .ToList();
        }

        public RestaurantDto? GetRestaurantById(int id)
        {
            Restaurant? restaurant = _restaurantRepository.GetRestaurantById(id);
            if (restaurant == null) 
            { 
                return null;
            }
            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                ImageUrl = restaurant.ImageUrl,
            };
        }
        public Restaurant? ValidateRestaurant(AuthenticationRequestDto dto)
        {
            Restaurant? restaurant = _restaurantRepository.GetByEmail(dto.Email);
            if (restaurant is null || _passwordHasher.VerifyHashedPassword(restaurant, restaurant.Password, dto.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return restaurant;
        }

        public bool UpdateRestaurant(UpdateRestaurantDto dto, int restaurantId)
        {
            Restaurant? restaurant = _restaurantRepository.GetRestaurantById(restaurantId);
            if (restaurant is null)
            {
                return false;
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.ImageUrl = dto.ImageUrl;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                if (_passwordHasher.VerifyHashedPassword(restaurant, restaurant.Password, dto.CurrentPassword ?? "") == PasswordVerificationResult.Failed)
                {
                    throw new Exception("La contraseña actual es incorrecta");
                }
                restaurant.Password = _passwordHasher.HashPassword(restaurant, dto.Password);
            }

            _restaurantRepository.UpdateRestaurant(restaurant, restaurantId);
            return true;
        }

        public bool DeleteRestaurant(int restaurantId)
        {
            Restaurant? restaurant = _restaurantRepository.GetRestaurantById(restaurantId);
            if (restaurant is null)
            {
                return false;
            }

            _restaurantRepository.DeleteRestaurant(restaurant);
            return true;
        }
    }
}

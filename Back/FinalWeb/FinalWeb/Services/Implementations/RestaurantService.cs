using FinalWeb.Entities;
using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Repositories.Interfaces;
using FinalWeb.Services.Interfaces;

namespace FinalWeb.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private IRestaurantRepository _restaurantRepository;
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
                Password = dto.Password,
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
            };
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
    }
}

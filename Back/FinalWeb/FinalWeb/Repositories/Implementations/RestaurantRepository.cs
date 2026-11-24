using FinalWeb.Data;
using FinalWeb.Entities;
using FinalWeb.Repositories.Interfaces;

namespace FinalWeb.Repositories.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private RestaurantContext _context;
        public RestaurantRepository(RestaurantContext context)
        {
            _context = context;
        }
        public Restaurant? GetRestaurantById(int restaurantId)
        {
            return _context.Restaurants.SingleOrDefault(r => r.Id == restaurantId);
        }
        public int CreateRestaurant(Restaurant newRestaurant)
        {
            var createdRestaurant = _context.Restaurants.Add(newRestaurant).Entity;
            _context.SaveChanges();
            return createdRestaurant.Id;
        }
        public Restaurant? GetByEmail(string email)
        {
            return _context.Restaurants.SingleOrDefault(r => r.Email == email);
        }
        public List<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }
        public void UpdateRestaurant(Restaurant updatedRestaurant, int restaurantId)
        {
            Restaurant restaurantToUpdate = _context.Restaurants.First(r => r.Id == restaurantId);
            restaurantToUpdate.Name = updatedRestaurant.Name;
            restaurantToUpdate.Description = updatedRestaurant.Description;
            restaurantToUpdate.ImageUrl = updatedRestaurant.ImageUrl;
            restaurantToUpdate.Password = updatedRestaurant.Password;
            _context.SaveChanges();
        }
        public void DeleteRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }
    }
} 

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
    }
} 

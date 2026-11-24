using FinalWeb.Entities;

namespace FinalWeb.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Restaurant? GetRestaurantById (int restaurantId);
        int CreateRestaurant (Restaurant newRestaurant );
        Restaurant? GetByEmail(string email);
        List<Restaurant> GetAllRestaurants();
        void UpdateRestaurant(Restaurant updatedRestaurant, int restaurantId);
        void DeleteRestaurant(Restaurant restaurant);
    }
}

using FinalWeb.Entities;

namespace FinalWeb.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Restaurant? GetRestaurantById (Guid id);
        int CreateRestaurant (Restaurant );
    }
}

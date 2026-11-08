using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Models.DTOs.Requests;

namespace FinalWeb.Services.Interfaces
{
    public interface IRestaurantService
    {
        RestaurantDto? GetRestaurantById(int id);
        RestaurantDto? CreateRestaurant (CreateRestaurantDto dto);
        List<RestaurantDto> GetAllRestaurants();
    }
}

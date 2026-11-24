using FinalWeb.Entities;
using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;

namespace FinalWeb.Services.Interfaces
{
    public interface IRestaurantService
    {
        RestaurantDto? GetRestaurantById(int id);
        RestaurantDto? CreateRestaurant (CreateRestaurantDto dto);
        List<RestaurantDto> GetAllRestaurants();
        Restaurant? ValidateRestaurant(AuthenticationRequestDto dto);
        bool UpdateRestaurant(UpdateRestaurantDto dto, int restaurantId);
        bool DeleteRestaurant(int restaurantId);
    }
}

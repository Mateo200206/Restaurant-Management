using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;

namespace FinalWeb.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetProductsByRestaurant(int restaurantId);
        List<ProductDto> GetProductsByCategory(int categoryId);
        ProductDto? GetProductById(int productId);
        ProductDto? CreateProduct(CreateProductDto dto, int restaurantId);
        bool UpdateProduct(int productId, CreateProductDto dto, int restaurantId);
        bool DeleteProduct(int productId, int restaurantId);
        bool UpdateDiscount(int productId, UpdateDiscountDto dto, int restaurantId);
        bool UpdateHappyHour(int productId, UpdateHappyHourDto dto, int restaurantId);
        string ExportMenu(int restaurantId);
    }
}
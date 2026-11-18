using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;

namespace FinalWeb.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetAllCategories(int restaurantId);
        CategoryDto CreateCategory(CreateCategoryDto categoryDto, int restaurantId);
        bool UpdateCategory(CreateCategoryDto categoryDto,int categoryId,int restaurantId);
        bool DeleteCategory(int categoryId, int restaurantId);
    }
}

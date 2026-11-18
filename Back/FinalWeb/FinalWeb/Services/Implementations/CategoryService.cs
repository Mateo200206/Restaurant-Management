using FinalWeb.Entities;
using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Repositories.Interfaces;
using FinalWeb.Services.Interfaces;

namespace FinalWeb.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;                  // 1. el campo
        public CategoryService(ICategoryRepository categoryRepository)    // 2. el constructor
        {
            _categoryRepository = categoryRepository;                     // 3. la asignación
        }

        public List<CategoryDto> GetAllCategories(int restaurantId)
        {
            return _categoryRepository.GetAllCategories(restaurantId).Select(c => new CategoryDto { Id = c.Id, Name = c.Name }).ToList();
        }

        public CategoryDto CreateCategory(CreateCategoryDto categoryDto, int restaurantId)
        {
            Category newCategory = new Category { Name = categoryDto.Name, RestaurantId = restaurantId };
            int newId = _categoryRepository.CreateCategory(newCategory);
            return new CategoryDto { Id = newId, Name = newCategory.Name };
        }

        public bool UpdateCategory(CreateCategoryDto categoryDto, int categoryId, int restaurantId)
        {
            Category? category = _categoryRepository.GetCategoryById(categoryId);
            if (category is null || category.RestaurantId != restaurantId) return false;
            Category updated = new Category { Name = categoryDto.Name };
            _categoryRepository.UpdateCategory(updated, categoryId);
            return true;
        }
        public bool DeleteCategory(int categoryId, int restaurantId)
        {
            Category? category = _categoryRepository.GetCategoryById(categoryId);
            if (category is null || category.RestaurantId != restaurantId) return false;
            _categoryRepository.DeleteCategory(category);
            return true;
        }
    }
}

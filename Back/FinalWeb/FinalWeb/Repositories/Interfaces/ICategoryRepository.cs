using FinalWeb.Entities;

namespace FinalWeb.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories(int restaurantId);
        Category? GetCategoryById(int id);
        int CreateCategory(Category newCategory);
        void UpdateCategory(Category updatedCategory, int categoryId);
        void DeleteCategory(Category category);
    }
}

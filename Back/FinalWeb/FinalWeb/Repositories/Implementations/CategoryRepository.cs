using FinalWeb.Data;
using FinalWeb.Entities;
using FinalWeb.Repositories.Interfaces;

namespace FinalWeb.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private RestaurantContext _context;
        public CategoryRepository(RestaurantContext context)
        {
            _context = context;
        }
        public List<Category> GetAllCategories(int restaurantId)
        {
            return _context.Categories.Where(c => c.RestaurantId == restaurantId).ToList();
        }
        public int CreateCategory(Category newCategory)
        {
            var createdCategory = _context.Categories.Add(newCategory).Entity;
            _context.SaveChanges();
            return createdCategory.Id;
        }
        public Category? GetCategoryById(int id)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == id);
        }
        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        public void UpdateCategory(Category updatedCategory, int categoryId)
        {

            Category categoryToUpdate = _context.Categories.First(c => c.Id == categoryId);   // 1. traigo el que está en la base
            categoryToUpdate.Name = updatedCategory.Name;                  // 2. le copio los datos nuevos
            _context.SaveChanges();                                          // 3. EF detecta los cambios y guarda
        }

    }
}

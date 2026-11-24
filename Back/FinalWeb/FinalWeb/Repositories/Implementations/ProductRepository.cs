using FinalWeb.Data;
using FinalWeb.Entities;
using FinalWeb.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalWeb.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private RestaurantContext _context;
        public ProductRepository(RestaurantContext context)
        {
            _context = context;
        }
        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryId == categoryId).ToList();
        }
        public List<Product> GetAllProductsByRestaurant(int restaurantId)
        {
            return _context.Products.Include(p => p.Category).Where(p => p.Category.RestaurantId == restaurantId).ToList();
        }
        public int CreateProduct(Product newProduct)
        {
            var createdProduct = _context.Products.Add(newProduct).Entity;
            _context.SaveChanges();
            return createdProduct.Id;
        }
        public Product? GetProductById(int id)
        {
            return _context.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == id);
        }
        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product updatedProduct, int productId)
        {

            Product productToUpdate = _context.Products.First(p => p.Id == productId);   // 1. traigo el que está en la base
            productToUpdate.Name = updatedProduct.Name;                  // 2. le copio los datos nuevos
            productToUpdate.Description = updatedProduct.Description;
            productToUpdate.Price = updatedProduct.Price;
            productToUpdate.ImageUrl = updatedProduct.ImageUrl;
            productToUpdate.IsFeatured = updatedProduct.IsFeatured;
            productToUpdate.CategoryId = updatedProduct.CategoryId;
            _context.SaveChanges();                                          // 3. EF detecta los cambios y guarda
        }
    }
}

using FinalWeb.Entities;

namespace FinalWeb.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetProductsByCategory(int categoryId);
        List<Product> GetAllProductsByRestaurant(int restaurantId);
        Product? GetProductById(int id);
        int CreateProduct(Product newProduct);
        void UpdateProduct(Product updatedProduct, int productId);
        void DeleteProduct(Product product);
    }
}

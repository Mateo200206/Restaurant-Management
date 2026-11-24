using FinalWeb.Entities;
using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Repositories.Interfaces;
using FinalWeb.Services.Interfaces;
using System.Globalization;

namespace FinalWeb.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public List<ProductDto> GetProductsByRestaurant(int restaurantId)
        {
            return _productRepository.GetAllProductsByRestaurant(restaurantId).Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FinalPrice = product.Price - product.Price * product.DiscountPercentage / 100,
                ImageUrl = product.ImageUrl,
                IsFeatured = product.IsFeatured,
                DiscountPercentage = product.DiscountPercentage,
                HasHappyHour = product.HasHappyHour,
                CategoryId = product.CategoryId
            }).ToList();
        }

        public List<ProductDto> GetProductsByCategory(int categoryId)
        {
            return _productRepository.GetProductsByCategory(categoryId).Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FinalPrice = product.Price - product.Price * product.DiscountPercentage / 100,
                ImageUrl = product.ImageUrl,
                IsFeatured = product.IsFeatured,
                DiscountPercentage = product.DiscountPercentage,
                HasHappyHour = product.HasHappyHour,
                CategoryId = product.CategoryId
            }).ToList();
        }

        public ProductDto? GetProductById(int productId)
        {
            Product? product = _productRepository.GetProductById(productId);
            if (product is null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FinalPrice = product.Price - product.Price * product.DiscountPercentage / 100,
                ImageUrl = product.ImageUrl,
                IsFeatured = product.IsFeatured,
                DiscountPercentage = product.DiscountPercentage,
                HasHappyHour = product.HasHappyHour,
                CategoryId = product.CategoryId
            };
        }

        public ProductDto? CreateProduct(CreateProductDto dto, int restaurantId)
        {
            // La categoría tiene que existir y ser del restaurante logueado
            Category? category = _categoryRepository.GetCategoryById(dto.CategoryId);
            if (category is null || category.RestaurantId != restaurantId)
            {
                return null;
            }

            Product product = new()
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                IsFeatured = dto.IsFeatured,
                CategoryId = dto.CategoryId,
                DiscountPercentage = 0,
                HasHappyHour = false
            };
            int newProductId = _productRepository.CreateProduct(product);

            return new ProductDto
            {
                Id = newProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                FinalPrice = product.Price,
                ImageUrl = product.ImageUrl,
                IsFeatured = product.IsFeatured,
                DiscountPercentage = product.DiscountPercentage,
                HasHappyHour = product.HasHappyHour,
                CategoryId = product.CategoryId
            };
        }

        public bool UpdateProduct(int productId, CreateProductDto dto, int restaurantId)
        {
            // el producto tiene que ser del restaurante logueado
            Product? product = _productRepository.GetProductById(productId);
            if (product is null || product.Category.RestaurantId != restaurantId)
            {
                return false;
            }

            // y la categoría nueva también (si no, podría moverlo al menú de otro)
            Category? newCategory = _categoryRepository.GetCategoryById(dto.CategoryId);
            if (newCategory is null || newCategory.RestaurantId != restaurantId)
            {
                return false;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.IsFeatured = dto.IsFeatured;
            product.CategoryId = dto.CategoryId;

            _productRepository.UpdateProduct(product, productId);
            return true;
        }

        public bool DeleteProduct(int productId, int restaurantId)
        {
            Product? product = _productRepository.GetProductById(productId);
            if (product is null || product.Category.RestaurantId != restaurantId)
            {
                return false;
            }

            _productRepository.DeleteProduct(product);
            return true;
        }

        public bool UpdateDiscount(int productId, UpdateDiscountDto dto, int restaurantId)
        {
            Product? product = _productRepository.GetProductById(productId);
            if (product is null || product.Category.RestaurantId != restaurantId)
            {
                return false;
            }

            product.DiscountPercentage = dto.DiscountPercentage;
            _productRepository.UpdateProduct(product, productId);
            return true;
        }

        public bool UpdateHappyHour(int productId, UpdateHappyHourDto dto, int restaurantId)
        {
            Product? product = _productRepository.GetProductById(productId);
            if (product is null || product.Category.RestaurantId != restaurantId)
            {
                return false;
            }

            product.HasHappyHour = dto.HasHappyHour;
            _productRepository.UpdateProduct(product, productId);
            return true;
        }
        public string ExportMenu(int restaurantId)
        {
            List<Product> products = _productRepository.GetAllProductsByRestaurant(restaurantId);
            if (products.Count == 0)
            {
                return "";
            }

            string header = "Categoria,Producto,Descripcion,Precio,Descuento,PrecioFinal,Destacado,HappyHour\n";

            string menuData = products.Aggregate(header, (result, product) =>
                result +
                $"{Quote(product.Category.Name)}," +
                $"{Quote(product.Name)}," +
                $"{Quote(product.Description)}," +
                $"{product.Price.ToString(CultureInfo.InvariantCulture)}," +
                $"{product.DiscountPercentage}," +
                $"{(product.Price - product.Price * product.DiscountPercentage / 100).ToString(CultureInfo.InvariantCulture)}," +
                $"{(product.IsFeatured ? "Si" : "No")}," +
                $"{(product.HasHappyHour ? "Si" : "No")}\n"
            );

            return menuData;
        }

        // Pone el texto entre comillas para que una coma dentro del texto no rompa las columnas
        private static string Quote(string? text)
        {
            return "\"" + (text ?? "").Replace("\"", "\"\"") + "\"";
        }
    }
}
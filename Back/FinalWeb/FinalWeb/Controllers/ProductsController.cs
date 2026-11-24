using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FinalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetMyProducts()
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            return Ok(_productService.GetProductsByRestaurant(restaurantId));
        }

        [HttpGet("export")]
        public IActionResult ExportMenu()
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            string csv = _productService.ExportMenu(restaurantId);
            if (string.IsNullOrEmpty(csv))
            {
                return BadRequest("No tenés productos para exportar");
            }

            byte[] bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv)).ToArray();
            return File(bytes, "text/csv", "menu.csv");
        }

        // Menú completo de un restaurante
        [HttpGet("/api/restaurants/{restaurantId}/products")]
        [AllowAnonymous]
        public IActionResult GetProductsByRestaurant(int restaurantId)
        {
            return Ok(_productService.GetProductsByRestaurant(restaurantId));
        }

        // Productos de una categoría
        [HttpGet("/api/categories/{categoryId}/products")]
        [AllowAnonymous]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            return Ok(_productService.GetProductsByCategory(categoryId));
        }

        // Detalle de un producto
        [HttpGet("{productId}")]
        [AllowAnonymous]
        public IActionResult GetProductById(int productId)
        {
            ProductDto? product = _productService.GetProductById(productId);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto dto)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            ProductDto? created = _productService.CreateProduct(dto, restaurantId);
            if (created is null)
            {
                return BadRequest("La categoría no existe o no pertenece a tu restaurante");
            }
            return Created("Created", created);
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProduct(int productId, CreateProductDto dto)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            bool updated = _productService.UpdateProduct(productId, dto, restaurantId);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            bool deleted = _productService.DeleteProduct(productId, restaurantId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{productId}/discount")]
        public IActionResult UpdateDiscount(int productId, UpdateDiscountDto dto)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            bool updated = _productService.UpdateDiscount(productId, dto, restaurantId);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{productId}/happy-hour")]
        public IActionResult UpdateHappyHour(int productId, UpdateHappyHourDto dto)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            bool updated = _productService.UpdateHappyHour(productId, dto, restaurantId);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
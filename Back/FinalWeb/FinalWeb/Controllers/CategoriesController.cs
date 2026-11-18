using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Services.Implementations;
using FinalWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            return Ok(_categoryService.GetAllCategories(restaurantId));
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto dto)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            CategoryDto created = _categoryService.CreateCategory(dto,restaurantId);
            return Created("Created", created);
        }

        [HttpPut("{categoryId}")]                                  
        public IActionResult UpdateCategory(int categoryId,CreateCategoryDto dto)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);                 

            bool updated = _categoryService.UpdateCategory(dto, categoryId, restaurantId);  //  true / false
            if (!updated)
            {
                return NotFound();                             //  no existe o no es suyo → 404
            }
            return NoContent();                                //  salió bien, nada que devolver → 204
        }

        [HttpDelete("{categoryId}")]                               
        public IActionResult DeleteCategory(int categoryId)            
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            bool deleted = _categoryService.DeleteCategory(categoryId, restaurantId);   //  true / false
            if (!deleted)
            {
                return NotFound();                             //  404
            }
            return NoContent();                                //  204
        }
    }
}


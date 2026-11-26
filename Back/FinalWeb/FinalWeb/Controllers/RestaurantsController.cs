using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Models.DTOs.Responses;
using FinalWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult<List<RestaurantDto>> GetAll()
        {
            return Ok(_restaurantService.GetAllRestaurants());
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetRestaurant(int id) 
        {
           RestaurantDto? found = _restaurantService.GetRestaurantById(id);
            if (found == null) 
            {
                return NotFound();
            }
            return Ok(found);
        }

        [HttpPost]
        public ActionResult<RestaurantDto> CreateRestaurant(CreateRestaurantDto dto)
        {
            RestaurantDto? created = _restaurantService.CreateRestaurant(dto);

            if (created is null)
            {
                return Conflict("Ya existe este email");
            }

            return CreatedAtAction(nameof(GetRestaurant), new { id = created.Id }, created);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateMyRestaurant(UpdateRestaurantDto dto)
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            bool updated;
            try
            {
                updated = _restaurantService.UpdateRestaurant(dto, restaurantId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteMyRestaurant()
        {
            int restaurantId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            bool deleted = _restaurantService.DeleteRestaurant(restaurantId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
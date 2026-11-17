using FinalWeb.Models.DTOs.Requests;
using FinalWeb.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalWeb.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRestaurantService _restaurantService;

        public AuthenticationController(IConfiguration config, IRestaurantService restaurantService)
        {
            _config = config;
            _restaurantService = restaurantService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequestDto authenticationRequestBody)
        {
            // Paso 1: validar credenciales
            var restaurant = _restaurantService.ValidateRestaurant(authenticationRequestBody);
            if (restaurant is null)
                return Unauthorized();

            // Paso 2: armar el token
            var securityPassword = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", restaurant.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", restaurant.Name));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(7),
                credentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }
    }
}
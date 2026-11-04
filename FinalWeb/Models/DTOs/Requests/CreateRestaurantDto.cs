using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Requests
{
    public record CreateRestaurantDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
    }
}

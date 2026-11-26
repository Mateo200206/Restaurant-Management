using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Requests
{
    public record UpdateRestaurantDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Password { get; set; }
        public string? CurrentPassword { get; set; }
    }
}
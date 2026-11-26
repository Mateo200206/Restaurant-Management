using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Requests
{
    public record AuthenticationRequestDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Requests
{
    public record AuthenticationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

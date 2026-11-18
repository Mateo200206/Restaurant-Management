using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Requests
{
    public record CreateCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}

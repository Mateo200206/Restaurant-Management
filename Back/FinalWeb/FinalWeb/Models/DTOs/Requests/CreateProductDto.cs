using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Requests
{
    public record CreateProductDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Range(0.01, 100000)]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}

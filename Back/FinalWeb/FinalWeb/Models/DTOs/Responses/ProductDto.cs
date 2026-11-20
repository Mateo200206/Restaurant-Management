using System.ComponentModel.DataAnnotations;

namespace FinalWeb.Models.DTOs.Responses
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public bool HasHappyHour { get; set; }
        public int DiscountPercentage { get; set; } = 0;
    }
}

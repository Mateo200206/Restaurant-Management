namespace FinalWeb.Models.DTOs.Responses
{
    public record RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace FinalWeb.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; } = 0;
        public string? ImageUrl { get; set; }

        public bool IsFeatured { get; set; }          // destacado: sí/no
        public int DiscountPercentage { get; set; }   // descuento: 0 a 100 (0 = sin descuento)
        public bool HasHappyHour { get; set; }        // happy hour: activado sí/no

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

    }
}

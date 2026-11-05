using System.ComponentModel.DataAnnotations.Schema;

namespace FinalWeb.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Price { get; set; } = 0;
        public string? ImageUrl { get; set; }

    }
}

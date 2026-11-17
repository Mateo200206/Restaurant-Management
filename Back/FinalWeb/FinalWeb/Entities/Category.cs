using System.ComponentModel.DataAnnotations.Schema;

namespace FinalWeb.Entities
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
        public List<Product> Products { get; set; } = new();

    }
}

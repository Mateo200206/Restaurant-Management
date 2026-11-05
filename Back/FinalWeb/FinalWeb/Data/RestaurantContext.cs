using Microsoft.EntityFrameworkCore;
using FinalWeb.Entities;

namespace FinalWeb.Data
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }
    }
}

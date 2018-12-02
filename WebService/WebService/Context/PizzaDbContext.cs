using System.Data.Entity;
using WebService.Models;

namespace WebService.Context
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
            : base("name=PizzaDbContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientOfOfferedPizza> IngredientsOfOfferedPizza { get; set; }
        public DbSet<IngredientOfOrderedPizza> IngredientsOfOrderedPizza { get; set; }
        public DbSet<OfferedPizza> OfferedPizzas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedPizza> OrderedPizzas { get; set; }
    }
}
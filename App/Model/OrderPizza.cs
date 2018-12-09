using System.Collections.Generic;

namespace Model
{
    public class OrderPizza
    {
        public List<Ingredient> Ingredients { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }

        public OrderPizza(int Id, double price, List<Ingredient> ingredients)
        {
            this.Id = Id;
            this.Price = price;
            this.Ingredients = ingredients;
        }
    }
}
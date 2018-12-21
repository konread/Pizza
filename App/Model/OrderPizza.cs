using System.Collections.Generic;

namespace Model
{
    public class OrderPizza
    {
        public List<Ingredient> IngredientsOfOrderedPizza { get; set; }
        public int Id_Order { get; set; }
        public double Price { get; set; }
        public string IngredientsStr{ get; set; }

        public OrderPizza(int Id, double price, List<Ingredient> ingredients)
        {
            this.Id_Order = Id;
            this.Price = price;
            this.IngredientsOfOrderedPizza = ingredients;
            this.IngredientsStr = "";

            if (IngredientsOfOrderedPizza != null)
            {
                foreach (Ingredient ingredient in IngredientsOfOrderedPizza)
                {
                    this.IngredientsStr += ingredient + ", ";
                }
            }
        }
    }
}
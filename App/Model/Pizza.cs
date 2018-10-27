using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public void initPrice(List<Ingredient> ingredients)
        {
            foreach(Ingredient ingredient in ingredients)
            {
                this.Price += ingredient.Price;
            }
        }
    }
}

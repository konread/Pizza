using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OfferPizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}

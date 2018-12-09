using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OfferedPizza
    {
        public int Id_Offered_Pizza { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}

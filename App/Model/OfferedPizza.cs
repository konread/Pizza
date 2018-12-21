using System.Collections.Generic;

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

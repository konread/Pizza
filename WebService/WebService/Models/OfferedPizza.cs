using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class OfferedPizza
    {
        [Key]
        public int Id_Offered_Pizza { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<IngredientOfOfferedPizza> IngredientsOfOfferedPizza { get; set; }

        public OfferedPizza() { }
    }
}
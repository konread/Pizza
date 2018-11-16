using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Ingredient
    {
        [Key]
        public int Id_Ingredient { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<IngredientOfOfferedPizza> IngredientsOfOfferedPizza { get; set; }
        public virtual ICollection<IngredientOfOrderedPizza> IngredientsOfOrderedPizza { get; set; }

        public Ingredient() { }
    }
}
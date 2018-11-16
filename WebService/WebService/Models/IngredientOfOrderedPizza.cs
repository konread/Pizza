using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class IngredientOfOrderedPizza
    {
        [Key]
        public int Id_Ingredient_Of_Ordered_Pizza { get; set; }

        public int Id_Ingredient { get; set; }
        public int Id_Ordered_Pizza { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual OrderedPizza OrderedPizza { get; set; }

        public IngredientOfOrderedPizza() { }
    }
}
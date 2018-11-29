using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models
{
    [Table("IngredientsOfOrderedPizza")]
    public class IngredientOfOrderedPizza
    {
        [Key]
        public int Id_Ingredient_Of_Ordered_Pizza { get; set; }

        [ForeignKey("Ingredients")]
        public int Id_Ingredient { get; set; }
        [ForeignKey("OrderedPizzas")]
        public int Id_Ordered_Pizza { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual OrderedPizza OrderedPizza { get; set; }

        public IngredientOfOrderedPizza() { }
    }
}
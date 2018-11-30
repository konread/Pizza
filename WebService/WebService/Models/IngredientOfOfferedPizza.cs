using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models
{
    [Table("IngredientsOfOfferedPizza")]
    public class IngredientOfOfferedPizza
    {
        [Key]
        public int Id_Ingredient_Of_Offered_Pizza { get; set; }

        [ForeignKey("OfferedPizzas")]
        public int Id_Offered_Pizza { get; set; }
        [ForeignKey("Ingredients")]
        public int Id_Ingredient { get; set; }

        public virtual OfferedPizza OfferedPizzas { get; set; }
        public virtual Ingredient Ingredients { get; set; }

        public IngredientOfOfferedPizza() { }
    }
}
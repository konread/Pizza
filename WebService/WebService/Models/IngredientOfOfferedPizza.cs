using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class IngredientOfOfferedPizza
    {
        [Key]
        public int Id_Ingredient_Of_Offered_Pizza { get; set; }

        public int Id_Offered_Pizza { get; set; }
        public int Id_Ingredient { get; set; }

        public virtual OfferedPizza OfferedPizza { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        public IngredientOfOfferedPizza() { }
    }
}
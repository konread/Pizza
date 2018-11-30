using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models
{
    [Table("OfferedPizzas")]
    public class OfferedPizza
    {
        [Key]
        public int Id_Offered_Pizza { get; set; }

        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<IngredientOfOfferedPizza> IngredientsOfOfferedPizza { get; set; }

        public OfferedPizza() { }
    }
}
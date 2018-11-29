using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models
{
    [Table("OrderedPizzas")]
    public class OrderedPizza
    {
        [Key]
        public int Id_Ordered_Pizza { get; set; }

        [ForeignKey("Orders")]
        public int Id_Order { get; set; }
        public double Price { get; set; }

        public virtual ICollection<IngredientOfOrderedPizza> IngredientsOfOrderedPizza { get; set; }
        public virtual Order Order { get; set; }

        public OrderedPizza() { }
    }
}
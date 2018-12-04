using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebService.Models
{
    [DataContract]
    [Table("OrderedPizzas")]
    public class OrderedPizza
    {
        [DataMember]
        [Key]
        public int Id_Ordered_Pizza { get; set; }

        [DataMember]
        [ForeignKey("Orders")]
        public int Id_Order { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        public virtual ICollection<IngredientOfOrderedPizza> IngredientsOfOrderedPizza { get; set; }
        public virtual Order Orders { get; set; }

        public OrderedPizza() { }
    }
}
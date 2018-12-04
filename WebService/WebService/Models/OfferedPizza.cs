using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebService.Models
{
    [DataContract]
    [Table("OfferedPizzas")]
    public class OfferedPizza
    {
        [DataMember]
        [Key]
        public int Id_Offered_Pizza { get; set; }

        [DataMember]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [DataMember]
        public double Price { get; set; }

        public virtual ICollection<IngredientOfOfferedPizza> IngredientsOfOfferedPizza { get; set; }

        public OfferedPizza() { }
    }
}
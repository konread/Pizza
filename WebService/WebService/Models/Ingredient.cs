using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebService.Models
{
    [DataContract]
    [Table("Ingredients")]
    public class Ingredient
    {
        [DataMember]
        [Key]
        public int Id_Ingredient { get; set; }

        [DataMember]
        [StringLength(15)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        public virtual ICollection<IngredientOfOfferedPizza> IngredientsOfOfferedPizza { get; set; }
        public virtual ICollection<IngredientOfOrderedPizza> IngredientsOfOrderedPizza { get; set; }

        public Ingredient() { }
    }
}
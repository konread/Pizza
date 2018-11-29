using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id_Order { get; set; }

        [ForeignKey("Customers")]
        public int Id_Customer { get; set; }
        public double Price { get; set; }
        public int Order_Date { get; set; }
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderedPizza> OrderedPizzaa { get; set; }

        public Order() { }
    }
}
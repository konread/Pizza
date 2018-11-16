using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebService.Models
{
    public class Order
    {
        [Key]
        public int Id_Order { get; set; }

        public int Id_Customer { get; set; }
        public double Price { get; set; }
        public int Order_Date { get; set; }
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderedPizza> OrderedPizzaa { get; set; }

        public Order() { }
    }
}
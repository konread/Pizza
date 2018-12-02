using System;
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

        [DisplayFormat(DataFormatString = "{DD-MM-YYYY}")]
        [DataType(DataType.Date)]
        public DateTime Order_Date { get; set; }
        public string Status { get; set; }

        public virtual Customer Customers { get; set; }
        public virtual ICollection<OrderedPizza> OrderedPizzas { get; set; }

        public Order() { }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebService.Models
{
    [DataContract]
    [Table("Orders")]
    public class Order
    {
        [DataMember]
        [Key]
        public int Id_Order { get; set; }

        [DataMember]
        [ForeignKey("Customers")]
        public int Id_Customer { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        [DisplayFormat(DataFormatString = "{DD-MM-YYYY}")]
        [DataType(DataType.Date)]
        public DateTime Order_Date { get; set; }
        [DataMember]
        public string Status { get; set; }

        public virtual Customer Customers { get; set; }
        public virtual ICollection<OrderedPizza> OrderedPizzas { get; set; }

        public Order() { }
    }
}
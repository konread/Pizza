using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id_Customer { get; set; }

        public string First_Name { get; set; }
        public string Surname { get; set; }
        public string Street_Name { get; set; }
        public int House_Number { get; set; }
        public string City_Name { get; set; }
        public string Postal_code { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer() { }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebService.Models
{
    [DataContract]
    [Table("Customers")]
    public class Customer
    {
        [DataMember]
        [Key]
        public int Id_Customer { get; set; }

        [DataMember]
        public string First_Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Street_Name { get; set; }
        [DataMember]
        public int House_Number { get; set; }
        [DataMember]
        public string City_Name { get; set; }
        [DataMember]
        public string Postal_code { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer() { }
    }
}
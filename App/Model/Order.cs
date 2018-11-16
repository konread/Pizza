using System.Collections.Generic;

namespace Model
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string DateOrder { get; set; }
        public string Status { get; set; }
        public Client Client { get; set; }
        public List<OrderPizza> Pizzas { get; set; }
    }
}
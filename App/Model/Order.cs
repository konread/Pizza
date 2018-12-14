using System.Collections.Generic;

namespace Model
{
    public class Order
    {
        public List<string> OrderStatus { get; set;}

        public int Id_Order { get; set; }
        public double Price { get; set; }
        public string Order_Date { get; set; }
        public string Status { get; set; }
        public int Id_Customer { get; set; }
        public Client Client { get; set; }
        public List<OrderPizza> orderedPizzas { get; set; }

        public Order()
        {
            OrderStatus = new List<string>();

            OrderStatus.Add("NOWE");
            OrderStatus.Add("REALIZOWANE");
            OrderStatus.Add("ZAKONCZONE");
            OrderStatus.Add("ANULOWANE");
        }
    }
}
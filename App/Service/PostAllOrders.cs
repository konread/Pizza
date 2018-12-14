using Model;
using System;
using System.Collections.Generic;

namespace Service
{
    public class PostAllOrders
    {
        public int Id_Customer { get; set; }
        public double Price { get; set; }
        public List<OrderPizza> OrderedPizzas { get; set; }

        public PostAllOrders(int idCustomer, double price, DateTime date, string status, List<OrderPizza> orderedPizzas)
        {
            Id_Customer = idCustomer;
            Price = price;
            OrderedPizzas = orderedPizzas;
        }
    }
}

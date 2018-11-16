using Model;
using System.Collections.Generic;

namespace WebService
{
    public class Data
    {
        public static List<OfferPizza> GetListOffersPizza()
        {
            List<Ingredient> ingredients1 = new List<Ingredient>
            {
                new Ingredient() { Id = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id = 2, Name = "Składnik 2", Price = 1.0 },
                new Ingredient() { Id = 3, Name = "Składnik 3", Price = 1.0 }
            };

            List<Ingredient> ingredients2 = new List<Ingredient>
            {
                new Ingredient() { Id = 4, Name = "Składnik 4", Price = 1.0 },
                new Ingredient() { Id = 5, Name = "Składnik 5", Price = 1.0 },
                new Ingredient() { Id = 6, Name = "Składnik 6", Price = 1.0 }
            };

            List<Ingredient> ingredients3 = new List<Ingredient>
            {
                new Ingredient() { Id = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id = 3, Name = "Składnik 3", Price = 1.0 },
                new Ingredient() { Id = 5, Name = "Składnik 5", Price = 1.0 }
            };

            List<OfferPizza> listOffers = new List<OfferPizza>
            {
                new OfferPizza() { Id = 1, Name = "Pizza 1", Price = 3.00, Ingredients = ingredients1 },
                new OfferPizza() { Id = 2, Name = "Pizza 2", Price = 3.00, Ingredients = ingredients2 },
                new OfferPizza() { Id = 3, Name = "Pizza 3", Price = 3.00, Ingredients = ingredients3 },
                new OfferPizza() { Id = 4, Name = "Pizza 4", Price = 3.00, Ingredients = ingredients1 },
                new OfferPizza() { Id = 5, Name = "Pizza 5", Price = 3.00, Ingredients = ingredients1 },
                new OfferPizza() { Id = 6, Name = "Pizza 6", Price = 3.00, Ingredients = ingredients1 }
            };

            return listOffers;
        }

        public static List<Ingredient> GetListIngredientsAll()
        {
            List<Ingredient> listIngredientsAll = new List<Ingredient>
            {
                new Ingredient() { Id = 1, Name = "Składnik 1", Price = 1.0, Status = false },
                new Ingredient() { Id = 2, Name = "Składnik 2", Price = 1.0, Status = false },
                new Ingredient() { Id = 3, Name = "Składnik 3", Price = 1.0, Status = false },
                new Ingredient() { Id = 4, Name = "Składnik 4", Price = 1.0, Status = false },
                new Ingredient() { Id = 5, Name = "Składnik 5", Price = 1.0, Status = false },
                new Ingredient() { Id = 6, Name = "Składnik 6", Price = 1.0, Status = false }
            };

            return listIngredientsAll;
        }

        public static List<Order> GetListOrder()
        {
            List<Ingredient> ingredients1 = new List<Ingredient>
            {
                new Ingredient() { Id = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id = 2, Name = "Składnik 2", Price = 1.0 },
                new Ingredient() { Id = 3, Name = "Składnik 3", Price = 1.0 }
            };

            List<Ingredient> ingredients2 = new List<Ingredient>
            {
                new Ingredient() { Id = 4, Name = "Składnik 4", Price = 1.0 },
                new Ingredient() { Id = 5, Name = "Składnik 5", Price = 1.0 },
                new Ingredient() { Id = 6, Name = "Składnik 6", Price = 1.0 }
            };

            List<Ingredient> ingredients3 = new List<Ingredient>
            {
                new Ingredient() { Id = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id = 3, Name = "Składnik 3", Price = 1.0 },
                new Ingredient() { Id = 5, Name = "Składnik 5", Price = 1.0 }
            };

            List<OrderPizza> listOrdersPizza1 = new List<OrderPizza>();
            listOrdersPizza1.Add(new OrderPizza(1, 3.00, ingredients1));

            List<OrderPizza> listOrdersPizza2 = new List<OrderPizza>();
            listOrdersPizza2.Add(new OrderPizza(2, 3.00, ingredients2));
            listOrdersPizza2.Add(new OrderPizza(3, 3.00, ingredients3));

            Client client1 = new Client() { Name = "Jan", Surname = "Kowalski", City = "Lodz", Street = "Wolczanska", HouseNumber = "225", FlatNumber = "", Postcode = "00-000", PostOffice = "Lodz" };
            Client client2 = new Client() { Name = "Robert", Surname = "Lewandowski", City = "Lodz", Street = "Wolczanska", HouseNumber = "225", FlatNumber = "", Postcode = "00-000", PostOffice = "Lodz" };

            List<Order> listOrders = new List<Order>
            {
                new Order() { Id = 1, Price = 3.00, DateOrder = "2018-10-11", Status = "Nowe", Client = client1, Pizzas = listOrdersPizza1 },
                new Order() { Id = 2, Price = 3.00, DateOrder = "2018-10-11", Status = "Nowe", Client = client2, Pizzas = listOrdersPizza2 },
            };

            return listOrders;
        }
    }
}
using Model;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WebService
{
    public class Data
    {
        public static List<OfferedPizza> GetListOffersPizza()
        {
            GetAllWithIngredients getAllWithIngredients = null;

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + Service.Properties.Settings.Default.GetAllWithIngredients);
                
                var response = httpClient.GetStringAsync(uri).Result;

                getAllWithIngredients = JsonConvert.DeserializeObject<GetAllWithIngredients>(response);
            }

            return getAllWithIngredients.Pizzas;
        }

        public static List<Ingredient> GetListIngredientsAll()
        {
            GetAll getAll = null;

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + Service.Properties.Settings.Default.GetAll);

                var response = httpClient.GetStringAsync(uri).Result;

                getAll = JsonConvert.DeserializeObject<GetAll>(response);
            }

            return getAll.Ingredients;
        }

        public static List<Order> GetListOrder()
        {
            List<Ingredient> ingredients1 = new List<Ingredient>
            {
                new Ingredient() { Id_Ingredient = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id_Ingredient = 2, Name = "Składnik 2", Price = 1.0 },
                new Ingredient() { Id_Ingredient= 3, Name = "Składnik 3", Price = 1.0 }
            };

            List<Ingredient> ingredients2 = new List<Ingredient>
            {
                new Ingredient() { Id_Ingredient = 4, Name = "Składnik 4", Price = 1.0 },
                new Ingredient() { Id_Ingredient = 5, Name = "Składnik 5", Price = 1.0 },
                new Ingredient() { Id_Ingredient = 6, Name = "Składnik 6", Price = 1.0 }
            };

            List<Ingredient> ingredients3 = new List<Ingredient>
            {
                new Ingredient() { Id_Ingredient = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id_Ingredient = 3, Name = "Składnik 3", Price = 1.0 },
                new Ingredient() { Id_Ingredient = 5, Name = "Składnik 5", Price = 1.0 }
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
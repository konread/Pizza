using Model;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

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
            GetAllIngredients getAllIngredients = null;

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + Service.Properties.Settings.Default.GetAllIngredients);

                var response = httpClient.GetStringAsync(uri).Result;

                getAllIngredients = JsonConvert.DeserializeObject<GetAllIngredients>(response);
            }

            return getAllIngredients.Ingredients;
        }

        public static List<Order> GetListOrder()
        {
            GetAllOrders getAllOrders = null;

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + Service.Properties.Settings.Default.GetAllWithPizzasAndIngredients);

                var response = httpClient.GetStringAsync(uri).Result;

                getAllOrders = JsonConvert.DeserializeObject<GetAllOrders>(response);
            }

            foreach(Order order in getAllOrders.Orders)
            {
                order.Client = GetCustomerById(order.Id_Customer);
            }

            return getAllOrders.Orders;
        }

        public static Client GetCustomerById(int id)
        {
            Client client = null;

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + Service.Properties.Settings.Default.GetCustomerById + id);

                var response = httpClient.GetStringAsync(uri).Result;

                client = JsonConvert.DeserializeObject<Client>(response);
            }

            return client;
        }

        public static void SetOrderStatus(int idOrder, string status)
        {
            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + "api/Order/UpdateStatus?orderId=" + idOrder + "&status=" + status);
                var result = httpClient.PostAsync(uri, null).Result;
            }
        }

        public static void SetListOrdersPizza(int idCustomer, double price, List<OrderPizza> orderPizzas)
        {
            PostAllOrders postAllOrders = new PostAllOrders(idCustomer, price, DateTime.Now, "NOWE", orderPizzas);

            string body = JsonConvert.SerializeObject(postAllOrders);

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + "api/Order/AddWithPizzaAndIngredients");

                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(uri, content).Result;
            }
        }

        public static Client SetCustomer(string first_Name, string surname, string city_Name, string street_Name, int house_Number, string postal_code)
        {
            Client client = null;

            using (var httpClient = new HttpClient())
            {
                Uri uri = new Uri(Service.Properties.Settings.Default.Host + "api/Customer/Add" + "?" + "name=" + first_Name +  "&" + 
                                                                                                        "surname=" + surname + "&" +
                                                                                                        "streetName=" + street_Name + "&" +
                                                                                                        "houseNumber=" + house_Number + "&" +
                                                                                                        "cityName=" + city_Name + "&" +
                                                                                                        "postalCode=" + postal_code );
                var result = httpClient.PostAsync(uri, null).Result;

                string jsonString = result.Content.ReadAsStringAsync().Result;
                client = JsonConvert.DeserializeObject<Client>(jsonString);

                Debug.WriteLine(result);
            }

            return client;
        }
    }
}
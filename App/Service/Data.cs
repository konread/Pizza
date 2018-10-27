using Model;
using System.Collections.Generic;

namespace WebService
{
    public class Data
    {
        public static List<Pizza> GetListPizzas()
        {
            List<Pizza> listPizzas = new List<Pizza>
            {
                new Pizza() { Id = 1, Name = "Pizza 1", Price = 25.00},
                new Pizza() { Id = 2, Name = "Pizza 2", Price = 25.00 },
                new Pizza() { Id = 3, Name = "Pizza 3", Price = 25.00 },
                new Pizza() { Id = 4, Name = "Pizza 4", Price = 25.00 },
                new Pizza() { Id = 5, Name = "Pizza 5", Price = 25.00 },
                new Pizza() { Id = 6, Name = "Pizza 6", Price = 25.00 }
            };

            return listPizzas;
        }

        public static List<Ingredient> GetListIngredients(int id_pizza)
        {
            List<Ingredient> listIngredients = new List<Ingredient>
            {
                new Ingredient() { Id = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id = 2, Name = "Składnik 2", Price = 1.0 },
                new Ingredient() { Id = 3, Name = "Składnik 3", Price = 1.0 },
            };

            return listIngredients;
        }

        public static List<Ingredient> GetListIngredients()
        {
            List<Ingredient> listIngredients = new List<Ingredient>
            {
                new Ingredient() { Id = 1, Name = "Składnik 1", Price = 1.0 },
                new Ingredient() { Id = 2, Name = "Składnik 2", Price = 1.0 },
                new Ingredient() { Id = 3, Name = "Składnik 3", Price = 1.0 },
                new Ingredient() { Id = 4, Name = "Składnik 4", Price = 1.0 },
                new Ingredient() { Id = 5, Name = "Składnik 5", Price = 1.0 },
                new Ingredient() { Id = 6, Name = "Składnik 6", Price = 1.0 }
            };

            return listIngredients;
        }
    }
}
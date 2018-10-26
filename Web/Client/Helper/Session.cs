using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Client.Helper
{
    public static class Session
    {
        private static class SessionConstans
        {
            public static readonly string Pizza = "Pizza";
            public static readonly string ListPizzas = "ListPizzas";
            public static readonly string Ingredient = "Ingredient";
            public static readonly string ListIngredients = "ListIngredients";
        }

        public static void SetPizza(HttpSessionState session, Pizza pizza)
        {
            session[SessionConstans.Pizza] = pizza;
        }

        public static Pizza GetPizza(HttpSessionState session)
        {
            return (Pizza) session[SessionConstans.Pizza];
        }

        public static void SetListPizzas(HttpSessionState session, List<Pizza> listPizzas)
        {
            session[SessionConstans.ListPizzas] = listPizzas;
        }

        public static List<Pizza> GetListPizzas(HttpSessionState session)
        {
            return (List<Pizza>) session[SessionConstans.ListPizzas];
        }

        public static void SetIngredient(HttpSessionState session, Ingredient ingredient)
        {
            session[SessionConstans.Ingredient] = ingredient;
        }

        public static Ingredient GetIngredient(HttpSessionState session)
        {
            return (Ingredient) session[SessionConstans.Ingredient];
        }

        public static void SetListIngredients(HttpSessionState session, List<Ingredient> listIngredients)
        {
            session[SessionConstans.ListIngredients] = listIngredients;
        }

        internal static List<Ingredient> GetIngredients(HttpSessionState session)
        {
            return (List<Ingredient>) session[SessionConstans.ListIngredients];
        }
    }
}
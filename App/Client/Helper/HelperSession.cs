using Model;
using System.Collections.Generic;
using System.Web.SessionState;

namespace Client.Helper
{
    public static class HelperSession
    {
        private static class SessionConstans
        {
            public static readonly string OfferPizza = "OfferPizza";
            public static readonly string ListOffersPizza = "ListOffersPizza";
            public static readonly string ListIngredientsAll = "ListIngredientsAll";
            public static readonly string ListIngredientsOffer = "ListIngredientsOffer";
            public static readonly string ListOrdersPizza = "ListOrdersPizza";
            public static readonly string SumOrderedPizzas = "SumOrderedPizzas";
            public static readonly string TotalPriceOrderedPizzas = "TotalPriceOrderedPizzas";
            public static readonly string ListIngredientsSelected = "ListIngredientsSelected";
        }

        public static void SetOfferPizza(HttpSessionState session, OfferedPizza offer)
        {
            session[SessionConstans.OfferPizza] = offer;
        }

        public static OfferedPizza GetOfferPizza(HttpSessionState session)
        {
            return (OfferedPizza) session[SessionConstans.OfferPizza];
        }

        public static void SetListOffersPizza(HttpSessionState session, List<OfferedPizza> listOffersPizza)
        {
            session[SessionConstans.ListOffersPizza] = listOffersPizza;
        }

        public static List<OfferedPizza> GetListOffersPizza(HttpSessionState session)
        {
            return (List<OfferedPizza>) session[SessionConstans.ListOffersPizza];
        }

        public static void SetListIngredientsAll(HttpSessionState session, List<Ingredient> listIngredientsAll)
        {
            session[SessionConstans.ListIngredientsAll] = listIngredientsAll;
        }

        internal static List<Ingredient> GetListIngredientsAll(HttpSessionState session)
        {
            return (List<Ingredient>) session[SessionConstans.ListIngredientsAll];
        }

        public static void SetListIngredientsOffer(HttpSessionState session, List<Ingredient> listIngredientsOffer)
        {
            session[SessionConstans.ListIngredientsOffer] = listIngredientsOffer;
        }

        internal static List<Ingredient> GetListIngredientsOffer(HttpSessionState session)
        {
            return (List<Ingredient>)session[SessionConstans.ListIngredientsOffer];
        }

        public static void SetListIngredientsSelected(HttpSessionState session, List<Ingredient> listIngredientsSelected)
        {
            session[SessionConstans.ListIngredientsSelected] = listIngredientsSelected;
        }

        internal static List<Ingredient> GetListIngredientsSelected(HttpSessionState session)
        {
            return (List<Ingredient>)session[SessionConstans.ListIngredientsSelected];
        }

        public static void SetListOrdersPizza(HttpSessionState session, List<OrderPizza> listOrdersPizza)
        {
            session[SessionConstans.ListOrdersPizza] = listOrdersPizza;
        }

        internal static List<OrderPizza> GetListOrdersPizza(HttpSessionState session)
        {
            return (List<OrderPizza>)session[SessionConstans.ListOrdersPizza];
        }

        internal static void RemoveListOrdersPizza(HttpSessionState session)
        {
            session.Remove(SessionConstans.ListOrdersPizza);
        }

        public static void SetSumOrderedPizzas(HttpSessionState session, double price)
        {
            session[SessionConstans.SumOrderedPizzas] = price;
        }

        internal static double GetSumOrderedPizzas(HttpSessionState session)
        {
            return session[SessionConstans.SumOrderedPizzas] != null ? (double)session[SessionConstans.SumOrderedPizzas] : 0.0;
        }

        public static void SetTotalPriceOrderedPizzas(HttpSessionState session, double price)
        {
            session[SessionConstans.TotalPriceOrderedPizzas] = price;
        }

        internal static double GetTotalPriceOrderedPizzas(HttpSessionState session)
        {
            return session[SessionConstans.TotalPriceOrderedPizzas] != null ? (double)session[SessionConstans.TotalPriceOrderedPizzas] : 0.0;
        }
    }
}
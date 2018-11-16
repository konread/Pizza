using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        }

        public static void SetOfferPizza(HttpSessionState session, OfferPizza offer)
        {
            session[SessionConstans.OfferPizza] = offer;
        }

        public static OfferPizza GetOfferPizza(HttpSessionState session)
        {
            return (OfferPizza) session[SessionConstans.OfferPizza];
        }

        public static void SetListOffersPizza(HttpSessionState session, List<OfferPizza> listOffersPizza)
        {
            session[SessionConstans.ListOffersPizza] = listOffersPizza;
        }

        public static List<OfferPizza> GetListOffersPizza(HttpSessionState session)
        {
            return (List<OfferPizza>) session[SessionConstans.ListOffersPizza];
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
    }
}
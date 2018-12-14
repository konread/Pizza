using Client.Helper;
using Model;
using Model.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class PizzaDetails : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    OfferedPizza offerPizza = HelperSession.GetOfferPizza(Session);

                    List<Ingredient> listIngredientsAll = HelperSession.GetListIngredientsAll(Session);
                    List<Ingredient> listIngredientsOffer = HelperIngredient.Connect(offerPizza.Ingredients, listIngredientsAll);

                    HelperSession.SetListIngredientsOffer(Session, listIngredientsOffer);

                    GvListIngredients.DataSource = listIngredientsOffer;
                    GvListIngredients.DataBind();

                    LbTitle.Text = offerPizza.Name;
                    LbPrice.Text = offerPizza.Price.ToString("0.00") + "";

                    HelperSession.SetListIngredientsSelected(Session, offerPizza.Ingredients);
                }
            }
        }

        protected void CbStatus_CheckedChanged(object sender, EventArgs e)
        {
            OfferedPizza offerPizza = HelperSession.GetOfferPizza(Session);

            List<Ingredient> listIngredientsOffer = HelperSession.GetListIngredientsOffer(Session);
            List<Ingredient> listIngredientsSelected = HelperSession.GetListIngredientsSelected(Session);

            CheckBox cbStatus = (CheckBox) sender;
            GridViewRow row = (GridViewRow) cbStatus.NamingContainer;

            if (row != null)
            {
                int index = row.RowIndex;
                Ingredient ingredient = listIngredientsOffer[index];

                ingredient.Status = !(ingredient.Status);

                if (ingredient.Status)
                {
                    offerPizza.Price += ingredient.Price;

                    listIngredientsSelected.Add(ingredient);
                }
                else
                {
                    offerPizza.Price -= ingredient.Price;

                    listIngredientsSelected.Remove(ingredient);
                }

                listIngredientsOffer[index] = ingredient;

                LbPrice.Text = offerPizza.Price.ToString("0.00") + "";

                HelperSession.SetListIngredientsOffer(Session, listIngredientsOffer);
                HelperSession.SetListIngredientsSelected(Session, listIngredientsSelected);
                HelperSession.SetOfferPizza(Session, offerPizza);
            }
        }

        protected void BtnOrder_Click(object sender, EventArgs e)
        {
            OfferedPizza offerPizza = HelperSession.GetOfferPizza(Session);
            List<Ingredient> listIngredientsSelected = HelperSession.GetListIngredientsSelected(Session);

            OrderPizza orderPizza = new OrderPizza(offerPizza.Id_Offered_Pizza, offerPizza.Price, listIngredientsSelected);

            List<OrderPizza> listOrdersPizza = HelperSession.GetListOrdersPizza(Session);

            listOrdersPizza.Add(orderPizza);

            HelperSession.SetListOrdersPizza(Session, listOrdersPizza);

            double partialSum = HelperSession.GetSumOrderedPizzas(Session);

            partialSum += orderPizza.Price;

            HelperSession.SetSumOrderedPizzas(Session, partialSum);

            double totalSum = Properties.Settings.Default.PriceDeliveryAndService + partialSum;

            HelperSession.SetTotalPriceOrderedPizzas(Session, totalSum);

            Response.Redirect("Basket.aspx");
        }
    }
}
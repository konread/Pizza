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
                    LbPrice.Text = offerPizza.Price + "";

                }
            }
        }

        protected void CbStatus_CheckedChanged(object sender, EventArgs e)
        {
            OfferedPizza offerPizza = HelperSession.GetOfferPizza(Session);

            List<Ingredient> listIngredientsOffer = HelperSession.GetListIngredientsOffer(Session);

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
                }
                else
                {
                    offerPizza.Price -= ingredient.Price;
                }

                listIngredientsOffer[index] = ingredient;

                LbPrice.Text = offerPizza.Price + "";

                HelperSession.SetListIngredientsOffer(Session, listIngredientsOffer);
                HelperSession.SetOfferPizza(Session, offerPizza);
            }
        }

        protected void BtnOrder_Click(object sender, EventArgs e)
        {
            OfferedPizza offerPizza = HelperSession.GetOfferPizza(Session);

            OrderPizza orderPizza = new OrderPizza(1, offerPizza.Price, offerPizza.Ingredients);

            List<OrderPizza> listOrdersPizza = HelperSession.GetListOrdersPizza(Session);

            listOrdersPizza.Add(orderPizza);

            HelperSession.SetListOrdersPizza(Session, listOrdersPizza);

            Response.Redirect("Basket.aspx");
        }
    }
}
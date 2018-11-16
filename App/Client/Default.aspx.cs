using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<OfferPizza> listOffersPizza = WebService.Data.GetListOffersPizza();
                List<Ingredient> listIngredientsAll = WebService.Data.GetListIngredientsAll();

                Helper.HelperSession.SetListOffersPizza(Session, listOffersPizza);
                Helper.HelperSession.SetListIngredientsAll(Session, listIngredientsAll);

                if(Helper.HelperSession.GetListOrdersPizza(Session) == null)
                {
                    List<OrderPizza> listOrders = new List<OrderPizza>();
                    Helper.HelperSession.SetListOrdersPizza(Session, listOrders);
                }

                LvListOffersPizza.DataSource = listOffersPizza;
                LvListOffersPizza.DataBind();
            }
        }

        protected void BtnOfferDetails_Click(object sender, EventArgs e)
        {
            Button btnOfferDetails = (Button) sender;

            int index = Convert.ToInt32(btnOfferDetails.CommandArgument.ToString());

            List<OfferPizza> listOffersPizza = Helper.HelperSession.GetListOffersPizza(Session);
            OfferPizza offer = listOffersPizza[index];

            Helper.HelperSession.SetOfferPizza(Session, offer);

            Response.Redirect("OfferDetails.aspx");
        }

        protected void BtnBasket_Click(object sender, EventArgs e)
        {
            Response.Redirect("Basket.aspx");
        }
    }
}
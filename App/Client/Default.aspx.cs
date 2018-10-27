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
                List<Pizza> listPizzas = WebService.Data.GetListPizzas();

                Helper.Session.SetListPizzas(Session, listPizzas);

                LvListPizzas.DataSource = listPizzas;
                LvListPizzas.DataBind();
            }
        }

        protected void BtnPizzaDetails_Click(object sender, EventArgs e)
        {
            Button btnPizzaDetails = (Button) sender;

            int index = Convert.ToInt32(btnPizzaDetails.CommandArgument.ToString());

            List<Pizza> listPizzas = Helper.Session.GetListPizzas(Session);
            Pizza pizza = listPizzas[index];

            Helper.Session.SetPizza(Session, pizza);

            Response.Redirect("PizzaDetails.aspx");
        }
    }
}
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web.UI;

namespace Client
{
    public partial class Basket : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<OrderPizza> listOrdersPizza = Helper.HelperSession.GetListOrdersPizza(Session);

                LvListOrdersPizza.DataSource = listOrdersPizza;
                LvListOrdersPizza.DataBind();
            }
        }

        protected void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("BtnCancelOrder_Click");
        }

        protected void BtnGoToCheckout_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("BtnGoToCheckout_Click");
        }

        protected void AcceptOrder_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("AcceptOrder");

            Helper.HelperSession.RemoveListOrdersPizza(Session);

            Response.Redirect("Default.aspx");
        }
    }
}
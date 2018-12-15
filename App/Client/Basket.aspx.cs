using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Client
{
    public partial class Basket : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<OrderPizza> listOrdersPizza = Helper.HelperSession.GetListOrdersPizza(Session);

                if (listOrdersPizza.Count == 0)
                {
                    Response.Redirect("EmptyBasket.aspx");
                }
                else
                {
                    LvListOrdersPizza.DataSource = listOrdersPizza;
                    LvListOrdersPizza.DataBind();

                    LbPartialSum.Text = Helper.HelperSession.GetSumOrderedPizzas(Session).ToString("0.00") + "";
                    LbTotal.Text = Helper.HelperSession.GetTotalPriceOrderedPizzas(Session).ToString("0.00") + "";
                    LbDeliveryAndService.Text = Properties.Settings.Default.PriceDeliveryAndService.ToString("0.00") + "";
                }
            }
        }

        protected void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            Button cancelOrder = (Button) sender;

            int index = Convert.ToInt32(cancelOrder.CommandArgument.ToString());

            List<OrderPizza> listOrdersPizza = Helper.HelperSession.GetListOrdersPizza(Session);

            OrderPizza orderPizza = listOrdersPizza[index];

            listOrdersPizza.RemoveAt(index);

            Helper.HelperSession.SetListOrdersPizza(Session, listOrdersPizza);

            double partialSum = Helper.HelperSession.GetSumOrderedPizzas(Session);

            partialSum -= orderPizza.Price;

            Helper.HelperSession.SetSumOrderedPizzas(Session, partialSum);

            double totalSum = Properties.Settings.Default.PriceDeliveryAndService + partialSum;

            Helper.HelperSession.SetTotalPriceOrderedPizzas(Session, totalSum);

            Debug.WriteLine("BtnCancelOrder_Click");

            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void BtnGoToCheckout_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("BtnGoToCheckout_Click");
        }

        protected void AcceptOrder_Click(object sender, EventArgs e)
        {
            string name = this.Name.Text;
            string surname = this.Surname.Text;
            string street = this.Street.Text;
            int houseNumber = int.Parse(this.HouseNumber.Text);
            string postCode = this.PostCode.Text;
            string city = this.City.Text;

            int idCustomer = WebService.Data.SetCustomer(name, surname, city, street, houseNumber, postCode).Id_Customer;

            WebService.Data.SetListOrdersPizza(idCustomer, Helper.HelperSession.GetTotalPriceOrderedPizzas(Session), Helper.HelperSession.GetListOrdersPizza(Session));

            Helper.HelperSession.RemoveListOrdersPizza(Session);
            Helper.HelperSession.SetSumOrderedPizzas(Session, 0);
            Helper.HelperSession.SetTotalPriceOrderedPizzas(Session, 0);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Zamówienie zostało przyjęte!');window.location ='Default.aspx';", true);
        }
    }
}
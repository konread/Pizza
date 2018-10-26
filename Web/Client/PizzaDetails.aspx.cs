using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class PizzaDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    List<Ingredient> listIngredients = Service.Data.GetListIngredients();

                    Helper.Session.SetListIngredients(Session, listIngredients);

                    GvListIngredients.DataSource = listIngredients;
                    GvListIngredients.DataBind();
                }
            }
        }

        protected void BtnOrder_Click(object sender, EventArgs e)
        {

        }
    }
}
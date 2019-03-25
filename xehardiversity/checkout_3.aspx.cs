using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace xehardiversity
{
    public partial class checkout_3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddShipping(object sender, EventArgs e)
        {
            decimal shippingCost = 0.0M;
            if (expedited.Checked == true)
            {
                shippingCost = Convert.ToDecimal(expedited.Value);
            }

            if (flat.Checked == true)
            {
                shippingCost = Convert.ToDecimal(flat.Value);
            }

            if (internationalFlat.Checked == true)
            {
                shippingCost = Convert.ToDecimal(internationalFlat.Value);
            }

            Session["ShippingCost"] = shippingCost;
            decimal newTotalPrice = shippingCost + Convert.ToDecimal(Session["TotalPrice"]);
            Session["TotalPrice"] = newTotalPrice;
            Response.Redirect("checkout_4.aspx");
        }
    }
}
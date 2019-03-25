using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace xehardiversity
{
    public partial class checkout_4 : System.Web.UI.Page
    {
        public string stripePublishableKey = WebConfigurationManager.AppSettings["StripePublishableKey"];
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void tbToken_TextChanged(object sender, EventArgs e)
        {
            Session["TokenId"] = tbToken.Text;
            Session["TokenEmail"] = hdnEmail.Value;
            Response.Redirect("checkout_complete.aspx");
        }
    }
}
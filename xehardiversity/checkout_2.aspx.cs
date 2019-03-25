using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace xehardiversity
{
    public partial class checkout_2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LoadCustomer(object sender, EventArgs e)
        {
            
               // Load the customer values to the Session variable
               var guest = new MiddleEarth.Customer()
            {
                FirstName = first_name.Value,
                LastName = last_name.Value,
                Phone = phone_number.Value,
                Email = email_address.Value,
                Address = apartment_suite.Value + " " + street_address.Value,
                City = city.Value,
                State = state.Value,
                Zip = postcode.Value,
                Company = company.Value,
                Country = country.Value
            };

            if (Session["Guest"] == null)
            {
                Session["Guest"] = guest;
            }
            else
            {
                // If the guest sesstion variable is not empty then we have a guest here already. Do some validation ?
                
            }
            Response.Redirect("checkout_3.aspx");
        }
    }
}
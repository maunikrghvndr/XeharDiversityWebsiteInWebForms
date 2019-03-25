using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace xehardiversity
{
    public class DataOperations
    {
        string gConnectionString;
        public DataOperations()
        {
            gConnectionString = ConfigurationManager.ConnectionStrings["Xehar"].ToString();
        }

        public DataSet ExecuteSQLQuery(string sQuery)
        {
            DataSet ds = new DataSet();
            sQuery = sQuery.Trim();
            if (sQuery.Length > 0)
            {
                using (MySqlConnection conn = new MySqlConnection(gConnectionString))


                using (MySqlDataAdapter myData = new MySqlDataAdapter(sQuery, conn))
                {
                    conn.Open();
                    myData.Fill(ds);
                    myData.Dispose();
                    conn.Close();

                }
            }
            return ds;
        }
    }

    public partial class checkout_complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["TokenId"] != null)
            {
                var customers = new StripeCustomerService();
                var charges = new StripeChargeService();

                var customer = customers.Create(new StripeCustomerCreateOptions
                {
                    Email = Session["TokenEmail"].ToString(),
                    SourceToken = Session["TokenId"].ToString()
                });

                var charge = charges.Create(new StripeChargeCreateOptions
                {
                    Amount = Convert.ToInt32(Convert.ToDecimal(Session["TotalPrice"].ToString().Substring(0)) * 100),
                    Description = "",
                    Currency = "usd",
                    CustomerId = customer.Id
                });

               Console.WriteLine(charge);


                // Okay so here is what we want to accomplish: Insert Customer to Database, Insert Order into Database, Insert OrderDetails into Database, Subtrack child SKU at the specified quantity 

                using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Xehar"].ConnectionString))
                {
                    // Insert new guest customer into database
                    if (Session["Guest"] != null)
                    {
                        MiddleEarth.Customer guest = (MiddleEarth.Customer)Session["Guest"];
                        MiddleEarth.CreateGuestCustomer(con, guest, out var newCID);

                        // Now create a new Order
                        var order = new MiddleEarth.Order()
                        {
                            CID = newCID,
                            OrderDate = DateTime.Now,
                            Address = guest.Address,
                            City = guest.City,
                            State = guest.State,
                            Zip = guest.Zip,
                            Country = guest.Country,
                            ShippingCost = Convert.ToDecimal(Session["ShippingCost"])
                        };
                        // Now Insert new order to database
                        MiddleEarth.CreateOrder(con, order, newCID, out var newOID);

                        // Get list of products in order from cart object
                        var cart = (MiddleEarth.Cart) Session["CartObjects"];
                        // Insert new Order detail
                        MiddleEarth.CreateOrderDetail(con, order, newOID, cart.Products);
                        // Update the quanitity
                        MiddleEarth.UpdateOrderedProductQuantity(con, order, newOID, cart.Products);
                        // send email notification
                        SmtpClient nine = new SmtpClient
                        {
                            Host = "email-smtp.us-east-1.amazonaws.com",
                            DeliveryFormat = SmtpDeliveryFormat.International,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Port = 587,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("AKIAJNGLGDCTEI47BYGQ", "ApVr7lLHurlAZ2+VCNv4rlcBZc3xJNMzxlp1uuMXveLE"),
                            EnableSsl = true
                        };
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("orders@xehardiversity.com");
                        mail.To.Add(new MailAddress(guest.Email));
                        mail.CC.Add(new MailAddress("orders@xehardiversity.com"));
                        mail.CC.Add(new MailAddress("alan@xehar.com"));
                        mail.Subject = "XeharDiversity Receipt";
                        mail.SubjectEncoding = Encoding.UTF8;

                        mail.Body = mail.Body = "<img width='150' src='http://xeharcurvy.com/wp-content/uploads/2016/11/Screen-Shot-2018-02-28-at-5.50.20-PM.png' class='CToWUd'>" +
                        "<br/><br/><br/>Dear " + guest.FirstName + " " + guest.LastName + "," +
                            "<br/><br/><br/> Thank you for your order from XeharDiversity! We wanted to let you know that your order (#" + newOID.ToString() + ") has been made." +
                               "<br/><br/><br/><br/>Shipped To:<br/> " +
                                  guest.FirstName + " " + guest.LastName + "<br/>" +
                                    guest.Address + "<br/>" +
                                guest.City + ", " + guest.State + " " + guest.Zip + " " + guest.Country + "<br/><br/> <b>Thank you for your business and we look forward to serving you in the future!"

                               + "<br/> XeharDiversity" +
                                "<br/>Phone: +1 310 616 3085" +
                                "<br/>Email: orders@xehardiversity.com" +
                                "<br/>Website: www.xehardiversity.com</b>";
                        mail.IsBodyHtml = true;
                        try
                        {
                            nine.Send(mail);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            throw;
                        }
                        //MailMessage mm = new MailMessage();
                        //mm.To.Add(new MailAddress(guest.Email, guest.FirstName));
                        //mm.From = new MailAddress("simrandeep@xehar.com");
                        //mm.Sender = new MailAddress("simrandeep@xehar.com", "Xehar");
                        //mm.Subject = "This is Test Email";
                        //mm.Body = "<h3>This is Testing SMTP Mail Send By Me</h3>";
                        //mm.IsBodyHtml = true;
                        //mm.Priority =MailPriority.High; // Set Priority to sending mail
                        //SmtpClient smtCliend = new SmtpClient();
                        //smtCliend.Host = "email-smtp.us-east-1.amazonaws.com";
                        //smtCliend.Port = 25;    // SMTP port no            
                        //smtCliend.Credentials = new NetworkCredential("AKIAJNGLGDCTEI47BYGQ", "ApVr7lLHurlAZ2+VCNv4rlcBZc3xJNMzxlp1uuMXveLE");
                        //smtCliend.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //try
                        //{
                        //    smtCliend.Send(mm);
                        //}
                        //catch (System.Net.Mail.SmtpException ex)
                        //{
                        //    Label1.Text = ex.ToString();
                        //}
                        //catch (Exception exe)
                        //{
                        //    Label1.Text = "\n\n\n" + exe.ToString();
                        //}



                        lblOrderId.Text = newOID.ToString();

                        // Abort Session
                        Session.Abandon();
                    }
                }
                
            }
        }
    }
}
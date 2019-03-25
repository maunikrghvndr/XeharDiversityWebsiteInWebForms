using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace xehardiversity
{
    public partial class cart : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Session["CartObjects"] = "1"; // Get this is Id from the previous pages where the session starts

                MiddleEarth.Cart existingCart = (MiddleEarth.Cart)Session["CartObjects"];
                //DataOperations xdo = new DataOperations();
                //DataSet ds;
                //if (Session["EmailId"] != null)
                //    ds = xdo.ExecuteSQLQuery("Select * from CartItems where EmailId=" + Session["EmailId"].ToString());
                //else if (Session["CookieId"] != null)
                //    ds = xdo.ExecuteSQLQuery("Select * from CartItems where CookieId=" + Session["CookieId"].ToString());
                //else
                //    ds = xdo.ExecuteSQLQuery("Select * from CartItems where SessionId=" + Session["SessionId"].ToString());
                //DataSet dsP;
                //double dItemTotal = 0;
                //double dCartTotal = 0;
                //hdnItemsCount.Value = ds.Tables[0].Rows.Count.ToString();
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    //query to be filtered with sales channel id also as needed, it is hard coded to 1 for now
                //    dsP = xdo.ExecuteSQLQuery("Select p.ProductName, i.Front, sc.SalesChannelPrice from Products p inner join images pi on p.ChildSKUID=pi.ChildSKUID inner join images i on pi.IMID=i.IMID inner join productsaleschannels sc on p.ChildSKUID=sc.ChildSKUID Where p.ChildSKUID=" + ds.Tables[0].Rows[i]["PId"].ToString() + " and sc.SCID=1");
                //    if (dsP.Tables[0].Rows.Count > 0)
                //    {
                //        hdnCartIds.Value += ds.Tables[0].Rows[i]["Id"].ToString() + ",";
                //        dItemTotal = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[i]["Qty"].ToString()) * Convert.ToDouble(dsP.Tables[0].Rows[0]["SalesChannelPrice"].ToString()), 2);
                //        ltrlCartItems.Text += "<tr> <td class='action'><a href = '#' ><i class='fa fa-times' aria-hidden='true'></i></a></td>" +
                //                            "<td class='cart_product_img'><a href = '#' ><img src='" + dsP.Tables[0].Rows[0]["Front"].ToString() + "' alt='" + dsP.Tables[0].Rows[0]["ProductName"].ToString() + "'/></a></td>" +
                //                            "<td class='cart_product_desc'><h5>" + dsP.Tables[0].Rows[0]["ProductName"].ToString() + "</h5></td>" +
                //                            "<td class='price'><span>$" + dsP.Tables[0].Rows[0]["SalesChannelPrice"].ToString() + "</span></td>" +
                //                            "<td class='qty'><div class='quantity'><span class='qty-minus' onclick='minusQty(" + i + "," + dsP.Tables[0].Rows[0]["SalesChannelPrice"].ToString() + ");'><i class='fa fa-minus' aria-hidden='true'></i></span>" +
                //                                   " <input type = 'number' class='qty-text' id='tb" + i + "' step='1' min='1' max='99' name='quantity' value='" + ds.Tables[0].Rows[i]["Qty"].ToString() + "' onchange='changeTotPrice(" + i + "," + dsP.Tables[0].Rows[0]["SalesChannelPrice"].ToString() + ");' />" +
                //                                   " <span class='qty-plus' onclick='plusQty(" + i + "," + dsP.Tables[0].Rows[0]["SalesChannelPrice"].ToString() + ");' ><i class='fa fa-plus' aria-hidden='true'></i></span></div></td>" +
                //                                   "<td class='total_price'><span id ='spTP" + i + "'>$" + dItemTotal + "</span></td></tr>";
                //        dCartTotal += dItemTotal;
                //    }
                //}
                //spTot.InnerText = "$" + dCartTotal;
                //spTotal.InnerText = "$" + dCartTotal;
                decimal total = 0;
                if (existingCart != null)
                {
                    ET.Value = existingCart.Products.Count.ToString();
                    int actualQ = 0, count = 0;
                    foreach (var prod in existingCart.Products)
                    {
                        // Here is what we need to do here:
                        // I need to first get the updated quantity value from database and store it in the hidden element named quan
                        // We will use ajax to implement this later
                        // For the current product find the max number of items
                        UpdateQuantity(prod, ref actualQ);
                        //quan.Value = actualQ.ToString();
                        ltrlCartItems.Text += "<tr> <td class='action'><a title=" + prod.ID + " href = '#'    id=" + prod.ID + "  onclick='return DeleteFromCart(event);' ><i class='ion-ios-close' id=" + prod.ID + " onclick='return DeleteFromCart(event);'></i> Delete</a></td>" +
                                              "<td class='cart_product_img'><a href = '#' ><img src='img/product-img/" + Regex.Match(prod.Images[0], @"[^//]+$") + "' alt='" + prod.ProductName + "'/></a></td>" +
                                              "<td class='cart_product_desc'><h5>" + prod.ProductName + "</h5></td>" +
                                              "<td class='cart_product_size'><span>" + prod.Size + "</span></td>" +
                                              "<td class='price'><span>$" + prod.Price + "</span></td>" +
                                              "<td class='qty'> <div class='quantity'>" + @"<span class=""qty-minus"" onclick=""Decrementee(" + ET.Value + "," + actualQ + "," + count + @");"">" +
                                              "<i aria-hidden='true'></i>-</span>" + "<input type='number' class='qty-text' id='qty" + count + "' step='1' min='1' max='" + actualQ + "' name='quantity' value='" + prod.Quantity + "'>" + "<span class='qty-plus' onclick='SizeIncrementee(" + ET.Value + "," + actualQ + "," + count + ");'><i class='' aria-hidden='true'></i>+</span>" +
                                              "</div> </td>" +


                                                   "<td class='total_price'><span>" + prod.Quantity * prod.Price + "</span></td></tr>";

                        // dCartTotal += dItemTotal;
                        count++;
                        total += prod.Quantity * prod.Price;
                        //tit.Value += prod.ID + ",";
                    }
                    total = total + (total * 0.0725M);
                    total = Math.Truncate(100 * total) / 100;
                    spTotal.InnerHtml = total.ToString();
                }
                else
                {
                    Response.Redirect("shop.aspx");
                }
            }
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            // Split the conents of hidden element upquan
            var newQuans = upquan.Value.Split(',').ToList();
            var products = (MiddleEarth.Cart)Session["CartObjects"];

            // Change the current cart products
            for (int i = 0; i < newQuans.Count - 1; i++)
            {
                products.Products[i].Quantity = Convert.ToInt32(newQuans[i]);
            }
            Session["CartObjects"] = products;
            // 
            Session["CartIds"] = hdnCartIds.Value;
            List<string> lCartItems = hdnCartIds.Value.Split(',').ToList();
            List<string> lQtys = hdnQtys.Value.Split(',').ToList();
            DataOperations xdo = new DataOperations();

            for (int i = 0; i < lCartItems.Count - 1; i++)
            {
                if (Convert.ToInt32(lQtys[i]) == 0)
                    xdo.ExecuteSQLQuery("Delete from CartItems Where Id=" + lCartItems[i]);
                else
                    xdo.ExecuteSQLQuery("Update CartItems Set Qty='" + lQtys[i] + "' Where Id=" + lCartItems[i]);
            }
            Session["TotalPrice"] = dTotal.Value;
            Response.Redirect("checkout_2.aspx");
        }

        protected void DeleteCartProduct(object sender, EventArgs e)
        {
            var cskuID = Convert.ToInt32(tit.Value);
            MiddleEarth.Cart existingCart = (MiddleEarth.Cart)Session["CartObjects"];

            foreach (var prod in existingCart.Products.ToList())
            {
                if (prod.ID == cskuID)
                {
                    existingCart.Products.Remove(prod);
                }

            }

            Response.Redirect("cart.aspx");
        }

        protected void UpdateCart(object sender, EventArgs e)
        {
            // First we want to get the value of the updated product quantity

        }

        protected void UpdateQuantity(MiddleEarth.Product clickedProduct, ref int actualQuantity)
        {
            // check the database for the quantity 
            string query = ("select quantity from childsku where childskuID='" + clickedProduct.ID + "'");
            using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Xehar"].ConnectionString))
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        actualQuantity = Convert.ToInt32(dr["quantity"]);
                    }

                    dr.Close();
                }

                con.Close();
            }

        }
    }
}
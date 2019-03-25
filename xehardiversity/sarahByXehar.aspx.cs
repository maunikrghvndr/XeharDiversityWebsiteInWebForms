using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

namespace xehardiversity
{
    public partial class sarahByXehar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // CopyImages();
                GetData();
                RenderProducts();
            }
        }

        protected List<MiddleEarth.Product> products;
        protected List<MiddleEarth.ProductImages> productsImages;
        protected List<MiddleEarth.ProductSizeQuantity> productSizeQuantities;
        protected List<int> childSKUs;
        protected List<int> parentSKUs;

        protected void GetData()
        {
            using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Xehar"].ConnectionString))
            {
                products = MiddleEarth.GetSarahProducts(con, out var sts, out var errors);
                var pImages = new List<MiddleEarth.Product>();
                productsImages = MiddleEarth.GetProductsImages(con, products, out var status, out var errs, out pImages);
                for (int i = 0; i < pImages.Count; i++)
                {
                    products[i].Images = pImages[i].Images;
                }
                productSizeQuantities = MiddleEarth.GetProductSizeQuantity(con, out var s, out var msg);
                childSKUs = MiddleEarth.GetChildren(con, out var dbsts, out var emsg);
                parentSKUs = MiddleEarth.GetParents(con, out var dbs, out var eMsg);
            }
        }

        protected void CopyImages()
        {
            // The purpose of this function is to copy the image files for each product into the img folder on this current project. Is this even possible?
            // Theorectically grab the image location on the local harddrive, then copy the file to the img folder
            var applicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
            using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Xehar"].ConnectionString))
            {
                var pImgs = new List<MiddleEarth.Product>();
                var productImages = MiddleEarth.GetProductsImages(con, pImgs, out var sts, out var errors, out pImgs);
                if (sts)
                {
                    foreach (var productImage in productImages)
                    {
                        for (int i = 0; i < productImage.images.Length; i++)
                        {

                            var fullFilePath = "C:/" + productImage.images[i];
                            FileInfo f = new FileInfo(fullFilePath);
                            var fileName = Regex.Match(fullFilePath, @"/[^/]+$");
                            var copyToPath = applicationPath + "img\\product-img" + fileName;
                            var existingFile = copyToPath;
                            // First we want to check if the file already exists
                            if (!File.Exists(existingFile))
                            {
                                // If it doesn't exist then copy to project STILL NEED TO CHECK THIS!!
                                File.Copy(f.FullName, copyToPath);
                            }
                        }
                    }
                }
            }
        }

        protected MiddleEarth.Product FindProduct(string pid)
        {
            var p = new MiddleEarth.Product() { ProductName = "NO Bloody Products!!" };
            GetData();
            foreach (MiddleEarth.Product product in products)
            {
                if (pid == product.ParentID.ToString())
                {
                    return product;
                }
            }

            return p;
        }

        // So here is what we want to do: if I give a product id return to me the sizes available which will be technically strings that have the size
        protected List<string> GetSizes(MiddleEarth.Product pid)
        {
            List<string> productSizes = new List<string>();
            //GetData();
            var matches = new List<MiddleEarth.ProductSizeQuantity>();
            foreach (var psq in productSizeQuantities)
            {
                if (pid.ParentID == psq.PID)
                {
                    matches.Add(psq); // matches should have all the PIDs of a specific number
                }
            }
            // Once i have all the PIDs lets now search through the parent IDs
            foreach (var match in matches)
            {
                // Give me the distinct parent ids in the matches list
                var pSizes = new List<int>();
                var buffer = new List<int>();
                foreach (var parent in parentSKUs)
                {
                    buffer.Add(match.ParentID);
                    if (buffer.Contains(parent))
                    {
                        pSizes.Add(parent);
                    }
                }

                if (pSizes.Count > 1)
                {
                    // We go here when there is a product id with more than one color
                }
                else
                {
                    var s = matches.FindAll(
                        delegate (MiddleEarth.ProductSizeQuantity sku) { return pSizes[0] == sku.ParentID; });
                    foreach (var p in s)
                    {
                        productSizes.Add(p.Size + "," + p.Quantity + "," + p.ChildID);
                    }

                    return productSizes;
                }
            }
            return productSizes;
        }

        protected void RenderProducts()
        {
            // For this function we want to create the html markup elements that the products will sit in. However many products are in the database that is how many times we will render the HTML
            var productMarkup = "";
            int imageCounter = 0, elementCounter = 0;
            foreach (var product in products)
            {
                // Here is where we need to get the product image name from the product images string array
                var img1Name = Regex.Match(productsImages[imageCounter].images[0], @"[^//]+$");
                var img2Name = Regex.Match(productsImages[imageCounter].images[1], @"[^//]+$");
                imageCounter++;
                var pName = product.ProductName;
                var pQuantity = product.Quantity;
                var pPrice = product.Price;
                var pid = product.ParentID;
                var des = product.Description;
                var features_style = product.Features.Style;
                var features_measurments = product.Features.Measurements;
                var features_material = product.Features.Material;
                var origin = product.Origin;

                string html = @"
                

  <!-- >>>>>>>>>>>>>>>> TEST SHOP<<<<<<<<<<<<<<<< -->
<!------- TRUE SHOP --------->
                            <div class=""col-12 col-sm-6 col-lg-4"">
                                <div class=""single_product_area mb-30"">
                                    <div class=""single_arrivals_slide"">
                                        <div class=""product_image""  >
                                            <!-- Product Image -->
                                            <img class=""normal_img""  src=""img/product-img/" + img1Name + @""" alt="""">
                                            <img class=""hover_img""  title=" + pid + @" element=" + elementCounter + @"  onclick=""return Quicky(event);"" src=""img/product-img/" + img2Name + @""" alt="""" data-toggle=""modal"" data-target=""#quickview""> 

                                            <!-- Product Badge 
                                            <div class=""product_badge"">
                                                <span class=""""></span>
                                            </div>
-->
                                            <!-- Wishlist 
                                            <div class=""product_wishlist"">
                                                <a href=""#"" title=""Wishlist""><i class=""""></i></a>
                                            </div>
-->
                                            <!-- Compare 
                                            <div class=""product_compare"">
                                                <a href=""#"" title=""""><i class=""""></i></a>
                                            </div>
-->
                                            <!-- Quick View -->
                                            <div class=""product_quick_view"">
                                                <a title=" + pid + @" href=""#"" element=" + elementCounter + @" onclick=""return Quicky(event);"" data-toggle=""modal"" data-target=""#quickview""><i class=""ion-ios-pricetag"" aria-hidden=""true""></i> Quick View</a>
                                            </div>
                                        </div>
                                        <!-- Product Description -->
                                        <div class=""product_description"">
                                            <p class=""brand_name"">Xehar Diversity</p>
                                           <p1  hidden>" + des + @"</p1> 
                                            <p2  hidden>" + features_style + @"</p2>  
                                            <p4  hidden>" + features_measurments + @"</p4>  
                                            <p5  hidden>" + origin + @"</p5> 
                                            <h5 title=""ProductName""><a href=""#"">" + pName + @"</a></h5>
                                            <h6>$" + pPrice + @"</h6>
                                        </div>
                                    </div>
                                </div>
                            </div>
<!------- TRUE SHOP --------->
                            ";
                elementCounter++;
                productMarkup += html;
                //pros.Value += pid + ",";
            }

            productRow.InnerHtml = productMarkup;
            // Here is where we want to get the product sizes and quantities
            // First get all product ids
            string pds = "", markup = "";

            //SizeForPid();
            //foreach (var product in products)
            //{
            //    pds += product.ID + ",";
            //}
            //SizeQuantity.Text = pds;

            // Next get the sizes for each pid



            foreach (var product in products)
            {
                markup += @"<div id=""p" + product.ParentID + @""">";
                var sizes = GetSizes(product);
                foreach (var size in sizes)
                {
                    var sq = size.Split(',');
                    markup += @"<p quantity=""" + sq[1] + @""" childSKU=""" + sq[2] + @""">" + sq[0] + "</p>";
                }
                markup += "</div>";
            }
            quirks.InnerHtml = markup;

        }

        //protected void SizeForPid()
        //{

        //    string markup = "";
        //    foreach (var product in products)
        //    {
        //        markup += @"<div id=""p" + product.ParentID + @""">";
        //        var sizes = GetSizes(product);
        //        foreach (var size in sizes)
        //        {
        //            var sq = size.Split(',');
        //            markup += @"<p quantity=""" + sq[1] + @""" childSKU=""" + sq[2] + @""">" + sq[0] + "</p>";
        //        }
        //        markup += "</div>";
        //    }
        //    quirks.InnerHtml = markup;

        //}


        protected void AddToCart(object sender, EventArgs e)
        {
            // Create a new cart object. We basically want to initalize the members of the cart object so we can grab all the necessary information now
            string pid = Regex.Match(pros.Value, @"[0-9]+").ToString();
            string qua = quan.Value;
            MiddleEarth.Product clickedProduct = FindProduct(pid);

            clickedProduct.Quantity = Convert.ToInt32(quan.Value);
            clickedProduct.ID = Convert.ToInt32(cSKU.Value);
            clickedProduct.Size = size.Value.ToString();
            int actualQuantity = 0;

            // Add session variable...do we really need session variable if we are putting the data into the cart object? Yes
            // It will have to depend on if we can move the Cart object around the pages serialization and what not, this will work
            // We have to check if the session variable is empty, if so then create the session variable then add the product to the cart

            if (Session["CartObjects"] == null)
            {
                var cart = new MiddleEarth.Cart();
                cart.Products = new List<MiddleEarth.Product>();
                cart.Products.Add(clickedProduct);
                Session["CartObjects"] = cart;
                //UpdateSizeQuantity(clickedProduct.ID, clickedProduct.Quantity);
                //SizeForPid();
            }
            else
            {

                var count = 0;

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

                // If the session variable is not empty then deserialize the object into a List of Products and add a Product to the list



                MiddleEarth.Cart existingCart = (MiddleEarth.Cart)Session["CartObjects"];
                foreach (var product in existingCart.Products)
                {
                    if (product.ID == clickedProduct.ID)
                    {

                        if (actualQuantity >= product.Quantity + clickedProduct.Quantity)
                        {
                            product.Quantity = product.Quantity + clickedProduct.Quantity;
                            count++;
                        }
                        else
                        {
                            product.Quantity = actualQuantity;
                            count++;
                        }
                    }
                }
                if (count == 0)
                {
                    existingCart.Products.Add(clickedProduct);
                    Session["CartObjects"] = existingCart;
                }

                // UpdateSizeQuantity(clickedProduct.ID, clickedProduct.Quantity);
                // SizeForPid();
            }

            // Scoll to products after adding products to cart
            Response.Redirect("sarahByXehar.aspx#Products_productRow");
        }


        protected void UpdateSizeQuantity(int id, int quant)
        {
            //MiddleEarth.ProductSizeQuantity productsize = new MiddleEarth.ProductSizeQuantity()  ;
            foreach (var prodsize in productSizeQuantities)
            {
                if (prodsize.ChildID == id)
                {
                    prodsize.Quantity = prodsize.Quantity - quant;
                }
            }

        }

        protected void AddSizeQuantity(object sender, EventArgs e)
        {

        }
    }
}
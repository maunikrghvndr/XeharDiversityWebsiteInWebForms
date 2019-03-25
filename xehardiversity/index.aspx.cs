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

namespace xehardiversity
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
              //  CopyImages();
               // InsertProducts();
              //  GetImages();
            }
        }

        //protected void InsertProducts()
        //{
        //    using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Xehar"].ConnectionString))
        //    {
        //        var products = MiddleEarth.GetProducts(con, out var sts, out var errors);
        //        if (sts)
        //        {
        //            //
        //        }
        //    }
        //}

        //protected void CopyImages()
        //{
        //    // The purpose of this function is to copy the image files for each product into the img folder on this current project. Is this even possible?
        //    // Theorectically grab the image location on the local harddrive, then copy the file to the img folder
        //    var applicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
        //    using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Xehar"].ConnectionString))
        //    {
        //        var productImages = MiddleEarth.GetProductsImages(con, out var sts, out var errors);
        //        if (sts)
        //        {
        //            foreach (var productImage in productImages)
        //            {
        //                for (int i = 0; i < productImage.images.Length; i++)
        //                {
        //                    var fullFilePath = productImage.images[i];
        //                    var fileName = Regex.Match(fullFilePath, @"\\[^\\]+$");
        //                    var copyToPath = applicationPath + "img\\product-img";
        //                    var existingFile = copyToPath + fileName;
        //                    // First we want to check if the file already exists
        //                    if (!File.Exists(existingFile))
        //                    {
        //                        // If it doesn't exist then copy to project STILL NEED TO CHECK THIS!!
        //                        File.Copy(fullFilePath, copyToPath);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void GetImages()
        //{
        //    using (var con = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Xehar"].ConnectionString))
        //    {
        //        var productImages = MiddleEarth.GetProductsImages(con, out var sts, out var errors);
        //        if (sts)
        //        {
        //            //
        //        }
        //    }
        //}
    }
}
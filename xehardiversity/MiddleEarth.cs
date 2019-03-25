using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace xehardiversity
{
    public class MiddleEarth
    {
        public static bool TestConnection(IDbConnection con, out string message)
        {
            try
            {
                con.Open();
                message = "Connection Successful!";
                con.Close();
            }
            catch (Exception s)
            {
                message = "Couldn't connect to the database: " + s.Message;
                return false;
            }
            return true;
        }

        public static bool TestConnection(IDbConnection con)
        {
            try
            {
                con.Open();
                con.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #region MiddleEarth Dataflow

        public static List<int> GetChildren(MySqlConnection con, out bool status, out string message)
        {
            var children = new List<int>();
            const string query = "select childskuID from childsku";
            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out var errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        children.Add((int) dr["childskuID"]);
                    }
                    dr.Close();
                }
                con.Close();
                return children;
            }
            status = false;
            message = errorMessage;
            return children;
        }

        public static List<int> GetParents(MySqlConnection con, out bool status, out string message)
        {
            var parents = new List<int>();
            const string query = "select parentskuID from parentsku";
            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out var errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        parents.Add((int)dr["parentskuID"]);
                    }
                    dr.Close();
                }
                con.Close();
                return parents;
            }
            status = false;
            message = errorMessage;
            return parents;
        }

        public static List<Product> GetProducts(MySqlConnection con, out bool status, out string message)
        {
            var products = new List<Product>(); // Create a List of Products to hold values from the Database
            const string query = "Select * from xehar_diversity_products"; // the string query we will give the database to execute
            string errorMessage; // This will be the error message if things go wrong with the database

            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new Product
                        {
                            ID = dr["PID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["PID"],
                            ParentID = dr["parentid"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["parentid"],
                            ProductName = dr["ProductName"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ProductName"],
                            ModelNumber = dr["ModelNumber"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ModelNumber"],
                            Quantity = dr["quantity"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["quantity"],
                            Price = dr["price"].GetType().FullName == "System.DBNull" ? 0.0M : (decimal)dr["price"],
                            Description = dr["desciption"].GetType().FullName == "System.DBNull" ? "" : (string)dr["desciption"],
                            Features = JsonConvert.DeserializeObject<ExtraFeatures>(dr["features"].ToString()),
                            Origin = dr["origin"].GetType().FullName == "System.DBNull" ? "" : (string)dr["origin"],
                        });
                    }
                    dr.Close();
                }
                con.Close();
                return products;
            }
            status = false;
            message = errorMessage;
            return products;
        }

        public static List<ProductSizeQuantity> GetProductSizeQuantity(MySqlConnection con, out bool status, out string message)
        {
            var products = new List<ProductSizeQuantity>(); // Create a List of Products to hold values from the Database
            const string query = "select p.PID, ps.parentskuID, c.childskuID, c.name, s.name as Size, c.quantity from products p, size s, parentsku ps, childsku c where p.PID = ps.PID and c.parentskuID = ps.parentskuID and c.sizeID = s.sizeID and c.quantity != 0 order by p.PID"; // the string query we will give the database to execute
            string errorMessage; // This will be the error message if things go wrong with the database

            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new ProductSizeQuantity
                        {
                            PID = dr["parentskuID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["parentskuID"],
                            ParentID = dr["parentskuID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["parentskuID"],
                            ChildID = dr["childskuID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["childskuID"],
                            Size = dr["Size"].GetType().FullName == "System.DBNull" ? "" : (string)dr["Size"],
                            Quantity = dr["quantity"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["quantity"]
                        });
                    }
                    dr.Close();
                }
                con.Close();
                return products;
            }
            status = false;
            message = errorMessage;
            return products;
        }

        public static List<ProductImages> GetProductsImages(MySqlConnection con, List<MiddleEarth.Product> diversity_products, out bool status, out string message, out List<Product> images)
        {
           //var diversity_products = MiddleEarth.GetProducts(con, out var sts, out var errors);
            var products = new List<ProductImages>(); // Create a List of Products to hold values from the Database
            var imgs = new List<Product>();
           // const string query = "Select * from images"; // the string query we will give the database to execute
            string errorMessage; // This will be the error message if things go wrong with the database

            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                foreach (var diversityProduct in diversity_products)
                {
                    string query = "select IMID, parentskuID from images where parentskuID = " + diversityProduct.ParentID;
                    using (var cmd = new MySqlCommand(query, con))
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            products.Add(new ProductImages
                            {
                                //ChildSKUID = dr["ChildSKUID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["ChildSKUID"],
                                ParentID = dr["parentskuID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["parentskuID"],
                                IMID = dr["IMID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["IMID"]
                            });
                        }
                        dr.Close();
                    }
                }
                // We have to now grab the images from the database since there is more than one column
                foreach (var product in products)
                {
                    string q = "select * from images where parentskuID = " + product.ParentID;
                    using (var cmd = new MySqlCommand(q, con))
                    {
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            product.images = new string[] { dr["Front"].GetType().FullName == "System.DBNull" ? "" : (string)dr["Front"], dr["Back"].GetType().FullName == "System.DBNull" ? "" : (string)dr["Back"], dr["Left"].GetType().FullName == "System.DBNull" ? "" : (string)dr["Left"], dr["Right"].GetType().FullName == "System.DBNull" ? "" : (string)dr["Right"] };
                            imgs.Add(new Product() { Images = product.images });
                        }
                        dr.Close();
                    }
                }

                con.Close();
                images = imgs;
                return products;
            }
            status = false;
            message = errorMessage;
            images = new List<Product>();
            return products;
        }

        // Insert into Orders
        public static void CreateOrder(MySqlConnection con, Order o, int cid, out int oid)
        {
            oid = 0;
            if (TestConnection(con, out var errors))
            {
                con.Open();
                var sql = "Insert into orders" +
                          "(CID, SCID, OrderDate, ShipAddress, ShipCity, ShipState, ShipZip, ShipCountry, ShippingCost)" +
                          "Values(@cid, @scid, @orderDate, @shipAddress, @shipCity, @shipState, @shipZip, @shipCountry, @shippingCost)";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@scid", 8);
                    cmd.Parameters.AddWithValue("@orderDate", o.OrderDate.SqlTranslateDateTime());
                    cmd.Parameters.AddWithValue("@shipAddress", o.Address);
                    cmd.Parameters.AddWithValue("@shipCity", o.City);
                    cmd.Parameters.AddWithValue("@shipState", o.State);
                    cmd.Parameters.AddWithValue("@shipZip", o.Zip);
                    cmd.Parameters.AddWithValue("@shipCountry", o.Country);
                    cmd.Parameters.AddWithValue("@shippingCost", o.ShippingCost);
                    cmd.ExecuteNonQuery();
                }

                var last = "select OID from orders order by OID desc limit 1";
                using (var cmd = new MySqlCommand(last, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        oid = (int)dr["OID"];
                    }
                }
                con.Close();
            }
        }
        // Insert into Order Details
        public static void CreateOrderDetail(MySqlConnection con, Order o, int oid, List<Product> products) // This products is to be used for the list of products in the cart object
        {
            if (TestConnection(con, out var errors))
            {
                con.Open();
                foreach (var p in products)
                {
                    var sql = "Insert into ordersDetails" +
                                      "(OID, childSkuID, UnitPrice, Quantity)" +
                                      "Values(@oid, @childSkuID, @unitPrice, @quantity)";
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@oid", oid);
                        cmd.Parameters.AddWithValue("@childSkuID", p.ID); // The child SKU ID is selected once the user selects a size
                        cmd.Parameters.AddWithValue("@unitPrice", p.Price);
                        cmd.Parameters.AddWithValue("@quantity", p.Quantity);
                        cmd.ExecuteNonQuery();
                    } 
                }
                con.Close();
            }
        }

        // Insert into Order Details
        public static void UpdateOrderedProductQuantity(MySqlConnection con, Order o, int oid, List<Product> products) // This products is to be used for the list of products in the cart object
        {
            if (TestConnection(con, out var errors))
            {
                con.Open();
                foreach (var p in products)
                {
                    var sql = "Update childsku set quantity = quantity-@quantity where childSkuID= @childSkuID" ;
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        //cmd.Parameters.AddWithValue("@oid", oid);
                        cmd.Parameters.AddWithValue("@childSkuID", p.ID); // The child SKU ID is selected once the user selects a size
                       // cmd.Parameters.AddWithValue("@unitPrice", p.Price);
                        cmd.Parameters.AddWithValue("@quantity", p.Quantity);
                        cmd.ExecuteNonQuery();
                    }
                    var sql2 = "Update parentsku set quantity = quantity-@quantity where parentskuID=( select parentskuID from childsku where childskuID=@childSkuID)";
                    using (var cmd = new MySqlCommand(sql2, con))
                    {
                        //cmd.Parameters.AddWithValue("@oid", oid);
                        cmd.Parameters.AddWithValue("@childSkuID", p.ID); // The child SKU ID is selected once the user selects a size
                                                                          // cmd.Parameters.AddWithValue("@unitPrice", p.Price);
                        cmd.Parameters.AddWithValue("@quantity", p.Quantity);
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }

        // Insert into Customers
        public static void CreateGuestCustomer(MySqlConnection con, Customer c, out int cid)
        {
            cid = 0;
            if (TestConnection(con, out var errors))
            {
                con.Open();
                var sql = "Insert into customers" +
                          "(FirstName, LastName, Phone, Email, Street, City, State, Zip, Country, Company)" +
                          "Values(@firstName, @lastName, @phone, @email, @street, @city, @state, @zip, @country, @company)";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@firstName", c.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", c.LastName);
                    cmd.Parameters.AddWithValue("@phone", c.Phone);
                    cmd.Parameters.AddWithValue("@email", c.Email);
                    cmd.Parameters.AddWithValue("@street", c.Address);
                    cmd.Parameters.AddWithValue("@city", c.City);
                    cmd.Parameters.AddWithValue("@state", c.State);
                    cmd.Parameters.AddWithValue("@zip", c.Zip);
                    cmd.Parameters.AddWithValue("@country", c.Country);
                    cmd.Parameters.AddWithValue("@company", c.Company);
                    cmd.ExecuteNonQuery();
                }
                // Once the customer gets created we want to grab the CID of the customer for the orders table
                var last = "select CID from customers  order by CID desc limit 1";
                using (var cmd = new MySqlCommand(last, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cid = (int)dr["CID"];
                    }
                }
                con.Close();
            }
        }

        // Update Child SKU Quantity
        public static void UpdateChildSkuQuantity(MySqlConnection con, List<Product> products)
        {
            if (TestConnection(con, out var errors))
            {
                con.Open();
                foreach (var p in products)
                {
                    var sql = "Update childsku set quantity = quantity - " + p.Quantity +
                              "where childskuID = " + p.ID;
                    using (var cmd = new MySqlCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }


        public static List<Product> GetAccessories(MySqlConnection con, out bool status, out string message)
        {
            var products = new List<Product>(); // Create a List of Products to hold values from the Database
            const string query = "Select * from xehar_diversity_accessories"; // the string query we will give the database to execute
            string errorMessage; // This will be the error message if things go wrong with the database

            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new Product
                        {
                            ID = dr["PID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["PID"],
                            ParentID = dr["parentid"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["parentid"],
                            ProductName = dr["ProductName"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ProductName"],
                            ModelNumber = dr["ModelNumber"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ModelNumber"],
                            Quantity = dr["quantity"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["quantity"],
                            Price = dr["price"].GetType().FullName == "System.DBNull" ? 0.0M : (decimal)dr["price"],
                            Description = dr["desciption"].GetType().FullName == "System.DBNull" ? "" : (string)dr["desciption"],
                            Features = JsonConvert.DeserializeObject<ExtraFeatures>(dr["features"].ToString()),
                            Origin = dr["origin"].GetType().FullName == "System.DBNull" ? "" : (string)dr["origin"],
                        });
                    }
                    dr.Close();
                }
                con.Close();
                return products;
            }
            status = false;
            message = errorMessage;
            return products;
        }
        public static List<Product> GetSarahProducts(MySqlConnection con, out bool status, out string message)
        {
            var products = new List<Product>(); // Create a List of Products to hold values from the Database
            const string query = "Select * from xehar_diversity_sarah"; // the string query we will give the database to execute
            string errorMessage; // This will be the error message if things go wrong with the database

            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new Product
                        {
                            ID = dr["PID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["PID"],
                            ParentID = dr["parentid"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["parentid"],
                            ProductName = dr["ProductName"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ProductName"],
                            ModelNumber = dr["ModelNumber"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ModelNumber"],
                            Quantity = dr["quantity"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["quantity"],
                            Price = dr["price"].GetType().FullName == "System.DBNull" ? 0.0M : (decimal)dr["price"],
                            Description = dr["desciption"].GetType().FullName == "System.DBNull" ? "" : (string)dr["desciption"],
                            Features = JsonConvert.DeserializeObject<ExtraFeatures>(dr["features"].ToString()),
                            Origin = dr["origin"].GetType().FullName == "System.DBNull" ? "" : (string)dr["origin"],
                        });
                    }
                    dr.Close();
                }
                con.Close();
                return products;
            }
            status = false;
            message = errorMessage;
            return products;
        }
        public static List<Product> GetShoes(MySqlConnection con, out bool status, out string message)
        {
            var products = new List<Product>(); // Create a List of Products to hold values from the Database
            const string query = "Select * from xehar_diversity_shoes"; // the string query we will give the database to execute
            string errorMessage; // This will be the error message if things go wrong with the database

            // Open Connection using the helper function to validate the database connectivity. If the connection is successful then open do what we need to with it.
            if (TestConnection(con, out errorMessage))
            {
                status = true;
                message = errorMessage;
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        products.Add(new Product
                        {
                            ID = dr["PID"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["PID"],
                            ParentID = dr["parentid"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["parentid"],
                            ProductName = dr["ProductName"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ProductName"],
                            ModelNumber = dr["ModelNumber"].GetType().FullName == "System.DBNull" ? "" : (string)dr["ModelNumber"],
                            Quantity = dr["quantity"].GetType().FullName == "System.DBNull" ? 0 : (int)dr["quantity"],
                            Price = dr["price"].GetType().FullName == "System.DBNull" ? 0.0M : (decimal)dr["price"],
                            Description = dr["desciption"].GetType().FullName == "System.DBNull" ? "" : (string)dr["desciption"],
                            Features = JsonConvert.DeserializeObject<ExtraFeatures>(dr["features"].ToString()),
                            Origin = dr["origin"].GetType().FullName == "System.DBNull" ? "" : (string)dr["origin"],
                        });
                    }
                    dr.Close();
                }
                con.Close();
                return products;
            }
            status = false;
            message = errorMessage;
            return products;
        }
        #endregion

        #region Database Class Objects

        public class Product
        {
            public int ID { get; set; }
            public string SKU { get; set; }
            public string ProductName { get; set; }
            public string ModelNumber { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public string[] Images { get; set; }
            public string Size { get; set; }
            public int ParentID { get; set; }
            public string Description { get; set; }
            public ExtraFeatures Features { get; set; }
            public string Origin { get; set; }
        }

        public class ProductSizeQuantity
        {
            public int PID { get; set; }
            public int ParentID { get; set; }
            public int ChildID { get; set; }
            public string Size { get; set; }
            public int Quantity { get; set; }
        }
        public class ExtraFeatures
        {
            public string Style { get; set; }
            public string Material { get; set; }
            public string Measurements { get; set; }
        }

        public class Customer
        {
            public int CID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Company { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Country { get; set; }
        }

        public class Order
        {
            public int OID { get; set; }
            public int CID { get; set; }
            public int ChildSKUID { get; set; }
            public DateTime OrderDate { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string Country { get; set; }
            public int Quantity { get; set; }
            public decimal ShippingCost { get; set; }
        }

        public class ProductImages : IEnumerable
        {
            public int PID { get; set; }
            public string[] images { get; set; }
            public int IMID { get; set; }
            public int ParentID { get; set; }
            public IEnumerator GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public class Cart
        {
            public List<Product> Products { get; set; }
        }

        #endregion
    }
}
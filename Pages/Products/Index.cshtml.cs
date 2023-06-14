using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nma_graphics.Pages.Customers;
using nma_graphics.Pages.Products;
using System.Data.SqlClient;

namespace nma_graphics.Pages.Products
{
    public class IndexModel : PageModel
    {
        public List<ProductInfo> ListProducts = new List<ProductInfo>();
        public String errorMessage = "";
        public String successMessage = "";
        public String search = "";
        public void OnGet()
        {
            search = Request.Query["search"];
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM products WHERE productdes LIKE '%" + @search + "%'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo pi = new ProductInfo();
                                pi.id = "" + reader.GetInt32(0);
                                pi.productdes = reader.GetString(1);
                                pi.unitprice = (int)reader.GetDecimal(2);

                                ListProducts.Add(pi);

                            }
                            if (ListProducts.Count() == 0)
                            {
                                errorMessage = "Any Product was not found with name " + search;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class ProductInfo
    {
        public String id;
        public String productdes;
        public int unitprice;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using nma_graphics.Pages.Products;
using System.Data.SqlClient;

namespace nma_graphics.Pages.Products
{
    public class IndexModel : PageModel
    {
        public List<ProductInfo> ListProducts = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                //String connectionString = "Data Source=DESKTOP-DKT6IOK\\SQLEXPRESS;Initial Catalog=NMA_Graphics;Integrated Security=True";
                var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo ci = new ProductInfo();
                                ci.id = "" + reader.GetInt32(0);
                                ci.name = reader.GetString(1);
                                ci.contactno = reader.GetString(2);
                                ci.address = reader.GetString(3);
                                ci.email = reader.GetString(4);

                                ListProducts.Add(ci);

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
        public String name;
        public String contactno;
        public String address;
        public String email;
    }
}

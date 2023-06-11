using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace nma_graphics.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> ListCients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Kinetico;Integrated Security=True";
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo ci = new ClientInfo();
                                ci.id = "" + reader.GetInt32(0);
                                ci.name = reader.GetString(1);
                                ci.email = reader.GetString(2);
                                ci.phone = reader.GetString(3);
                                ci.address = reader.GetString(4);
                                ci.created_at = reader.GetDateTime(5).ToString();

                                ListCients.Add(ci);

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

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}

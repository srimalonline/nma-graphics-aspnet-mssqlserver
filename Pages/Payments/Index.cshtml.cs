using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace nma_graphics.Pages.Payments
{
    public class IndexModel : PageModel
    {
            public List<CustomerHistoryInfo> ListCustomerHistory = new List<CustomerHistoryInfo>();
            public String errorMessage = "";
            public String successMessage = "";
            public String search = "";
            public void OnGet()
            {
                search = Request.Query["search"];
                try
                {
                    //String connectionString = "Data Source=DESKTOP-DKT6IOK\\SQLEXPRESS;Initial Catalog=NMA_Graphics;Integrated Security=True";
                    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "SELECT * FROM viewdue";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    CustomerHistoryInfo chi = new CustomerHistoryInfo();
                                    chi.orderid = "" + reader.GetInt32(0);
                                    chi.order_deliver_date = reader.GetDateTime(1);
                                    chi.customerid = "" + reader.GetInt32(2);
                                    chi.opid = "" + reader.GetInt32(3);
                                     chi.total = (int)reader.GetDecimal(4);
                                    chi.paid = (int)reader.GetDecimal(5);
                                    chi.total = (int)reader.GetDecimal(6);

                                    ListCustomerHistory.Add(chi);

                                }
                                if (ListCustomerHistory.Count() == 0)
                                {
                                    errorMessage = "Any Customer was not found with name " + search;
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

        public class CustomerHistoryInfo
        {
            public String orderid;
            public DateTime order_deliver_date;
            public String customerid;
            public String opid;
            public int total;
            public int paid;
            public int due;
        }
}

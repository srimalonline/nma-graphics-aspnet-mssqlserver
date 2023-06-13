using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace nma_graphics.Pages.Customers
{
    public class CreateModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            customerInfo.name = Request.Form["name"];
            customerInfo.address = Request.Form["address"];
            customerInfo.contactno = Request.Form["contactno"];
            customerInfo.email = Request.Form["email"];
            if (customerInfo.name.Length == 0 || customerInfo.email.Length == 0 || customerInfo.contactno.Length == 9 || customerInfo.contactno.Length == 0 || customerInfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            // save the new client into the database
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO customers " +
                        "(name, contactno, address, email) VALUES " +
                        "(@name, @contactno, @address ,@email);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customerInfo.name);
                        command.Parameters.AddWithValue ("@contactno", customerInfo.contactno);
                        command.Parameters.AddWithValue("@address", customerInfo.address);
                        command.Parameters.AddWithValue("@email", customerInfo.email);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            customerInfo.name = ""; customerInfo.email = ""; customerInfo.contactno = ""; customerInfo.address = "";
            successMessage = "New Client Added Successfully";

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace nma_graphics.Pages.Customers
{
    public class EditModel : PageModel
    {
        public CustomerInfo customerInfo = new CustomerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String conectionString = "Data Source=DESKTOP-DKT6IOK\\SQLEXPRESS;Initial Catalog=NMA_Graphics;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM customers WHERE customerid=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerInfo.id = "" + reader.GetInt32(0);
                                customerInfo.name = reader.GetString(1);
                                customerInfo.contactno = reader.GetString(2);
                                customerInfo.address = reader.GetString(3);
                                customerInfo.email = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost() 
        {
            customerInfo.id = Request.Form["id"];
            customerInfo.name= Request.Form["name"];
            customerInfo.contactno = Request.Form["contactno"];
            customerInfo.address = Request.Form["address"];
            customerInfo.email= Request.Form["email"];
            if (customerInfo.name.Length == 0 || customerInfo.email.Length == 0 || customerInfo.contactno.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            // save the new client into the database
            try
            {
                String conectionString = "Data Source=.\\sqlexpress;Initial Catalog=NMA_Graphics;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(conectionString))
                {
                    connection.Open();
                    String sql = "UPDATE customers "+
                        "SET name=@name, contactno=@contactno, address=@address, email=@email "+
                        "WHERE customerid=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", customerInfo.name);
                        command.Parameters.AddWithValue("@contactno", customerInfo.contactno);
                        command.Parameters.AddWithValue("@address", customerInfo.address);
                        command.Parameters.AddWithValue("@email", customerInfo.email);
                        command.Parameters.AddWithValue("@id", customerInfo.id);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            Response.Redirect("/Customers/Index");
        }
    }
}

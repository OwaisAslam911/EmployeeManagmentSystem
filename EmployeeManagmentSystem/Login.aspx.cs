using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (ValidateUser(username, password))
            {
              
                Session["Username"] = username;  

               
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                // Show an error message if the credentials are incorrect
                errorMessage.Text = "Invalid username or password.";
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND UserPassword = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password); // Use plain-text password (not recommended for production)

                connection.Open();
                int userCount = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();

                return userCount > 0;  // If count is greater than 0, user is valid
            }
        }

    }
}
  

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
using System.Web.Http.Results;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        private int ValidateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            string query = "SELECT UserId FROM Users WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password); // Use plain-text password (not recommended for production)

                connection.Open();
                int result = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();

                return result != null ? Convert.ToInt32(result) : -1;   // If count is greater than 0, user is valid
            }
        }
        private List<string> GetUserRoles(int userId)
        {
            List<string> roles = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            string query = "SELECT r.RoleName FROM Role r " +
                           "JOIN UserRoles ur ON r.RoleId = ur.RoleId " +
                           "WHERE ur.UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add(reader["RoleName"].ToString());
                }
                connection.Close();
            }

            return roles;
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            int userId = ValidateUser(username, password);
            if (userId != 0)
            {
                Session["Username"] = username;
                 List<string> roles = GetUserRoles(userId);
        Session["Roles"] = roles;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                // Show an error message if the credentials are incorrect
                errorMessage.Text = "Invalid username or password.";
            }
        }

        

    }
}
  

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
        private (int userId, int employeeId) ValidateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            // Updated query to join Users and Employees on EmployeeId
            string query = "SELECT u.UserId, u.EmployeeId, e.EmployeeName " +
                           "FROM Users u " +
                           "JOIN Employees e ON u.EmployeeId = e.EmployeeId " +
                           "WHERE u.Username = @Username AND u.Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password); // Use a secure password method in production

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    int userId = Convert.ToInt32(reader["UserId"]);
                    int employeeId = Convert.ToInt32(reader["EmployeeId"]);
                    connection.Close();
                    return (userId, employeeId);  // Return both UserId and EmployeeId
                }
                else
                {
                    connection.Close();
                    return (0, 0);  // If no match is found
                }
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
            try
            {
                string username = usernameTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();

                var (userId, employeeId) = ValidateUser(username, password);

                if (userId != 0 && employeeId != 0)
                {
                    Session["Username"] = username;
                    Session["EmployeeId"] = employeeId;
                    List<string> roles = GetUserRoles(userId);
                    Session["Roles"] = roles;

                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    errorMessage.Text = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display it for debugging purposes
                errorMessage.Text = "An error occurred: " + ex.Message;
            }
        }




    }
}
  

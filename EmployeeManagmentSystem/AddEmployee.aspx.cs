using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class AddEmployee : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Roles"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }


                List<string> roles = (List<string>)Session["Roles"];

                if (!roles.Contains("Admin"))
                {
                    Response.Redirect("~/Default.aspx");
                }


            }
            BindManagersDropdown();
        }

        private void BindManagersDropdown()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            string query = "SELECT u.UserId, u.UserName " +
                           "FROM Users u " +
                           "INNER JOIN UserRoles ur ON u.UserId = ur.UserId " +
                           "INNER JOIN Roles r ON ur.RoleId = r.RoleId " +
                           "WHERE r.RoleName = 'Manager'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                managerDropdownList.DataSource = reader;  // Bind the data to the dropdown list
                managerDropdownList.DataTextField = "UserName"; // Display the manager's name
                managerDropdownList.DataValueField = "UserId"; // Use UserId as the value
                managerDropdownList.DataBind();

                connection.Close();
            }

            // Optionally, add a default item like "Select Manager"
            managerDropdownList.Items.Insert(0, new ListItem("Select Manager", "0"));
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string employeeName = EmployeeName.Text.Trim(); // Get employee name
            string userName = UserName.Text.Trim(); // Get username
            string email = Email.Text.Trim(); // Get email
            DateTime joiningDate = Convert.ToDateTime(JoiningDate.Text); // Get joining date
            string gender = GenderMale.Checked ? "Male" : "Female"; // Get gender from radio buttons
           
            bool isManager = IsManager.Checked; // Check if employee is also a manager
            decimal salary = decimal.Parse(Salary.Text);
            // Insert employee data
            AddEmployees(employeeName, userName, email, joiningDate, gender,  isManager, salary);
        }

        private void AddEmployees(string employeeName, string userName, string email, DateTime joiningDate, string gender, bool isManager, decimal salary)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (UserName, UserPassword, CreatedBy, CreatedAt) VALUES (@UserName, @UserPassword, @CreatedBy, @CreatedAt); " +
                               "INSERT INTO Employees (EmployeeName, EmployeeEmail, JoiningDate, Gender, status, Salary) VALUES (@EmployeeName, @Email, @JoiningDate, @Gender,  'Active', @Salary);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters for Users table
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@UserPassword", "defaultPassword"); // Set a default password or generate one
                    command.Parameters.AddWithValue("@CreatedBy", ""); // Or retrieve from session
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    // Add parameters for Employees table
                    command.Parameters.AddWithValue("@EmployeeName", employeeName);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@JoiningDate", joiningDate);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Salary", salary); // Pass the parsed decimal salary
                    

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            
            if (isManager)
            {
                AssignManagerRole(userName); 
            }
        }

        private void AssignManagerRole(string userName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO UserRoles (UserId, RoleId) " +
                               "SELECT u.UserId, r.RoleId FROM Users u " +
                               "JOIN Roles r ON r.RoleName = 'Manager' WHERE u.UserName = @UserName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


    }
}

public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeEmail { get; set; }
    public string JoiningDate { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public decimal Salary { get; set; }
    public decimal Status { get; set; }
    public decimal UserId { get; set; }
    public decimal Gender { get; set; }
   

   
}
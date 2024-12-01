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
                           "INNER JOIN Role r ON ur.RoleId = r.RoleId " +
                           "WHERE r.RoleName = 'Manager'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                managerDropdownList.DataSource = reader;  
                managerDropdownList.DataTextField = "UserName"; 
                managerDropdownList.DataValueField = "UserId"; 
                managerDropdownList.DataBind();

                connection.Close();
            }

            
            managerDropdownList.Items.Insert(0, new ListItem("Select Manager", "0"));
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string employeeName = EmployeeName.Text.Trim(); 
            string userName = UserName.Text.Trim(); 
            string email = Email.Text.Trim(); 
            DateTime joiningDate = Convert.ToDateTime(JoiningDate.Text);
            string phone = Phone.Text.Trim();
            string gender = GenderMale.Checked ? "Male" : "Female";
            string password = Password.Text.Trim();
            bool isManager = IsManager.Checked;
            decimal salary = decimal.Parse(Salary.Text);

            int managerId = 0;  
            if (managerDropdownList.SelectedValue != "0" && !string.IsNullOrEmpty(managerDropdownList.SelectedValue))
            {
                managerId = int.Parse(managerDropdownList.SelectedValue); 
            }

            AddEmployees(employeeName, userName, email, phone, joiningDate, gender, isManager, salary, password, managerId);
        }

       private void AddEmployees(string employeeName, string userName, string phone, string email, DateTime joiningDate, string gender, bool isManager, decimal salary, string password, int managerId)
{
    string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
    string roleName = isManager ? "Manager" : "Employee"; 

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        using (SqlCommand command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandText = @"
            DECLARE @EmployeeId INT;
            
            -- Insert into Employees and get the newly generated EmployeeId
            INSERT INTO Employees (EmployeeName, EmployeeEmail, Phone, JoiningDate, Gender, ManagerId, Status, Salary, CreatedAt, UpdatedAt)
            VALUES (@EmployeeName, @Email, @Phone, @JoiningDate, @Gender, @ManagerId, 'Active', @Salary, GETDATE(), GETDATE());
            SET @EmployeeId = SCOPE_IDENTITY();  -- Get the last inserted EmployeeId

            -- Insert into Users and link with the newly created EmployeeId
            INSERT INTO Users (UserName, Password, EmployeeId, Status, CreatedAt, UpdatedAt)
            VALUES (@UserName, @Password, @EmployeeId, 'Active', GETDATE(), GETDATE());

            -- Assign role in UserRoles
            INSERT INTO UserRoles (UserId, RoleId)
            SELECT u.UserId, r.RoleId
            FROM Users u
            JOIN Role r ON r.RoleName = @RoleName
            WHERE u.UserName = @UserName;
            ";

            command.Parameters.AddWithValue("@EmployeeName", employeeName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Phone", phone);  
            command.Parameters.AddWithValue("@JoiningDate", joiningDate);
            command.Parameters.AddWithValue("@Gender", gender);
            command.Parameters.AddWithValue("@ManagerId", managerId == 0 ? (object)DBNull.Value : managerId);
            command.Parameters.AddWithValue("@Salary", salary);

          
            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@RoleName", roleName);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}


    }
}
        
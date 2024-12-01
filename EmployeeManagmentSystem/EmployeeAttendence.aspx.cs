using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class EmployeeAttendence : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Session["Username"] == null)
                {
                    Response.Redirect("~/Login"); // Redirect to login if not logged in
                }
                else
                {
                    // Bind the attendance data based on the user's role (manager/admin)
                    BindAttendanceData();
                }
            }
        }

        private void BindAttendanceData()
        {
            string username = Session["Username"].ToString(); // Get the logged-in username
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            // Fetch the EmployeeId based on the logged-in user's Username
            int employeeId = GetEmployeeIdByUsername(username);

            // Check the user's role (manager/admin)
            string userRole = GetUserRoleByUsername(username);

            string query = string.Empty;

            // If the user is an admin, show all attendance data
            if (userRole == "Admin")
            {
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Remarks, a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "ORDER BY a.Date DESC";
            }
            // If the user is a manager, show only the attendance data of employees assigned to them
            else if (userRole == "Manager")
            {
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Remarks, a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "WHERE e.ManagerId = @EmployeeId " + // Filter by ManagerId (employees under this manager)
                        "ORDER BY a.Date DESC";
            }
            // If it's an employee, show only their own attendance
            else
            {
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Remarks, a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "WHERE a.EmployeeId = @EmployeeId " + // Filter by EmployeeId
                        "ORDER BY a.Date DESC";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                // Add parameter for filtering based on EmployeeId or ManagerId
                if (userRole != "Admin")
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                }

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Bind the fetched data to the GridView
                AttendanceGridView.DataSource = dataTable;
                AttendanceGridView.DataBind();
            }
        }

        private int GetEmployeeIdByUsername(string username)
        {
            int employeeId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            // Query to get EmployeeId from the Users table based on the logged-in username
            string query = "SELECT EmployeeId FROM Users WHERE Username = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", username);

                connection.Open();
                object result = command.ExecuteScalar(); // Execute the query and get the result

                if (result != null)
                {
                    employeeId = Convert.ToInt32(result); // Convert result to EmployeeId
                }
            }

            return employeeId; // Return the EmployeeId
        }

        private string GetUserRoleByUsername(string username)
        {
            string userRole = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            // Query to get the role of the user
            string query = "SELECT r.RoleName FROM Users u " +
                           "JOIN UserRoles ur ON u.UserId = ur.UserId " +
                           "JOIN Role r ON ur.RoleId = r.RoleId " +
                           "WHERE u.Username = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", username);

                connection.Open();
                object result = command.ExecuteScalar(); // Execute the query and get the result

                if (result != null)
                {
                    userRole = result.ToString(); // Return the role (e.g., Admin, Manager, Employee)
                }
            }

            return userRole; // Return the role
        }
    }
}

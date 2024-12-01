using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class AttendenceSheet : System.Web.UI.Page
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
                    // Bind the attendance data for the logged-in user
                    BindAttendanceData();
                }
            }
        }

        private void BindAttendanceData()
        {
            string username = Session["Username"].ToString(); // Get the logged-in username
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            // First, fetch the EmployeeId based on the logged-in user's Username
            int employeeId = GetEmployeeIdByUsername(username);

            // Now use the EmployeeId to filter the attendance data
            string query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Remarks, a.Status " +
                           "FROM Attendance a " +
                           "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                           "WHERE a.EmployeeId = @EmployeeId " +  // Filter by EmployeeId
                           "ORDER BY a.Date DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);  // Add the EmployeeId parameter
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
            string query = "SELECT EmployeeId FROM Users WHERE UserName = @UserName";

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
    }
}

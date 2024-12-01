using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace EmployeeManagmentSystem
{
    public partial class EmployeeAttendence : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Session["Username"] == null || Session["EmployeeId"] == null)
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
            int managerEmployeeId = Convert.ToInt32(Session["EmployeeId"]); // Get the logged-in manager's EmployeeId
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            // Get the user's role (Admin, Manager, or Employee)
            string userRole = GetUserRoleByUsername(username);

            string query;

            if (userRole == "Admin")
            {
                // Admin: Fetch all attendance data
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Remarks, a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "ORDER BY a.Date DESC";
            }
            else if (userRole == "Manager")
            {
                // Manager: Fetch attendance of employees managed by this manager
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Remarks, a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "WHERE e.ManagerId = @ManagerId " +
                        "ORDER BY a.Date DESC";
            }
            else
            {
                // Employee: Fetch only their own attendance
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Remarks, a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "WHERE e.EmployeeId = @EmployeeId " +
                        "ORDER BY a.Date DESC";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                // Add parameters to the query based on the user's role
                if (userRole == "Manager")
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ManagerId", managerEmployeeId);
                }
                else if (userRole != "Admin") // Employee-specific query
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeId", managerEmployeeId);
                }

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Bind the fetched data to the GridView
                AttendanceGridView.DataSource = dataTable;
                AttendanceGridView.DataBind();
            }
        }

        private string GetUserRoleByUsername(string username)
        {
            string userRole = string.Empty;
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            string query = "SELECT r.RoleName FROM Users u " +
                           "JOIN UserRoles ur ON u.UserId = ur.UserId " +
                           "JOIN Role r ON ur.RoleId = r.RoleId " +
                           "WHERE u.Username = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", username);

                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    userRole = result.ToString();
                }
            }

            return userRole;
        }
    }
}

using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
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
                if (Session["Roles"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }

                List<string> roles = (List<string>)Session["Roles"];

                if (!roles.Contains("Admin") && !roles.Contains("Manager "))
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            BindAttendanceData();
        }


        private void BindAttendanceData()
        {
            string username = Session["Username"].ToString(); 
            int managerEmployeeId = Convert.ToInt32(Session["EmployeeId"]); 
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            string userRole = GetUserRoleByUsername(username);

            string query;
            
            if (userRole == "Admin")
            {
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType, a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "ORDER BY a.Date DESC";
            }
            else if (userRole == "Manager ")
            {
                
                query = @"	SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType,  a.Status 
                        FROM Attendance a
                        JOIN Employees e ON a.EmployeeId = e.EmployeeId
                        WHERE e.ManagerId = @ManagerId
                        ORDER BY a.Date DESC";

            }
            else
            {
                query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType a.Status " +
                        "FROM Attendance a " +
                        "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                        "WHERE e.EmployeeId = @EmployeeId " +
                        "ORDER BY a.Date DESC";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                if (userRole == "Manager ")
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@ManagerId", managerEmployeeId);
                }
                else if (userRole != "Admin") 
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeId", managerEmployeeId);
                }

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

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
 
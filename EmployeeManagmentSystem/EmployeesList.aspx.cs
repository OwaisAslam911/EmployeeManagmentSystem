using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class EmployeeList : System.Web.UI.Page
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
                    Response.Redirect("~/AttendenceSheet.aspx");
                }
               
            }
            BindEmployeeData();
        }


        private void BindEmployeeData()
        {
            string username = Session["Username"].ToString();
            int managerEmployeeId = Convert.ToInt32(Session["EmployeeId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            string userRole = GetUserRoleByUsernameforEmp(username);

            string query;

            if (userRole == "Admin")
            {
                query = @"  SELECT e.EmployeeId, e.EmployeeName, e.JoiningDate, e.Gender, 
                   e.Salary, e.ManagerId, m.EmployeeName AS ManagerName, r.RoleName
            FROM Employees e
            LEFT JOIN Employees m ON e.ManagerId = m.EmployeeId
            JOIN Users u ON e.EmployeeId = u.EmployeeId
            JOIN UserRoles ur ON u.UserId = ur.UserId
            JOIN Role r ON ur.RoleId = r.RoleId";
            }
            else if (userRole == "Manager ")
            {

                query = @"
            SELECT e.EmployeeId, e.EmployeeName, e.JoiningDate, e.Gender, e.Salary, e.Status, 
            e.ManagerId, m.EmployeeName AS ManagerName, r.RoleName
            FROM Employees e
            LEFT JOIN Employees m ON e.ManagerId = m.EmployeeId
            JOIN Users u ON e.EmployeeId = u.EmployeeId
            JOIN UserRoles ur ON u.UserId = ur.UserId
            JOIN Role r ON ur.RoleId = r.RoleId
            WHERE e.ManagerId = @ManagerId";

            }
            else
            {
                query = @"SELECT e.EmployeeId, e.EmployeeName, e.Status , e.ManagerId
                        FROM Employees e
                        JOIN Role r ON r.RoleId = r.RoleId";
              
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

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        private string GetUserRoleByUsernameforEmp(string username)
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





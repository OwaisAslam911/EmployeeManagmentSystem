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
               
                if (Session["Username"] == null)
                {
                    Response.Redirect("~/Login"); 
                }
                else
                {
                    BindAttendanceData();
                }
            }
        }

        private void BindAttendanceData()
        {
            string username = Session["Username"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            int employeeId = GetEmployeeIdByUsername(username);

            string query = "SELECT a.AttendanceId, e.EmployeeName, a.Date, a.AttendanceType,  a.Status " +
                           "FROM Attendance a " +
                           "JOIN Employees e ON a.EmployeeId = e.EmployeeId " +
                           "WHERE a.EmployeeId = @EmployeeId " +  
                           "ORDER BY a.Date DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeId", employeeId);  
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                AttendanceGridView.DataSource = dataTable;
                AttendanceGridView.DataBind();
            }
        }

        private int GetEmployeeIdByUsername(string username)
        {
            int employeeId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            string query = "SELECT EmployeeId FROM Users WHERE UserName = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", username);

                connection.Open();
                object result = command.ExecuteScalar(); 

                if (result != null)
                {
                    employeeId = Convert.ToInt32(result); 
                }
            }

            return employeeId; 
        }
    }
}

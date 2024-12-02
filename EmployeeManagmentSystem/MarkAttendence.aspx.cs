using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class MarkAttendence : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("~/Login"); 
                }
            }
        }

        protected void MarkAttendanceButton_Click(object sender, EventArgs e)
        {
            string username = Session["Username"]?.ToString();
            string attendanceType = AttendanceType.SelectedValue; 

            if (string.IsNullOrEmpty(username))
            {
                MessageLabel.Text = "Session expired. Please log in again.";
                MessageLabel.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            try
            {
                
                int employeeId = GetEmployeeIdByUsername(username);

                if (employeeId == 0)
                {
                    MessageLabel.Text = "Invalid user.";
                    MessageLabel.ForeColor = System.Drawing.Color.Red;
                    return;
                }

               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string checkQuery = "SELECT COUNT(*) FROM Attendance WHERE EmployeeId = @EmployeeId AND [Date] = @Date";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                    checkCommand.Parameters.AddWithValue("@Date", DateTime.Now.Date);

                    connection.Open();
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageLabel.Text = "Attendance for today has already been marked.";
                        MessageLabel.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Attendance (EmployeeId, [Date], AttendanceType, Status) " +
                                   "VALUES (@EmployeeId, @Date, @AttendanceType, @Status)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    command.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                    command.Parameters.AddWithValue("@AttendanceType", attendanceType);
                   
                    command.Parameters.AddWithValue("@Status", "Active");

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageLabel.Text = "Attendance marked successfully.";
                        MessageLabel.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        MessageLabel.Text = "Failed to mark attendance. Please try again.";
                        MessageLabel.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = "An error occurred: " + ex.Message;
                MessageLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        private int GetEmployeeIdByUsername(string username)
        {
            int employeeId = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

            string query = "SELECT EmployeeId FROM Users WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

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

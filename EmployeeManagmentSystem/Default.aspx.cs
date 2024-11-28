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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                // If session is not set, redirect to login page
                Response.Redirect("~/Login");
            }
            else
            {
                // Session is set, proceed with page logic
                string username = Session["Username"].ToString();

            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            string query = @"Select EmployeeName, EmployeeEmail, JoiningDate, Gender,Salary from Employees";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader sqlReader = command.ExecuteReader();
                    GridView1.DataSource = sqlReader;
                    GridView1.DataBind();
                    sqlReader.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }


    }
    }

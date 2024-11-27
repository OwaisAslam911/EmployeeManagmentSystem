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
    public partial class EmployeesList : System.Web.UI.Page
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
        //protected void ReadGridViewData()
        //{
        //    foreach (GridViewRow row in GridView1.Rows)
        //    {
        //        // Ensure we are reading data from non-header rows
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            string EmployeeName = row.Cells[0].Text;
        //            string EmployeeEmial = row.Cells[1].Text;
        //            string JoiningDate = row.Cells[2].Text;
        //            string Gender = row.Cells[3].Text;

        //            string salary = row.Cells[5].Text;

        //            // You can now use these values as needed
        //            Response.Write($"EmployeeName: {EmployeeName}, EmployeeEmail: {EmployeeEmial}, JoiningDate: {JoiningDate}, Gender: {Gender},  Salary: {salary} <br>");
        //        }
        //    }
        //}

    }


}


public class Employees
        {
            public string EmployeeName { get; set; }
            public string Email { get; set; }
            public string JoiningDate { get; set; }
            public string Gender { get; set; }
            public string Salary { get; set; }
        }


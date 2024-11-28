using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class AttendenceSheet : System.Web.UI.Page
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

        }
    }
}
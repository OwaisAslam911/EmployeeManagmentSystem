using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] != null)
            {
                UserName.Text = Session["UserName"].ToString();
            }
            else
                {
                UserName.Text = "Hi, Guest"; 
                }
        }
        protected void logoutLink_Click(object sender, EventArgs e)
        {
            
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx"); 
        }

    }
}

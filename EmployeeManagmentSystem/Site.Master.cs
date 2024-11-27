using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagmentSystem
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Check if the user is authenticated
            //Session value is assign on the text box
            if (Session["UserName"] != null)
            {
                UserName.Text = Session["UserName"].ToString();
            }
            else
                {
                UserName.Text = "Hi, Guest"; // Default for unauthenticated users
                }
            }

        }
    }

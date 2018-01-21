using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceVolnteerBL;

namespace PoliceVolunteerUI
{
    public partial class MasterUI : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LogIn(object sender, EventArgs e)
        {
            string userName = userNameLog.Text;
            string password = passwordLog.Text;
            VolunteerBL volunteer = new VolunteerBL(userName, password);
            if (volunteer.PhoneNumber != "")
            {
                Session["User"] = volunteer.PhoneNumber;
            }
            userNameLog.Text = string.Empty;
            passwordLog.Text = string.Empty;
        }
        protected void LogOut(object sender, EventArgs e)
        {
            Session["User"] = string.Empty;
        }
    }
}
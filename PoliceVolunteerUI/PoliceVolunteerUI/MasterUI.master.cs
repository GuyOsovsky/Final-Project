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
            if (Session["User"].ToString() != "")
            {
                UserIdentefier.Text = "שלום " + (new VolunteerBL(Session["User"].ToString())).FName + " " + (new VolunteerBL(Session["User"].ToString())).LName;
            }
            else
            {
                UserIdentefier.Text = "";
            }
        }

        //is admin and is able to manage other volunteers
        protected bool IsAbleVolunteer()
        {
            if (Session["User"].ToString() == "") return false;
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            return volunteer.Type.PermmisionVolunteer;
        }

        protected void LogIn(object sender, EventArgs e)
        {
            //get parameters
            string userName = userNameLog.Text;
            string password = passwordLog.Text;
            //check if the volunteer exist
            VolunteerBL volunteer = new VolunteerBL(userName, password);
            if (volunteer.PhoneNumber != "" && volunteer.Status)
            {
                Session["User"] = volunteer.PhoneNumber;
            }
            //clear text boxes
            userNameLog.Text = string.Empty;
            passwordLog.Text = string.Empty;
            //refresh page
            Response.Redirect(Request.RawUrl);
        }

        protected void LogOut(object sender, EventArgs e)
        {
            Session["User"] = "";
            Response.Redirect("HomePageUI.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PoliceVolnteerBL;
using PoliceVolnteerDAL;

namespace PoliceVolunteerUI
{
    public partial class ActivitysUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            FillActivitys();
            FillSignedActivitys();
        }

        protected void FillActivitys()
        {
            DataSet activitys = ActivitysBL.GetActivities();
            FieldValue<ActivityField> Mask = new FieldValue<ActivityField>(ActivityField.ActivityDate, DateTime.Now, FieldType.Date, OperatorType.Greater);
            activitys.Tables[0].DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredTable = (activitys.Tables[0].DefaultView).ToTable();
            DataView dataView = new DataView(FilteredTable);
            ActivitysInformation.DataSource = dataView;
            ActivitysInformation.DataBind();
        }

        protected void FillSignedActivitys()
        {
            DataView dataView = new DataView((new VolunteerBL(Session["User"].ToString()).GetActivitys(DateTime.Now, PoliceVolnteerDAL.OperatorType.Greater)));
            SignedActivitys.DataSource = dataView;
            SignedActivitys.DataBind();
        }

        protected void ActivitySignUp(object sender, EventArgs e)
        {
            GridViewRow row = ActivitysInformation.Rows[int.Parse(((Button)sender).CommandArgument)];
            VolunteerBL User = new VolunteerBL(Session["User"].ToString());
            User.ActivitySignUp(int.Parse(((Label)row.Cells[0].FindControl("lblActivityCode")).Text));
        }

        protected void ActivitySignOut(object sender, EventArgs e)
        {
            GridViewRow row = SignedActivitys.Rows[int.Parse(((Button)sender).CommandArgument)];
            VolunteerBL User = new VolunteerBL(Session["User"].ToString());
            User.ActivitySignOut(int.Parse(((Label)row.Cells[0].FindControl("lblActivityCode")).Text));
        }
        
    }
}
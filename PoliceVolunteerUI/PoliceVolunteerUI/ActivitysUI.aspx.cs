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
            //fill activity gridview
            FillActivitys();
            FillSignedActivitys();
        }

        protected void FillActivitys()
        {
            Queue<FieldValue<ActivityField>> parameters = new Queue<FieldValue<ActivityField>>();
            parameters.Enqueue(new FieldValue<ActivityField>(ActivityField.ActivityDate, DateTime.Now, PoliceVolnteerDAL.Table.Activity, FieldType.Date, OperatorType.GreaterAndEquals));
            parameters.Enqueue(new FieldValue<ActivityField>(ActivityField.StartTime, DateTime.Now, PoliceVolnteerDAL.Table.Activity, FieldType.Time, OperatorType.Greater));
            //get all future activitys
            DataSet activitys = (new ActivitysBL(parameters, true)).Activitys;
            //filter out registered activitys
            for (int i = 0; i < activitys.Tables[0].Rows.Count; i++)
            {
                ReportsBL reports = new ReportsBL(Session["User"].ToString(), int.Parse(activitys.Tables[0].Rows[i]["ActivityCode"].ToString()));
                if (reports.Reports.Tables[0].Rows.Count > 0)
                {
                    activitys.Tables[0].Rows[i].Delete();
                    //i--;
                }
            }
            //bind data to gridview
            DataView dataView = new DataView(activitys.Tables[0]);
            ActivitysInformation.DataSource = dataView;
            ActivitysInformation.DataBind();
        }

        protected void FillSignedActivitys()
        {
            //get all registered activitys
            DataView dataView = new DataView((new VolunteerBL(Session["User"].ToString()).GetActivitys(DateTime.Now, PoliceVolnteerDAL.OperatorType.Greater)));
            //bind data to gridview
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
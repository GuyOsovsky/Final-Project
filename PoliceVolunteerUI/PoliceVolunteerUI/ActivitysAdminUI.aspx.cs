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
    public partial class ActivitysAdminUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            if(!(new VolunteerBL(Session["User"].ToString()).Type.PermmisionActivity))
            {
                Response.Redirect("HomePageUI.aspx");
            }
            //fill activity gridview
            FillActivitys();
            FillReports();
            FillOtherActivitys();
            if (!IsPostBack)
            {
                FillActivityList();
                FillVolunteerList();
            }
        }

        protected void FillActivitys()
        {
            //get all activitys
            DataSet activitys = (new ActivitysBL()).Activitys;
            activitys.Tables[0].Rows.Add();
            //bind data to gridview
            DataView dataView = new DataView(activitys.Tables[0]);
            ActivitysInformation.DataSource = dataView;
            ActivitysInformation.EditIndex = activitys.Tables[0].Rows.Count - 1;
            ActivitysInformation.DataBind();
        }

        protected void FillOtherActivitys()
        {
            WebServiceReference.generalWS webService = new WebServiceReference.generalWS();
            OtherActivitys.DataSource = webService.GetFutureActivitys().Tables[0];
            OtherActivitys.DataBind();
        }

        protected void FillReports()
        {
            ////get all registered activitys
            ReportsBL reports;
            if (ActivitysChooseReports.Text != "" && VolunteerChooseReports.Text != "")
                reports = new ReportsBL(VolunteerChooseReports.SelectedValue.ToString(), int.Parse(ActivitysChooseReports.SelectedValue.ToString()));
            else if (ActivitysChooseReports.Text == "" && VolunteerChooseReports.Text != "")
                reports = new ReportsBL(VolunteerChooseReports.SelectedValue.ToString());
            else if (ActivitysChooseReports.Text != "" && VolunteerChooseReports.Text == "")
                reports = new ReportsBL(int.Parse(ActivitysChooseReports.SelectedValue.ToString()));
            else
                reports = new ReportsBL("");//get an empty dataset
            DataView dataView = new DataView(reports.Reports.Tables[0]);
            //bind data to gridview
            Reports.DataSource = dataView;
            Reports.DataBind();
        }

        protected void FillActivityList()
        {
            //clear list
            ActivitysChooseReports.Items.Clear();
            //load activitys
            ActivitysBL activitys = new ActivitysBL();
            //add a blank space
            ActivitysChooseReports.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            //add all activitys to the list
            foreach (DataRow activity in activitys.Activitys.Tables[0].Rows)
            {
                ActivitysChooseReports.Items.Add(new ListItem(activity["ActivityName"].ToString(), activity["ActivityCode"].ToString()));
            }
            ActivitysChooseReports.DataBind();
        }

        protected void FillVolunteerList()
        {
            //clear list
            VolunteerChooseReports.Items.Clear();
            //load volunteers
            VolunteersBL volunteers = new VolunteersBL(false);
            //add a blank space
            VolunteerChooseReports.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            //add all volunteers to list
            foreach (VolunteerBL volunteer in volunteers.VolunteerList)
            {
                VolunteerChooseReports.Items.Add(new ListItem(volunteer.FName + " " + volunteer.LName, volunteer.PhoneNumber));
            }
            VolunteerChooseReports.DataBind();
        }

        protected void FillActivityTypesList(object sender, EventArgs e)
        {
            ActivitysTypes types = new ActivitysTypes();
            foreach (DataRow row in types.activityTypes.Tables[0].Rows)
            {
                ((DropDownList)sender).Items.Add(new ListItem(row["typeName"].ToString(), row["typeCode"].ToString()));
            }
            ((DropDownList)sender).DataBind();
        }

        protected void FillActivityManagerList(object sender, EventArgs e)
        {
            VolunteersBL volunteers = new VolunteersBL(false);
            foreach (VolunteerBL volunteer in volunteers.VolunteerList)
            {
                ((DropDownList)sender).Items.Add(new ListItem(volunteer.FName + " " + volunteer.LName, volunteer.PhoneNumber));
            }
            ((DropDownList)sender).DataBind();
        }

        protected void AddNewActivity(object sender, EventArgs e)
        {
            GridViewRow row = ActivitysInformation.Rows[ActivitysInformation.EditIndex];
            string activityName = ((TextBox)row.Cells[1].FindControl("InputActivityName")).Text;
            DateTime activityDate = DateTime.ParseExact(((TextBox)row.Cells[2].FindControl("InputActivityDate")).Text, "yyyy-M-d", System.Globalization.CultureInfo.InvariantCulture);
            DateTime activityStartTime = DateTime.ParseExact(((TextBox)row.Cells[3].FindControl("InputActivityStartTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime activityFinishTime = DateTime.ParseExact(((TextBox)row.Cells[4].FindControl("InputActivityFinishTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            string activityPlace = ((TextBox)row.Cells[5].FindControl("InputActivityPlace")).Text;
            string activityManeger = ((DropDownList)row.Cells[6].FindControl("InputActivityManeger")).SelectedValue.ToString();
            int activityMinParticipents = int.Parse(((TextBox)row.Cells[7].FindControl("InputActivityMinParticipents")).Text);
            int activityTypeCode = int.Parse(((DropDownList)row.Cells[8].FindControl("InputActivityTypeName")).SelectedValue.ToString()); 
            ActivityBL activity = new ActivityBL(activityName, activityDate, activityStartTime, activityFinishTime, activityManeger, activityTypeCode, activityPlace, activityMinParticipents);

        }

        //protected void DeleteActivity(object sender, EventArgs e)
        //{
        //    ActivityBL activity = new ActivityBL(0);
        //}

        protected void AddNewOtherActivity(object sender, EventArgs e)
        {
            GridViewRow row = OtherActivitys.Rows[int.Parse(((Button)sender).CommandArgument)];
            WebServiceReference.generalWS webService = new WebServiceReference.generalWS();
            WebServiceReference.ActivityBL newActivity = webService.GetActivity(int.Parse(((Label)row.Cells[0].FindControl("lblActivityCode")).Text));
            ActivityBL activity = new ActivityBL(newActivity.ActivityName, newActivity.ActivityDate, newActivity.StartTime, newActivity.FinishTime, newActivity.ActivityManager, newActivity.TypeCode, newActivity.Place, newActivity.MinNumberOfVolunteer);
            DataTable allActivitys = (new ActivitysBL(DateTime.Now)).Activitys.Tables[0];
            for (int i = 0; i < allActivitys.Rows.Count; i++)
            {
                ActivityBL compare = new ActivityBL(int.Parse(allActivitys.Rows[i]["ActivityCode"].ToString()));
                if (compare.Equals(activity))
                {
                    activity.DeleteActivity();
                    break;
                }
            }
        }

    }
}
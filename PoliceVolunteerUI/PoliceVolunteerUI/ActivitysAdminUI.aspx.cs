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
        }

        protected void FillActivitys()
        {
            //get all activitys
            DataSet activitys = (new ActivitysBL()).Activitys;
            //bind data to gridview
            DataView dataView = new DataView(activitys.Tables[0]);
            ActivitysInformation.DataSource = dataView;
            ActivitysInformation.DataBind();
        }

        protected void FillReports()
        {
            ////get all registered activitys
            ReportsBL reports = new ReportsBL();
            DataView dataView = new DataView(reports.Reports.Tables[0]);
            //bind data to gridview
            Reports.DataSource = dataView;
            Reports.DataBind();
        }
    }
}
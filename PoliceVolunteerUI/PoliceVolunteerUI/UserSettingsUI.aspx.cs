using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceVolnteerBL;

namespace PoliceVolunteerUI
{
    public partial class UserSettingsUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            DataView dataView = new DataView(volunteer.VolunteerToDataSet());
            UserInformationGV.DataSource = dataView;
            UserInformationGV.DataBind();
            //UserInformationGV.
        }
    }
}
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
            DataTable volunteerTable = new DataTable();
            volunteerTable.Columns.Add("מספר טלפון", typeof(String));
            volunteerTable.Columns.Add("מספר טלפון חירום", typeof(String));
            volunteerTable.Columns.Add("שם פרטי", typeof(String));
            volunteerTable.Columns.Add("שם משפחה", typeof(String));
            volunteerTable.Columns.Add("תאריך לידה", typeof(DateTime));
            volunteerTable.Columns.Add("שם משתמש", typeof(String));
            volunteerTable.Columns.Add("כתובת מגורים", typeof(String));
            volunteerTable.Columns.Add("עיר מגורים", typeof(String));
            volunteerTable.Columns.Add("אימייל", typeof(String));
            volunteerTable.Columns.Add("מספר זהות משטרתי", typeof(String));
            volunteerTable.Columns.Add("עיר שירות", typeof(String));
            volunteerTable.Columns.Add("תאריך התחלה", typeof(DateTime));
            volunteerTable.Rows.Add();
            volunteerTable.Rows[0][0] = volunteer.PhoneNumber;
            volunteerTable.Rows[0][1] = volunteer.EmergencyNumber;
            volunteerTable.Rows[0][2] = volunteer.FName;
            volunteerTable.Rows[0][3] = volunteer.LName;
            volunteerTable.Rows[0][4] = volunteer.BirthDate;
            volunteerTable.Rows[0][5] = volunteer.UserName;
            volunteerTable.Rows[0][6] = volunteer.HomeAddress;
            volunteerTable.Rows[0][7] = volunteer.HomeCity;
            volunteerTable.Rows[0][8] = volunteer.EmailAddress;
            volunteerTable.Rows[0][9] = volunteer.PoliceID;
            volunteerTable.Rows[0][10] = volunteer.ServeCity;
            volunteerTable.Rows[0][11] = volunteer.StartDate;
            DataView dataView = new DataView(volunteerTable);
            DataList1.DataSource = dataView;
            DataList1.DataBind();
            //UserInformationGV.DataSource = dataView;
            //UserInformationGV.DataBind();
            //UserInformationGV.
        }
    }
}
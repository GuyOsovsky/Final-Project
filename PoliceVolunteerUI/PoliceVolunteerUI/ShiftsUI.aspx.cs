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
    public partial class ShiftsUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            FillShifts();
            FillSignedShifts();
        }

        protected void FillShifts()
        {
            DataSet shifts = (new ShiftsBL(DateTime.Now)).GetDetails();
            FieldValue<ShiftsField> Mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, DateTime.Now, FieldType.Date, OperatorType.Greater);
            shifts.Tables[0].DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredTable = (shifts.Tables[0].DefaultView).ToTable();
            DataView dataView = new DataView(FilteredTable);
            ShiftsInformation.DataSource = dataView;
            ShiftsInformation.DataBind();
        }

        protected void FillSignedShifts()
        {
            DataTable shifts = (new VolunteerBL(Session["User"].ToString()).GetShifts(DateTime.Now, PoliceVolnteerDAL.OperatorType.Greater));
            shifts.Columns.Add("ShiftType", typeof(string));
            foreach (DataRow shift in shifts.Rows)
            {
                shift["ShiftType"] = (new ShiftTypesBL(int.Parse(shift["TypeCode"].ToString()))).TypeName;
            }
            DataView dataView = new DataView(shifts);
            SignedShifts.DataSource = dataView;
            SignedShifts.DataBind();
        }

        protected void ShiftsSignUp(object sender, EventArgs e)
        {
            GridViewRow row = ShiftsInformation.Rows[int.Parse(((Button)sender).CommandArgument)];
            VolunteerBL user = new VolunteerBL(Session["user"].ToString());
            user.ShiftSignUp(int.Parse(((Label)row.Cells[0].FindControl("lblShiftCode")).Text));
        }

        protected void ShiftsSignOut(object sender, EventArgs e)
        {
            GridViewRow row = SignedShifts.Rows[int.Parse(((Button)sender).CommandArgument)];
            VolunteerBL User = new VolunteerBL(Session["User"].ToString());
            User.ShiftSignOut(int.Parse(((Label)row.Cells[0].FindControl("lblShiftCode")).Text));
        }
    }
}
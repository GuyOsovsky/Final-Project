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
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            //fill shifts gridview
            FillShifts();
            FillSignedShifts();
        }

        protected void FillShifts()
        {
            Queue<FieldValue<ShiftsField>> parameters = new Queue<FieldValue<ShiftsField>>();
            parameters.Enqueue(new FieldValue<ShiftsField>(ShiftsField.DateOfShift, DateTime.Now, PoliceVolnteerDAL.Table.Shifts, FieldType.Date, OperatorType.Greater));
            //parameters.Enqueue(new FieldValue<ShiftsField>(ShiftsField.StartTime, DateTime.Now, PoliceVolnteerDAL.Table.Shifts, FieldType.Time, OperatorType.Greater));
            //get all shifts
            DataSet shifts = (new ShiftsBL(parameters, true)).GetDetails();
            DataTable FilteredTable = shifts.Tables[0];
            //filter all registered shifts
            for (int i = 0; i < FilteredTable.Rows.Count; i++)
            {
                VolunteerBL user = new VolunteerBL(Session["User"].ToString());
                DataTable UserShifts = user.GetShifts(DateTime.Now, OperatorType.Greater);
                for (int j = 0; j < UserShifts.Rows.Count; j++)
                {
                    if(UserShifts.Rows[j]["ShiftCode"].ToString() == FilteredTable.Rows[i]["ShiftCode"].ToString())
                    {
                        FilteredTable.Rows[i].Delete();
                        //i--;
                        break;
                    }
                }

            }
            //bind data to gridview
            DataView dataView = new DataView(FilteredTable);
            ShiftsInformation.DataSource = dataView;
            ShiftsInformation.DataBind();
        }

        protected void FillSignedShifts()
        {
            //get all registered shifts
            DataTable shifts = (new VolunteerBL(Session["User"].ToString()).GetShifts(DateTime.Now, PoliceVolnteerDAL.OperatorType.Greater));
            //add type column
            shifts.Columns.Add("ShiftType", typeof(string));
            foreach (DataRow shift in shifts.Rows)
            {
                shift["ShiftType"] = (new ShiftTypesBL(int.Parse(shift["TypeCode"].ToString()))).TypeName;
            }
            //bind data to gridview
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
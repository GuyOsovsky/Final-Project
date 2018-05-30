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

            FillBlankComments();

            //fill shifts gridviews
            FillShifts();
            FillSignedShifts();
            
        }

        protected void FillShifts()
        {
            ////get all shifts
            DataSet shifts = (new ShiftsBL(DateTime.Now)).GetDetails();
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

        protected void FillBlankComments()
        {
            //get volunteer info
            VolunteerBL vol = new VolunteerBL(Session["User"].ToString());
            //get all his old shifts
            DataTable shifts = vol.GetShifts(DateTime.Now, OperatorType.Lower);

            //change typeCode(number) of shift to shiftType(text)
            shifts.Columns.Add("ShiftType", typeof(string));
            foreach(DataRow row in shifts.Rows)
            {
                row["ShiftType"] = (new ShiftTypesBL(int.Parse(row["TypeCode"].ToString()))).TypeName;
            }
            shifts.Columns.Remove("TypeCode");

            //get all old reports of the volunteer
            DataTable reports = vol.GetShiftReports(DateTime.Now, OperatorType.Lower);

            //create a combine table
            DataTable combineTable = shifts;
            //add comments column
            combineTable.Columns.Add("comments");
            
            //for each shift add his comments by shiftCode, and remove all his finished comments
            for (int j = 0; j < combineTable.Rows.Count; j++)
            {
                for (int i = 0; i < reports.Rows.Count; i++)
                {
                    if (int.Parse(combineTable.Rows[j]["ShiftCode"].ToString()) == int.Parse(reports.Rows[i]["shiftCode"].ToString()))
                    {
                        //add shift comments by shiftCode
                        combineTable.Rows[j]["comments"] = reports.Rows[i]["comments"];
                        break;
                    }
                }
                if (combineTable.Rows[j]["comments"].ToString() != "")
                {
                    //remove finished comments
                    combineTable.Rows[j].Delete();
                    j--;
                }
            }

            blankShiftComments.DataSource = combineTable;
            blankShiftComments.DataBind();
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
        
        protected void UpdateComment(object sender, EventArgs e)
        {
            GridViewRow row = blankShiftComments.Rows[int.Parse(((Button)sender).CommandArgument)];

            VolunteerBL vol = new VolunteerBL(Session["User"].ToString());

            int shiftCode = int.Parse(((Label)row.Cells[0].FindControl("lblShiftCode")).Text);
            ShiftBL shift = new ShiftBL(shiftCode);

            string comment = ((TextBox)row.Cells[6].FindControl("CommentText")).Text;

            vol.ShiftReport(shift, comment);
        }

    }
}
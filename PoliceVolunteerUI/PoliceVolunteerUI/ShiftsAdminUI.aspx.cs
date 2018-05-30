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
    public partial class ShiftsAdminUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }

            FillCompleteShiftComments();
            FillCompleteCarReports();
            FillShifts();
            FillCarReports();

        }

        protected void FillShifts()
        {
            //get all shifts
            DataSet shifts = (new ShiftsBL(DateTime.Now)).GetDetails();
            shifts.Tables[0].Rows.Add();
            DataView dataView = new DataView(shifts.Tables[0]);
            ShiftsInformation.DataSource = dataView;
            ShiftsInformation.EditIndex = shifts.Tables[0].Rows.Count - 1;
            ShiftsInformation.DataBind();
        }

        protected void AddNewShift(object sender, EventArgs e)
        {
            GridViewRow row = ShiftsInformation.Rows[ShiftsInformation.EditIndex];
            int shiftType = int.Parse(((DropDownList)row.Cells[1].FindControl("InputShiftType")).SelectedValue.ToString());
            DateTime dateOfShift = DateTime.ParseExact(((TextBox)row.Cells[2].FindControl("InputDateOfShift")).Text, "yyyy-M-d", System.Globalization.CultureInfo.InvariantCulture);
            DateTime shiftStartTime = DateTime.ParseExact(((TextBox)row.Cells[3].FindControl("InputShiftStartTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime shiftFinishTime = DateTime.ParseExact(((TextBox)row.Cells[4].FindControl("InputShiftFinishTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            string shiftPlace = ((TextBox)row.Cells[5].FindControl("InputShiftPlace")).Text;
            ShiftBL shift = new ShiftBL(shiftType, dateOfShift, shiftStartTime, shiftFinishTime, shiftPlace);
        }

        protected void FillShiftTypesList(object sender, EventArgs e)
        {
            ShiftsTypesBL types = new ShiftsTypesBL();
            foreach(DataRow row in types.shiftTypes.Tables[0].Rows)
            {
                ((DropDownList)sender).Items.Add(new ListItem(row["TypeName"].ToString(), row["typeCode"].ToString()));
            }
            ((DropDownList)sender).DataBind();
        }

        protected void FillCompleteCarReports()
        {
            DataTable shifts = new ShiftsBL((new DateTime(1999, 1, 1)), DateTime.Now).GetDetails().Tables[0];

            DataTable reports = ShiftsBL.GetAllCarReports();

            DataTable result = shifts;

            result.Columns.Add("CarID");
            result.Columns.Add("Distance");

            for (int j = 0; j < result.Rows.Count; j++)
            {
                for (int i = 0; i < reports.Rows.Count; i++)
                {
                    int rShiftCode = int.Parse(reports.Rows[i]["ShiftCode"].ToString());
                    int sShiftCode = int.Parse(result.Rows[j]["ShiftCode"].ToString());

                    if (rShiftCode == sShiftCode)
                    {
                        result.Rows[j]["CarID"] = reports.Rows[i]["CarID"];
                        result.Rows[j]["Distance"] = reports.Rows[i]["Distance"];
                        break;
                    }
                }
            }

            foreach(DataRow row in result.Rows)
            {
                if (row["CarID"].ToString() == "" || row["Distance"].ToString() == "")
                    row.Delete();
            }

            CompleteCarReport.DataSource = result;
            CompleteCarReport.DataBind();
        }

        protected void FillCompleteShiftComments()
        {
            DataTable shifts = (new ShiftsBL((new DateTime(1999, 1, 1)), DateTime.Now)).shifts.Tables[0];
            shifts.Columns.Add("ShiftType", typeof(string));
            foreach (DataRow row in shifts.Rows)
            {
                row["ShiftType"] = (new ShiftTypesBL(int.Parse(row["TypeCode"].ToString()))).TypeName;
            }
            //shifts.Columns.Remove("TypeCode");

            DataTable reports = ShiftsBL.GetAllShiftReports();

            //create a combine table
            DataTable combineTable = shifts;
            //add comments column
            combineTable.Columns.Add("comments");

            for (int j = 0; j < combineTable.Rows.Count; j++)
            {
                for (int i = 0; i < reports.Rows.Count; i++)
                {
                    int rShiftCode = int.Parse(reports.Rows[i]["ShiftCode"].ToString());
                    int sShiftCode = int.Parse(combineTable.Rows[j]["ShiftCode"].ToString());

                    if (rShiftCode == sShiftCode)
                    {
                        combineTable.Rows[j]["comments"] = reports.Rows[i]["comments"];
                        break;
                    }
                }
            }

            foreach (DataRow row in combineTable.Rows)
            {
                if (row["comments"].ToString() == "")
                    row.Delete();
            }

            CompleteShiftComments.DataSource = combineTable;
            CompleteShiftComments.DataBind();
        }

        protected void FillCarReports()
        {
            DataTable shifts = new ShiftsBL((new DateTime(1999, 1, 1)), DateTime.Now).GetDetails().Tables[0];

            DataTable reports = ShiftsBL.GetAllCarReports();

            DataTable result = shifts;

            result.Columns.Add("CarID");
            result.Columns.Add("Distance");

            for (int j = 0; j < result.Rows.Count; j++)
            {
                bool flag = false;
                for (int i = 0; i < reports.Rows.Count; i++)
                {
                    int rShiftCode = int.Parse(reports.Rows[i]["ShiftCode"].ToString());
                    int sShiftCode = int.Parse(result.Rows[j]["ShiftCode"].ToString());

                    if (rShiftCode == sShiftCode)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    result.Rows[j].Delete();
                    //j--;
                }
            }

            CarReport.DataSource = result;
            CarReport.DataBind();
        }

        protected void UpdateCarReport(object sender, EventArgs e)
        {
            GridViewRow row = CarReport.Rows[int.Parse(((Button)sender).CommandArgument)];

            int shiftCode = int.Parse(((Label)row.Cells[0].FindControl("lblShiftCode")).Text);
            ShiftBL shift = new ShiftBL(shiftCode);

            string carID = ((DropDownList)row.Cells[6].FindControl("ChooseCarID")).Text;
            int Distance = int.Parse(((TextBox)row.Cells[6].FindControl("DistanceTbx")).Text);

            DataRow volRow = VolunteerBL.GetVolunteerByCarID(carID);
            VolunteerBL vol = new VolunteerBL(volRow["PhoneNumber"].ToString());

            vol.ShiftReport(shift, "", carID, Distance);
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ChooseCarID = (e.Row.FindControl("ChooseCarID") as DropDownList);

                DataTable carsTable = VolunteersBL.GetAllCars();

                foreach (DataRow car in carsTable.Rows)
                {
                    VolunteerBL vol = new VolunteerBL(car["PhoneNumber"].ToString());
                    string text = vol.FName + " " + vol.LName + " - " + car["CarID"];
                    ChooseCarID.Items.Add(new ListItem(text, car["CarID"].ToString()));
                }
                ChooseCarID.DataBind();
            }
        }
    }
}
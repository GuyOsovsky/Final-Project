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
            
        }

        protected void Fill_User_Settings()
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
            //volunteerTable.Rows.Add();
            //for (int i = 0; i < 12; i++)
            //{
            //    volunteerTable.Rows[0][i] = "";
            //}
            DataView dataView = new DataView(volunteerTable);
            UserInformation.DataSource = dataView;
            UserInformation.DataBind();
        }

        protected void UserInformation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////// First, make sure we're dealing a Data Row
            //if (e.Row.RowType != DataControlRowType.Header &&
            //    e.Row.RowType != DataControlRowType.Footer &&
            //    e.Row.RowType != DataControlRowType.Pager)
            //{ 

            //    if (e.Row.RowState != (DataControlRowState.Edit | DataControlRowState.Alternate) &&
            //        e.Row.RowState != (DataControlRowState.Edit | DataControlRowState.Normal))
            //    {
            //        // חישוב מחיר כולל לשורה
            //        Label l1 = (Label)e.Row.Cells[4].FindControl("LabelSum");
            //        //object price = DataBinder.Eval(e.Row.DataItem, "Price");
            //        //object Quantity = DataBinder.Eval(e.Row.DataItem, "Quantity");
            //        short Quantity = Convert.ToInt16(e.Row.Cells[1].Text);
            //        decimal price = Convert.ToDecimal(e.Row.Cells[2].Text);
            //        decimal s = (decimal)(price * Quantity);
            //        l1.Text = s.ToString();
            //    }
            //    else
            //    {
            //        Label l1 = (Label)e.Row.Cells[3].FindControl("LabelSum");
            //        //object price = DataBinder.Eval(e.Row.DataItem, "Price");
            //        //object Quantity = DataBinder.Eval(e.Row.DataItem, "Quantity");
            //        short Quantity = Convert.ToInt16(((TextBox)e.Row.Cells[1].Controls[0]).Text);
            //        decimal price = Convert.ToDecimal(e.Row.Cells[2].Text);

            //        decimal s = (decimal)(price * Quantity);
            //        l1.Text = s.ToString();
            //    }
            //}

            //// הצגת מחיר סופי של הסל 
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label l1 = (Label)e.Row.Cells[3].FindControl("LabelFooter");

            //    mShoppingBag = (Bag.ShoppingBag)Session["myShoppingBag"];

            //    double s = mShoppingBag.GetFinalPrice();
            //    l1.Text = s.ToString();
            //}
        }

        protected void Edit_Settings(object sender, EventArgs e)
        {

        }

        protected void UserInformation_RowEditing_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void UserInformation_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void UserInformation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

    }
}
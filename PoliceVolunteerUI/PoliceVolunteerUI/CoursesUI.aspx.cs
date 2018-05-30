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
    public partial class CoursesUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }

            //fill gridviews
            FillCourses();
            FillSignedCourses();
            FillValiditys();
        }

        protected void FillValiditys()
        {
            DataTable table = (new VolunteerBL(Session["User"].ToString())).GetValidities().Tables[0];
            table.Columns["VolunteerToValidity.ValidityCode"].ColumnName = "validityCode";
            DataView dataview = new DataView(table);
            validities.DataSource = dataview;
            validities.DataBind();
        }

        protected void FillCourses()
        {
            Queue<FieldValue<CourseField>> parameters = new Queue<FieldValue<CourseField>>();
            parameters.Enqueue(new FieldValue<CourseField>(CourseField.CourseDate, DateTime.Now, PoliceVolnteerDAL.Table.Course, FieldType.Date, OperatorType.Greater));
            //parameters.Enqueue(new FieldValue<CourseField>(CourseField.StartTime, DateTime.Now, PoliceVolnteerDAL.Table.Course, FieldType.Time, OperatorType.Greater));
            //get all courses
            DataSet courses = (new CoursesBL(parameters, true)).Courses;
            DataTable FilteredTable = courses.Tables[0];
            //filter by courses the user already registered to
            for (int i = 0; i < FilteredTable.Rows.Count; i++)
            {
                VolunteerBL user = new VolunteerBL(Session["User"].ToString());
                DataTable UserCourses = user.GetCourses(DateTime.Now, OperatorType.Greater);
                for (int j = 0; j < UserCourses.Rows.Count; j++)
                {
                    if (UserCourses.Rows[j]["CourseCode"].ToString() == FilteredTable.Rows[i]["CourseCode"].ToString())
                    {
                        FilteredTable.Rows[i].Delete();
                        //i--;
                        break;
                    }
                }
            }
            //bind data to gridview
            DataView dataView = new DataView(FilteredTable);
            CoursesInformation.DataSource = dataView;
            CoursesInformation.DataBind();
        }

        protected void FillSignedCourses()
        {
            //get all registered courses
            DataView dataView = new DataView((new VolunteerBL(Session["User"].ToString()).GetCourses(DateTime.Now, PoliceVolnteerDAL.OperatorType.Greater)));
            SignedActivitys.DataSource = dataView;
            SignedActivitys.DataBind();
        }

        protected void CourseSignUp(object sender, EventArgs e)
        {
            GridViewRow row = CoursesInformation.Rows[int.Parse(((Button)sender).CommandArgument)];
            VolunteerBL User = new VolunteerBL(Session["User"].ToString());
            User.CourseSignUp(int.Parse(((Label)row.Cells[0].FindControl("lblCourseCode")).Text));
        }

        protected void CourseSignOut(object sender, EventArgs e)
        {
            GridViewRow row = SignedActivitys.Rows[int.Parse(((Button)sender).CommandArgument)];
            VolunteerBL User = new VolunteerBL(Session["User"].ToString());
            User.CourseSignOut(int.Parse(((Label)row.Cells[0].FindControl("lblCourseCode")).Text));
        }

        protected void Validities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
                DataTable expiredValiditys = volunteer.GetValidities().Tables[0];
                foreach(DataRow validity in expiredValiditys.Rows)
                {
                    if (((Label)e.Row.Cells[0].FindControl("lblValidityCode")).Text == validity["VolunteerToValidity.ValidityCode"].ToString())
                    {
                        DateTime validityExpire = DateTime.Parse(validity["EndDate"].ToString());
                        DateTime current = DateTime.Now;
                        if ((current.Subtract(validityExpire)).TotalDays > -30)
                        {
                            e.Row.Font.Bold = false;
                            e.Row.BackColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
    }
}
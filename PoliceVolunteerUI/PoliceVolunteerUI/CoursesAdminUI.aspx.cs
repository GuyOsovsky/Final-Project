using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using PoliceVolnteerBL;
using PoliceVolnteerDAL;

namespace PoliceVolunteerUI
{
    public partial class CoursesAdminUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "" || !(new VolunteerBL(Session["User"].ToString()).Type.PermmisionCourse))
            {
                Response.Redirect("HomePageUI.aspx");
            }

            FillOtherCourses();
            FillOurCourses();
        }

        protected void FillOurCourses()
        {
            CoursesBL allCourses = new CoursesBL(DateTime.Now);

            DataView dataView = new DataView(allCourses.Courses.Tables[0]);
            CoursesInformation.DataSource = dataView;
            CoursesInformation.DataBind();

            //CoursesInformation.DataSource = dataView;
            //CoursesInformation.DataBind();

            ////get all courses
            //DataSet courses = (new CoursesBL()).Courses;
            ////filter by date
            //FieldValue<CourseField> Mask = new FieldValue<CourseField>(CourseField.CourseDate, DateTime.Now, PoliceVolnteerDAL.Table.Course, FieldType.Date, OperatorType.Greater);
            //courses.Tables[0].DefaultView.RowFilter = Mask.ToSql();
            //DataTable FilteredTable = (courses.Tables[0].DefaultView).ToTable();
            ////filter by courses the user already registered to
            //for (int i = 0; i < FilteredTable.Rows.Count; i++)
            //{
            //    VolunteerBL user = new VolunteerBL(Session["User"].ToString());
            //    DataTable UserCourses = user.GetCourses(DateTime.Now, OperatorType.Greater);
            //    for (int j = 0; j < UserCourses.Rows.Count; j++)
            //    {
            //        if (UserCourses.Rows[j]["CourseCode"].ToString() == FilteredTable.Rows[i]["CourseCode"].ToString())
            //        {
            //            FilteredTable.Rows[i].Delete();
            //            i--;
            //            break;
            //        }
            //    }
            //}
            ////bind data to gridview
            //DataView dataView = new DataView(FilteredTable);
            //CoursesInformation.DataSource = dataView;
            //CoursesInformation.DataBind();
        }

        protected void FillOtherCourses()
        {
            WebServiceReference.generalWS webService = new WebServiceReference.generalWS();
            OtherCourses.DataSource = webService.GetFutureCourses().Tables[0];
            OtherCourses.DataBind();
        }

        protected void AddOtherCourses(object sender, EventArgs e)
        {
            GridViewRow row = OtherCourses.Rows[int.Parse(((Button)sender).CommandArgument)];
            WebServiceReference.generalWS webService = new WebServiceReference.generalWS();
            WebServiceReference.CourseBL otherCourse = webService.GetCourse(int.Parse(((Label)row.Cells[0].FindControl("lblCourseCode")).Text));
            CourseBL course = new CourseBL(otherCourse.CourseName, otherCourse.CourseDate, otherCourse.StartTime, otherCourse.FinishTime, otherCourse.NameOfInstructor, otherCourse.IsRequeired, otherCourse.Place, otherCourse.Description, otherCourse.ValidityCode);
        }
    }
}
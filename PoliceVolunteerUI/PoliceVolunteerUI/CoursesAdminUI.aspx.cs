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
            //get all courses
            CoursesBL allCourses = new CoursesBL(DateTime.Now);
            allCourses.Courses.Tables[0].Rows.Add();
            allCourses.Courses.Tables[0].Columns.Add("Validity", typeof(string));

            for (int i = 0; i < allCourses.Courses.Tables[0].Rows.Count-1; i++)
            {
                allCourses.Courses.Tables[0].Rows[i]["Validity"] = new ValidityTypeBL(int.Parse(allCourses.Courses.Tables[0].Rows[i]["ValidityCode"].ToString())).ValidityName;
            }

            //bind data to gridview
            DataView dataView = new DataView(allCourses.Courses.Tables[0]);
            CoursesInformation.DataSource = dataView;
            CoursesInformation.EditIndex = allCourses.Courses.Tables[0].Rows.Count - 1;
            CoursesInformation.DataBind();
        }

        protected void AddNewCourse(object sender, EventArgs e)
        {
            GridViewRow row = CoursesInformation.Rows[CoursesInformation.EditIndex];
            string courseName = ((TextBox)row.Cells[1].FindControl("inputCourseName")).Text;
            DateTime courseDate = DateTime.ParseExact(((TextBox)row.Cells[2].FindControl("inputCourseDate")).Text, "yyyy-M-d", System.Globalization.CultureInfo.InvariantCulture);
            DateTime startTime = DateTime.ParseExact(((TextBox)row.Cells[3].FindControl("inputCourseStartTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            DateTime finishTime = DateTime.ParseExact(((TextBox)row.Cells[4].FindControl("inputCourseFinishTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            
            VolunteerBL instructor = new VolunteerBL(Session["User"].ToString());
            string nameOfInstructor = instructor.FName + " " + instructor.LName;
            
            string place = ((TextBox)row.Cells[5].FindControl("inputCoursePlace")).Text;
            string description = ((TextBox)row.Cells[6].FindControl("inputCourseDescription")).Text;
            ValidityTypeBL newValidity = new ValidityTypeBL(((DropDownList)row.Cells[7].FindControl("InputValidityType")).Text);
            CourseBL course = new CourseBL(courseName, courseDate, startTime, finishTime, nameOfInstructor, false, place, description, newValidity.ValidityCode);
        }

        //protected void AddNewActivity(object sender, EventArgs e)
        //{
        //    GridViewRow row = ActivitysInformation.Rows[ActivitysInformation.EditIndex];
        //    string activityName = ((TextBox)row.Cells[1].FindControl("InputActivityName")).Text;
        //    DateTime activityDate = DateTime.ParseExact(((TextBox)row.Cells[2].FindControl("InputActivityDate")).Text, "yyyy-M-d", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime activityStartTime = DateTime.ParseExact(((TextBox)row.Cells[3].FindControl("InputActivityStartTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture); ;
        //    DateTime activityFinishTime = DateTime.ParseExact(((TextBox)row.Cells[4].FindControl("InputActivityFinishTime")).Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture); ;
        //    string activityPlace = ((TextBox)row.Cells[5].FindControl("InputActivityPlace")).Text;
        //    string activityManeger = ((DropDownList)row.Cells[6].FindControl("InputActivityManeger")).SelectedValue.ToString();
        //    int activityMinParticipents = int.Parse(((TextBox)row.Cells[7].FindControl("InputActivityMinParticipents")).Text);
        //    int activityTypeCode = int.Parse(((DropDownList)row.Cells[8].FindControl("InputActivityTypeName")).SelectedValue.ToString());
        //    ActivityBL activity = new ActivityBL(activityName, activityDate, activityStartTime, activityFinishTime, activityManeger, activityTypeCode, activityPlace, activityMinParticipents);

        //}

        protected void FillValidityTypesList(object sender, EventArgs e)
        {
            ValidityTypesBL types = new ValidityTypesBL();
            for (int i = 0; i < types.ValidityTypeList.Count; i++)
            {
                ((DropDownList)sender).Items.Add(new ListItem(types.ValidityTypeList[i].ValidityName));
            }
            ((DropDownList)sender).DataBind();
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
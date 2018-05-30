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
            FillValiditys();
        }

        protected void FillValiditys()
        {
            VolunteersBL volunteers = new VolunteersBL(false);
            DataTable table = (volunteers.VolunteerList[0]).GetValidities().Tables[0];
            for (int i = 1; i < volunteers.VolunteerList.Count; i++)
            {
                table.Merge(volunteers.VolunteerList[i].GetValidities().Tables[0]);
            }
            //DataTable AllValidities = this.GetValidities().Tables[0];
            ////create mask to filter validities by todays date
            //FieldValue<VolunteerToValidityField> Mask = new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.EndDate, DateTime.Now.ToShortDateString(), Table.VolunteerToValidity, FieldType.Date, OperatorType.LowerAndEquals);
            ////filter unnececery validities
            //AllValidities.DefaultView.RowFilter = Mask.ToSql();
            //DataTable FilteredValidities = (AllValidities.DefaultView).ToTable();
            FieldValue<VolunteerToValidityField> Mask = new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.EndDate, DateTime.Now.AddDays(30), PoliceVolnteerDAL.Table.VolunteerToValidity, FieldType.Date, OperatorType.LowerAndEquals);
            table.DefaultView.RowFilter = Mask.ToSql();
            DataTable filteredTable = (table.DefaultView).ToTable();
            filteredTable.Columns["VolunteerToValidity.ValidityCode"].ColumnName = "validityCode";
            filteredTable.Columns.Add("FName", typeof(string));
            filteredTable.Columns.Add("LName", typeof(string));
            foreach (DataRow row in filteredTable.Rows)
            {
                VolunteerBL volunteer = new VolunteerBL(row["PhoneNumber"].ToString());
                row["FName"] = volunteer.FName;
                row["LName"] = volunteer.LName;
            }
            DataView dataview = new DataView(filteredTable);
            validities.DataSource = dataview;
            validities.DataBind();
        }

        protected void FillOurCourses()
        {
            //get all courses
            CoursesBL allCourses = new CoursesBL(DateTime.Now);
            allCourses.Courses.Tables[0].Rows.Add();
            allCourses.Courses.Tables[0].Columns.Add("Validity", typeof(string));

            for (int i = 0; i < allCourses.Courses.Tables[0].Rows.Count - 1; i++)
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
            VolunteersBL allVolunteers = new VolunteersBL(false);
            foreach (VolunteerBL volunteer in allVolunteers.VolunteerList)
            {
                DataTable validities = volunteer.GetValidities().Tables[0];
                bool isExists = false;
                foreach (DataRow validity in validities.Rows)
                {
                    if (newValidity.ValidityCode == int.Parse(validity["ValidityCode"].ToString()))
                    {
                        isExists = true;
                        DateTime validityExpire = DateTime.Parse(validity["EndDate"].ToString());
                        DateTime current = DateTime.Now;
                        if ((current.Subtract(validityExpire)).TotalDays > -30)
                        {
                            volunteer.CourseSignUp(course.CourseCode);
                            break;
                        }
                    }
                }
                if (!isExists)
                {
                    volunteer.CourseSignUp(course.CourseCode);
                }
            }
        }

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
            DataTable allCourses = (new CoursesBL(DateTime.Now)).Courses.Tables[0];
            for (int i = 0; i < allCourses.Rows.Count; i++)
            {
                CourseBL compare = new CourseBL(int.Parse(allCourses.Rows[i]["CourseCode"].ToString()));
                if (compare.Equals(course))
                {
                    course.DeleteCourse();
                    break;
                }
            }
        }

        protected void Validities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                VolunteerBL volunteer = new VolunteerBL(((Label)e.Row.Cells[1].FindControl("LblPhoneNumber")).Text);
                DataTable expiredValiditys = volunteer.GetValidities().Tables[0];
                foreach (DataRow validity in expiredValiditys.Rows)
                {
                    if (((Label)e.Row.Cells[0].FindControl("lblValidityCode")).Text == validity["VolunteerToValidity.ValidityCode"].ToString())
                    {
                        DateTime validityExpire = DateTime.Parse(validity["EndDate"].ToString());
                        DateTime current = DateTime.Now;
                        if ((current.Subtract(validityExpire)).TotalDays > 0)
                        {
                            e.Row.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                }
            }
        }
    }
}
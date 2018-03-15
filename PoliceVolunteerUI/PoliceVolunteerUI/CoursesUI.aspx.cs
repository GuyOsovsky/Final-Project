﻿using System;
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
        }

        protected void FillCourses()
        {
            //get all courses
            DataSet courses = (new CoursesBL()).Courses;
            //filter by date
            FieldValue<CourseField> Mask = new FieldValue<CourseField>(CourseField.CourseDate, DateTime.Now, PoliceVolnteerDAL.Table.Course, FieldType.Date, OperatorType.Greater);
            courses.Tables[0].DefaultView.RowFilter = Mask.ToSql();
            DataTable FilteredTable = (courses.Tables[0].DefaultView).ToTable();
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
                        i--;
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
    }
}
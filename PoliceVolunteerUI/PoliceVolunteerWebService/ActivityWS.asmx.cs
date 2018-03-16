using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using PoliceVolnteerBL;

namespace PoliceVolunteerWebService
{
    /// <summary>
    /// Summary description for ActivityWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ActivityWS : System.Web.Services.WebService
    {
        [WebMethod]
        public DataSet GetAllActivitys()
        {
            ActivitysBL activitys = new ActivitysBL();
            return activitys.Activitys;
        }

        [WebMethod]
        public DataSet GetActivitys(DateTime from, DateTime to)
        {
            ActivitysBL activitys = new ActivitysBL(from, to);
            return activitys.Activitys;
        }

        [WebMethod]
        public DataSet GetAllCourses()
        {
            CoursesBL courses = new CoursesBL();
            return courses.Courses;
        }

        [WebMethod]
        public DataSet GetCourses(DateTime from, DateTime to)
        {
            CoursesBL courses = new CoursesBL(from, to);
            return courses.Courses;
        }

        [WebMethod]
        public void AddVolunteer(string phoneNumber, string emergencyPhoneNumber, string fName, string lName, DateTime bDate, string userName, string password, string homeAddress, string homeCity, string emailAddress, string id, string policeID, string serveCity)
        {
            VolunteerBL volunteer = new VolunteerBL(phoneNumber, emergencyPhoneNumber, fName, lName, bDate, userName, password, homeAddress, homeCity, emailAddress, id, policeID, serveCity, (new VolunteerTypeBL("new")).TypeCode);
        }

        [WebMethod]
        public VolunteerBL GetVolunteer(string phoneNumber)
        {
            VolunteerBL volunteer = new VolunteerBL(phoneNumber);
            return volunteer;
        }
    }
}

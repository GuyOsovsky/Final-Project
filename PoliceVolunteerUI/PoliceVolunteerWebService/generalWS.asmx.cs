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
    /// Summary description for generalWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class generalWS : System.Web.Services.WebService
    {
        [WebMethod]
        public DataSet GetFutureActivitys()
        {
            ActivitysBL activitys = new ActivitysBL(DateTime.Now);
            return activitys.Activitys;
        }

        [WebMethod]
        public DataSet GetFutureCourses()
        {
            CoursesBL courses = new CoursesBL(DateTime.Now);
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

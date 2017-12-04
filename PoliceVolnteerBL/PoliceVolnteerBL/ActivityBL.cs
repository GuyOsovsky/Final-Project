using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceVolnteerDAL;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerBL
{
    public class ActivityBL
    {
        public int ActivityCode { get; set; }
        public string ActivityName { get; set; }
        public DateTime ActivityDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string ActivityManager { get; set; }
        public int TypeCode { get; set; }
        public string Place { get; set; }
        public int MinNumberOfVolunteer { get; set; }

        

        //build and adding
        public ActivityBL(string ActivityName, DateTime ActivityDate, DateTime StartTime, DateTime FinishTime, string ActivityManager, int TypeCode, string Place, int MinNumberOfVolunteer)
        {
            this.ActivityName = ActivityName;
            this.ActivityDate = ActivityDate;
            this.StartTime = StartTime;
            this.FinishTime = FinishTime;
            this.ActivityManager = ActivityManager;
            this.TypeCode = TypeCode;
            this.Place = Place;
            this.MinNumberOfVolunteer = MinNumberOfVolunteer;
            ActivityDAL.AddActivity(ActivityName, ActivityDate, StartTime, FinishTime, ActivityManager, TypeCode, Place, MinNumberOfVolunteer);
            this.ActivityCode = (int)ActivityDAL.GetTable().Tables[0].Rows[ActivityDAL.GetTable().Tables[0].Rows.Count - 1]["ActivityCode"];
        }

        //build from the database
        public ActivityBL(int activityCode)
        {
            this.ActivityCode = activityCode;
            DataSet ds = ActivityDAL.GetTable(new FieldValue<ActivityField>(ActivityField.ActivityCode, activityCode, FieldType.Number, OperatorType.Equals));
            this.ActivityName = (string)ds.Tables[0].Rows[0]["ActivityName"];
            this.ActivityDate = (DateTime)ds.Tables[0].Rows[0]["ActivityDate"];
            this.StartTime = (DateTime)ds.Tables[0].Rows[0]["StartTime"];
            this.FinishTime = (DateTime)ds.Tables[0].Rows[0]["FinishTime"];
            this.ActivityManager = (string)ds.Tables[0].Rows[0]["ActivityManager"];
            this.TypeCode = (int)ds.Tables[0].Rows[0]["TypeCode"];
            this.Place = (string)ds.Tables[0].Rows[0]["Place"];
            this.MinNumberOfVolunteer = (int)ds.Tables[0].Rows[0]["MinNumberOfVolunteer"];
        }

        public VolunteersBL GetAllVolunteers()
        {
            ReportsBL reports = new ReportsBL("", this.ActivityCode);
            VolunteersBL ret = new VolunteersBL(true);
            foreach (ReportBL report in reports.ReportList)
            {
                ret.AddVolunteer(new VolunteerBL(report.PhoneNumber));
            }
            return ret;
        }

        public ReportsBL GetAllReports()
        {
            return new ReportsBL("", this.ActivityCode);
        }

        public string GetActivityReport()
        {
            string report = "ACTIVITY REPORT\t\t\t\tcode: " + this.ActivityCode + "\n";
            report += "name: " + this.ActivityName + "\n";
            report += "date: " + this.ActivityDate + "\n";
            report += "start time: " + this.StartTime + "\n";
            report += "finish time: " + this.FinishTime + "\n";
            report += "manager: " + this.ActivityManager + "\n";
            report += "type: " + TypeToActivityDAL.GetTable(new FieldValue<TypeToActivityField>(TypeToActivityField.typeCode, this.TypeCode, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0]["typeName"].ToString() + "\n";
            report += "place: " + this.Place + "\n";
            report += "number of Participants: " + this.GetAllVolunteers().VolunteerList.Count.ToString() + "\n";
            return report;
        }

        public ReportsBL GetAllReports(string phoneNumber)
        {
            return new ReportsBL(phoneNumber , this.ActivityCode);
        }
    }
}

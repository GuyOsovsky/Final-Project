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
        public ActivityTypes TypeCode { get; set; }
        public string Place { get; set; }
        public int MinNumberOfVolunteer { get; set; }


        /// <summary>
        /// build and adding to database
        /// </summary>
        public ActivityBL(string ActivityName, DateTime ActivityDate, DateTime StartTime,
            DateTime FinishTime, string ActivityManager, int TypeCode, string Place, int MinNumberOfVolunteer)
        {
            this.ActivityName = ActivityName;
            this.ActivityDate = ActivityDate;
            this.StartTime = StartTime;
            this.FinishTime = FinishTime;
            this.ActivityManager = ActivityManager;
            this.TypeCode = new ActivityTypes(TypeCode);
            this.Place = Place;
            this.MinNumberOfVolunteer = MinNumberOfVolunteer;
            ActivityDAL.AddActivity(ActivityName, ActivityDate, StartTime, FinishTime, ActivityManager, TypeCode, Place, MinNumberOfVolunteer);
            this.ActivityCode = (int)ActivityDAL.GetTable().Tables[0].Rows[ActivityDAL.GetTable().Tables[0].Rows.Count - 1]["ActivityCode"];
        }

        /// <summary>
        /// build from the database
        /// </summary>
        public ActivityBL(int activityCode)
        {
            this.ActivityCode = activityCode;
            DataSet activityDataSet = ActivityDAL.GetTable(new FieldValue<ActivityField>(ActivityField.ActivityCode, activityCode, Table.Activity, FieldType.Number, OperatorType.Equals));
            this.ActivityName = (string)activityDataSet.Tables[0].Rows[0]["ActivityName"];
            this.ActivityDate = (DateTime)activityDataSet.Tables[0].Rows[0]["ActivityDate"];
            this.StartTime = (DateTime)activityDataSet.Tables[0].Rows[0]["StartTime"];
            this.FinishTime = (DateTime)activityDataSet.Tables[0].Rows[0]["FinishTime"];
            this.ActivityManager = (string)activityDataSet.Tables[0].Rows[0]["ActivityManager"];
            this.TypeCode = new ActivityTypes((int)activityDataSet.Tables[0].Rows[0]["Activity.TypeCode"]);
            this.Place = (string)activityDataSet.Tables[0].Rows[0]["Place"];
            this.MinNumberOfVolunteer = (int)activityDataSet.Tables[0].Rows[0]["MinNumberOfVolunteer"];
        }


        /// <summary>
        /// return VolunteersBL object with all the Volunteers in this specific activity
        /// </summary>
        public VolunteersBL GetAllVolunteers()
        {
            ReportsBL reports = new ReportsBL("", this.ActivityCode);
            VolunteersBL ret = new VolunteersBL(true);
            foreach (DataRow report in reports.Reports.Tables[0].Rows)
            {
                ret.AddVolunteer(new VolunteerBL(report["PhoneNumber"].ToString()));
            }
            return ret;
        }

        /// <summary>
        /// return textual report of this specific activiy
        /// </summary>
        public string GetActivityReport()
        {
            string report = "ACTIVITY REPORT\t\t\t\tcode: " + this.ActivityCode + "\n";
            report += "name: " + this.ActivityName + "\n";
            report += "date: " + this.ActivityDate + "\n";
            report += "start time: " + this.StartTime + "\n";
            report += "finish time: " + this.FinishTime + "\n";
            report += "manager: " + this.ActivityManager + "\n";
            report += "type: " + TypeToActivityDAL.GetTable(new FieldValue<TypeToActivityField>(TypeToActivityField.typeCode, this.TypeCode, Table.TypeToActivity, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0]["typeName"].ToString() + "\n";
            report += "place: " + this.Place + "\n";
            report += "number of Participants: " + this.GetAllVolunteers().VolunteerList.Count.ToString() + "\n";
            return report;
        }

        /// <summary>
        /// retrun all the reports from this specific activity
        /// </summary>
        public ReportsBL GetAllReports(string phoneNumber = "")
        {
            return new ReportsBL(phoneNumber , this.ActivityCode);
        }
    }
}

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
        public ActivityBL(int ActivityCode)
        {
            this.ActivityCode = ActivityCode;
            DataSet ds = ActivityDAL.GetTable(new FieldValue<ActivityField>(ActivityField.ActivityCode, ActivityCode.ToString(), FieldType.Number));
            this.ActivityName = (string)ds.Tables[0].Rows[0]["ActivityName"];
            this.ActivityDate = (DateTime)ds.Tables[0].Rows[0]["ActivityDate"];
            this.StartTime = (DateTime)ds.Tables[0].Rows[0]["StartTime"];
            this.FinishTime = (DateTime)ds.Tables[0].Rows[0]["FinishTime"];
            this.ActivityManager = (string)ds.Tables[0].Rows[0]["ActivityManager"];
            this.TypeCode = (int)ds.Tables[0].Rows[0]["TypeCode"];
            this.Place = (string)ds.Tables[0].Rows[0]["Place"];
            this.MinNumberOfVolunteer = (int)ds.Tables[0].Rows[0]["MinNumberOfVolunteer"];
        }

    }
}

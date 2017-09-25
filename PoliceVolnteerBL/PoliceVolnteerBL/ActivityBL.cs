using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PoliceVolnteerDAL;

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
        public int MinNumberOfVolnteer { get; set; }

        //int ActivityCode,        string ActivityName,        DateTime ActivityDate,        DateTime StartTime,        DateTime FinishTime,        string ActivityManager ,        int TypeCode,        string Place,        int MinNumberOfVolnteer

        public ActivityBL(/*int ActivityCode, */string ActivityName, DateTime ActivityDate, DateTime StartTime, DateTime FinishTime, string ActivityManager, int TypeCode, string Place, int MinNumberOfVolnteer)
        {
            this.ActivityName = ActivityName;
            this.ActivityDate = ActivityDate;
            this.StartTime = StartTime;
            this.FinishTime = FinishTime;
            this.ActivityManager = ActivityManager;
            this.TypeCode = TypeCode;
            this.Place = Place;
            this.MinNumberOfVolnteer = MinNumberOfVolnteer;
            ActivityDAL.AddActivity(ActivityName, ActivityDate, StartTime, FinishTime, ActivityManager, TypeCode, Place, MinNumberOfVolnteer);
            //this.ActivityCode = ActivityCode; //להכניס לתכונה קוד חדש שנוצר מפעולה קודמת!
        }

        
    }
}

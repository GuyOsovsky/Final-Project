﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceVolnteerDAL;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerBL
{
    public class ActivitysBL
    {
        public List<ActivityBL> ActivityList { get; private set; }

        public static DataSet GetActivities()
        {
            return ActivityDAL.GetTable();
        }

        //create ActivityList and add ActivityBL objects that were in a period of time
        public ActivitysBL(DateTime from = new DateTime(), DateTime to = new DateTime())
        {
            if (to.Year == 1)
                to = DateTime.Now;
            this.ActivityList = new List<ActivityBL>();
            DataRowCollection activityRow = ActivityDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < activityRow.Count; i++)
            {
                ActivityBL activity = new ActivityBL((int)activityRow[i]["ActivityCode"]);
                if(activity.ActivityDate >= from && activity.ActivityDate <= to)
                    ActivityList.Add(activity);
            }
        }

        //retrun sum of activities that were in a period of time
        public int SumOfActivities(DateTime from, DateTime to)
        {
            int count = 0;
            for (int i = 0; i < this.ActivityList.Count; i++)
            {
                if (ActivityList[i].ActivityDate >= from && ActivityList[i].ActivityDate <= to)
                    count++;
            }
            return count;
        }


    }
}
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
    public class ActivitysBL
    {
        public List<ActivityBL> ActivityList { get; private set; }

        public ActivitysBL(DateTime from = new DateTime(), DateTime to = new DateTime())
        {
            if (to.Year == 1)
                to = DateTime.Now;
            this.ActivityList = new List<ActivityBL>();
            DataRowCollection drc = ActivityDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ActivityBL activity = new ActivityBL((int)drc[i]["ActivityCode"]);
                if(activity.ActivityDate >= from && activity.ActivityDate <= to)
                    ActivityList.Add(activity);
            }
        }

        public int CountOfActivities(DateTime from, DateTime to)
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

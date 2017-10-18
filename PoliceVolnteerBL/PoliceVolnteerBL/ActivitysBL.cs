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
        public List<ActivityBL> ActivityList { get; set; }

        public ActivitysBL()
        {
            this.ActivityList = new List<ActivityBL>();
            DataRowCollection drc = ActivityDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ActivityList.Add(new ActivityBL((int)drc[i]["ActivityCode"]));
            }
        }
    }
}

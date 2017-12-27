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
    public class MediasBL
    {
        public List<MediaBL> MediaList { get; set; }

        public MediasBL()
        {
            MediaList = new List<MediaBL>();
            DataRowCollection drc = MediaDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                MediaList.Add(new MediaBL((string)drc[i]["FileName"]));
            }
        }

        public MediasBL(DateTime from, DateTime to)
        {
            MediaList = new List<MediaBL>();
            DataRowCollection drc = MediaDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                int activityCode = (int)(drc[i]["ActivityCode"]);
                ActivityBL activity = new ActivityBL(activityCode);
                if (activity.ActivityDate >= from && activity.ActivityDate <= to)
                    MediaList.Add(new MediaBL((string)drc[i]["FileName"]));
            }
        }

        public MediasBL(int fileType)
        {
            MediaList = new List<MediaBL>();
            DataRowCollection drc = MediaDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                if (((int)drc[i]["FileType"]) == fileType)
                    MediaList.Add(new MediaBL((string)drc[i]["FileName"]));
            }
        }
    }
}

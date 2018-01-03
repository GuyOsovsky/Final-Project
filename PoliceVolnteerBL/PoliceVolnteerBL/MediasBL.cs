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

        //create MediaList and add MediaBL objects that were in a period of time
        public MediasBL(DateTime from = new DateTime(), DateTime to = new DateTime())
        {
            if (to.Year == 1)
                to = DateTime.Now;
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

        //return all file by file type(format - txt,mp3...)
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

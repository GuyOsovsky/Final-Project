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

        /// <summary>
        /// create MediaList and add MediaBL objects that were in a period of time
        /// </summary>
        public MediasBL(DateTime from = new DateTime(), DateTime to = new DateTime())
        {
            if (to.Year == 1)
                to = DateTime.Now;
            MediaList = new List<MediaBL>();
            DataRowCollection mediaRows = MediaDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < mediaRows.Count; i++)
            {
                int activityCode = (int)(mediaRows[i]["ActivityCode"]);
                ActivityBL activity = new ActivityBL(activityCode);
                if (activity.ActivityDate >= from && activity.ActivityDate <= to)
                    MediaList.Add(new MediaBL((string)mediaRows[i]["FileName"]));
            }
        }

        /// <summary>
        /// return all file by file type(format - txt,mp3...)
        /// </summary>
        public MediasBL(int fileType)
        {
            MediaList = new List<MediaBL>();
            DataRowCollection mediaRows = MediaDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < mediaRows.Count; i++)
            {
                if (((int)mediaRows[i]["FileType"]) == fileType)
                    MediaList.Add(new MediaBL((string)mediaRows[i]["FileName"]));
            }
        }
    }
}

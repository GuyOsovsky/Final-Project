using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PoliceVolnteerDAL;

namespace PoliceVolnteerBL
{
    public class ActivitysTypes
    {
        public DataSet activityTypes { get; set; }

        /// <summary>
        /// creates a new object with all the types
        /// </summary>
        public ActivitysTypes()
        {
            activityTypes = TypeToActivityDAL.GetTable();
        }
    }
}

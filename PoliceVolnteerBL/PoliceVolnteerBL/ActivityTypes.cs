using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PoliceVolnteerDAL;

namespace PoliceVolnteerBL
{
    public class ActivityTypes
    {
        public DataSet activityTypes { get; set; }

        public ActivityTypes()
        {
            activityTypes = TypeToActivityDAL.GetTable();
        }
    }
}

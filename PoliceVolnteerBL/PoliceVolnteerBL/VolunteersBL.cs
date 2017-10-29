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
    public class VolunteersBL
    {
        public List<VolunteerBL> VolunteerList { get; set; }

        public VolunteersBL()
        {
            this.VolunteerList = new List<VolunteerBL>();
            DataRowCollection drc = VolunteerInfoDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                VolunteerList.Add(new VolunteerBL(drc[i]["PhoneNumber"].ToString()));
            }
        }

    }
}

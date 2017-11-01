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
    public class VolunteerToValiditysBL
    {
        public List<VolunteerToValidityBL> VolunteerToValidityList { get; set; }

        public VolunteerToValiditysBL()
        {
            this.VolunteerToValidityList = new List<VolunteerToValidityBL>();
            DataRowCollection drc = VolunteerToValidityDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                VolunteerToValidityList.Add(new VolunteerToValidityBL((string)drc[i]["PhoneNumber"], (int)drc[i]["ValidityCode"]));
            }
        }
    }
}

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
    public class VolunteerTypesBL
    {
        public List<VolunteerTypeBL> VolunteerTypeList { get; set; }

        public VolunteerTypesBL()
        {
            this.VolunteerTypeList = new List<VolunteerTypeBL>();
            DataRowCollection drc = VolunteerTypesDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                VolunteerTypeList.Add(new VolunteerTypeBL((int)drc[i]["TypeCode"]));
            }
        }

        public DataTable GetAllPermmisions()
        {
            DataTable allPermmisions = VolunteerTypesDAL.GetTable().Tables[0];
            allPermmisions.Columns.Remove("TypeCode");
            return allPermmisions;
        }
    }
}

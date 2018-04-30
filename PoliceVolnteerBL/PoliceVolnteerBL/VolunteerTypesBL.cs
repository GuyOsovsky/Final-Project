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

        /// <summary>
        /// create VolunteerTypeList and add all the VolunteerTypeBL objects
        /// </summary>
        public VolunteerTypesBL()
        {
            this.VolunteerTypeList = new List<VolunteerTypeBL>();
            DataRowCollection volunteerTypesRows = VolunteerTypesDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < volunteerTypesRows.Count; i++)
            {
                VolunteerTypeList.Add(new VolunteerTypeBL((int)volunteerTypesRows[i]["TypeCode"]));
            }
        }

        /// <summary>
        /// return all the permmisions that exist in database
        /// </summary>
        public DataTable GetAllPermmisions()
        {
            DataTable allPermmisions = VolunteerTypesDAL.GetTable().Tables[0];
            allPermmisions.Columns.Remove("TypeCode");
            return allPermmisions;
        }
    }
}

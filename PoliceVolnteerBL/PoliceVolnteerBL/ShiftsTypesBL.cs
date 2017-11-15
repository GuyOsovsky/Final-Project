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
    public class ShiftsTypesBL
    {
        public List<ShiftTypesBL> ShiftTypeList { get; private set; }

        public ShiftsTypesBL()
        {
            this.ShiftTypeList = new List<ShiftTypesBL>();
            DataRowCollection drc = ShiftsTypesDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ShiftTypeList.Add(new ShiftTypesBL((int)drc[i]["typeCode"]));
            }
        }

        public DataTable GetTypes()
        {
            DataTable Types = ShiftsTypesDAL.GetTable().Tables[0];
            Types.Columns.Remove("typeCode");
            return Types;
        }
    }
}

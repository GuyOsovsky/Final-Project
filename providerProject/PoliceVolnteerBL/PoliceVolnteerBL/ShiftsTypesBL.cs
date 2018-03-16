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

        //create ShiftTypeList and add all the ShiftTypesBL objects
        public ShiftsTypesBL()
        {
            this.ShiftTypeList = new List<ShiftTypesBL>();
            DataRowCollection shiftsTypesRows = ShiftsTypesDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < shiftsTypesRows.Count; i++)
            {
                ShiftTypeList.Add(new ShiftTypesBL((int)shiftsTypesRows[i]["typeCode"]));
            }
        }

        //return all shift types
        public DataTable GetTypes()
        {
            DataTable Types = ShiftsTypesDAL.GetTable().Tables[0];
            Types.Columns.Remove("typeCode");
            return Types;
        }
    }
}

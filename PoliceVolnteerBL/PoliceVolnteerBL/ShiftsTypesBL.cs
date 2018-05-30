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
        //public List<ShiftTypesBL> ShiftTypeList { get; private set; }

        public DataSet shiftTypes { get; set; }

        /// <summary>
        /// creates a new object with all the types
        /// </summary>
        public ShiftsTypesBL()
        {
            shiftTypes = ShiftsTypesDAL.GetTable();
        }

        ///// <summary>
        ///// create ShiftTypeList and add all the ShiftTypesBL objects
        ///// </summary>
        //public ShiftsTypesBL()
        //{
        //    this.ShiftTypeList = new List<ShiftTypesBL>();
        //    DataRowCollection shiftsTypesRows = ShiftsTypesDAL.GetTable().Tables[0].Rows;
        //    for (int i = 0; i < shiftsTypesRows.Count; i++)
        //    {
        //        ShiftTypeList.Add(new ShiftTypesBL((int)shiftsTypesRows[i]["typeCode"]));
        //    }
        //}

        ///// <summary>
        ///// return all shift types
        ///// </summary>
        //public DataTable GetTypes()
        //{
        //    DataTable Types = ShiftsTypesDAL.GetTable().Tables[0];
        //    Types.Columns.Remove("typeCode");
        //    return Types;
        //}
    }
}

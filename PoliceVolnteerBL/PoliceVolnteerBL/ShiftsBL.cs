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
    public class ShiftsBL
    {
        public List<ShiftBL> ShiftList { get; set; }

        public ShiftsBL()
        {
            this.ShiftList = new List<ShiftBL>();
            DataRowCollection drc = ShiftsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ShiftList.Add(new ShiftBL((int)drc[i]["ShiftCode"]));
            }
        }

        /// <summary>
        /// return all the information of all the shifts that will be done
        /// </summary>
        /// <returns></returns>
        public DataTable GetFutureShiftsInfo()
        {
            DataTable infoTable = ShiftsDAL.GetTable().Tables[0];
            int length = infoTable.Rows.Count;
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                if (((DateTime)infoTable.Rows[index]["StartTime"]).CompareTo(DateTime.Now) == -1)
                    infoTable.Rows.Remove(infoTable.Rows[index]);
                else
                    index++;
            }
            infoTable.Columns.Remove("ShiftCode");
            infoTable.Columns.Remove("TypeCode");
            return infoTable;
        }

        /// <summary>
        /// return all the shifts important information 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllShiftsInfo()
        {
            DataTable infoTable = ShiftsDAL.GetTable().Tables[0];
            infoTable.Columns.Remove("ShiftCode");
            infoTable.Columns.Remove("TypeCode");
            return infoTable;
        }
    }
}

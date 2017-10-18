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
    }
}

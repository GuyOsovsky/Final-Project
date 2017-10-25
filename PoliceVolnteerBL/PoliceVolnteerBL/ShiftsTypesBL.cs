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
        public List<ShiftTypeBL> ShiftTypeList { get; set; }

        public ShiftsTypesBL()
        {
            this.ShiftTypeList = new List<ShiftTypeBL>();
            DataRowCollection drc = ShiftsTypesDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ShiftTypeList.Add(new ShiftTypeBL((int)drc[i]["typeCode"]));
            }
        }
    }
}

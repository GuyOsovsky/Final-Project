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
        public List<ShiftBL> ShiftList { get; private set; }

        public ShiftsBL()
        {
            this.ShiftList = new List<ShiftBL>();
            DataRowCollection drc = ShiftsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ShiftList.Add(new ShiftBL((int)drc[i]["ShiftCode"]));
            }
        }

        public ShiftsBL(DateTime fromDate)
        {
            this.ShiftList = new List<ShiftBL>();
            DataTable ShiftsTable = ShiftsDAL.GetTable().Tables[0];
            FieldValue<ShiftsField> filter = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, fromDate, FieldType.Date, OperatorType.GreaterAndEquals);
            ShiftsTable.DefaultView.RowFilter = filter.ToString();
            ShiftsTable = (ShiftsTable.DefaultView).ToTable();
            DataRowCollection drc = ShiftsTable.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                ShiftList.Add(new ShiftBL((int)drc[i]["ShiftCode"]));
            }
        }

        public DataTable GetDetails()
        {
            DataTable shifts = ShiftsDAL.GetTable().Tables[0];
            shifts.Columns.Add("ShiftType", typeof(string));
            foreach (DataRow shift in shifts.Rows)
            {
                shift["ShiftType"] = (new ShiftTypesBL(int.Parse(shift["TypeCode"].ToString()))).TypeName;
            }
            shifts.Columns.Remove("ShiftCode");
            shifts.Columns.Remove("TypeCode");
            return shifts;
        }
    }
}

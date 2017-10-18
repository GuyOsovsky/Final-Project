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
    public class ShiftBL
    {
        public int ShiftCode { get; set; }
        public int TypeCode { get; set; }
        public DateTime DateOfShift { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Place { get; set; }

        public ShiftBL(int TypeCode, DateTime DateOfShif, DateTime StartTime, DateTime FinishTime, string Place)
        {
            this.TypeCode = TypeCode;
            this.DateOfShift = DateOfShift;
            this.StartTime = StartTime;
            this.FinishTime = FinishTime;
            this.Place = Place;
            ShiftsDAL.AddShift(TypeCode, DateOfShif, StartTime, FinishTime, Place);
            this.ShiftCode = (int)ShiftsDAL.GetTable().Tables[0].Rows[ShiftsDAL.GetTable().Tables[0].Rows.Count - 1]["ShiftCode"];
        }

        public ShiftBL(int ShiftCode)
        {
            this.ShiftCode = ShiftCode;
            DataSet ds = ShiftsDAL.GetTable(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, ShiftCode.ToString(), 1));
            this.TypeCode = (int)ds.Tables[0].Rows[0]["TypeCode"];
            this.DateOfShift = (DateTime)ds.Tables[0].Rows[0]["DateOfShift"];
            this.StartTime = (DateTime)ds.Tables[0].Rows[0]["StartTime"];
            this.FinishTime = (DateTime)ds.Tables[0].Rows[0]["FinishTime"];
            this.Place = (string)ds.Tables[0].Rows[0]["Place"];
        }
    }
}

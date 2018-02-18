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

        //create ShiftList and add all the ShiftBL objects 
        public ShiftsBL()
        {
            this.ShiftList = new List<ShiftBL>();
            DataRowCollection shiftsRows = ShiftsDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < shiftsRows.Count; i++)
            {
                ShiftList.Add(new ShiftBL((int)shiftsRows[i]["ShiftCode"]));
            }
        }

        //create ShiftList and add ShiftBL objects by "start date time"
        public ShiftsBL(DateTime fromDate)
        {
            //create the table
            this.ShiftList = new List<ShiftBL>();
            DataTable ShiftsTable = ShiftsDAL.GetTable().Tables[0];
            //filter out unwanted shifts(by date)
            FieldValue<ShiftsField> filter = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, fromDate, FieldType.Date, OperatorType.GreaterAndEquals);
            ShiftsTable.DefaultView.RowFilter = filter.ToString();
            ShiftsTable = (ShiftsTable.DefaultView).ToTable();

            DataRowCollection shiftsRows = ShiftsTable.Rows;
            for (int i = 0; i < shiftsRows.Count; i++)
            {
                ShiftList.Add(new ShiftBL((int)shiftsRows[i]["ShiftCode"]));
            }
        }

        //return table of the important details of all the shifts + shift type
        public DataTable GetDetails()
        {
            DataTable shifts = ShiftsDAL.GetTable().Tables[0];
            //adding ShiftType column to table
            shifts.Columns.Add("ShiftType", typeof(string));
            foreach (DataRow shift in shifts.Rows)
            {
                shift["ShiftType"] = (new ShiftTypesBL(int.Parse(shift["TypeCode"].ToString()))).TypeName;
            }
            //remove not necessary colomns
            shifts.Columns.Remove("ShiftCode");
            shifts.Columns.Remove("TypeCode");
            return shifts;
        }
    }
}

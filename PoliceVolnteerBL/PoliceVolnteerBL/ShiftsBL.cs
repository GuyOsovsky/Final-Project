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
        public DataSet shifts { get; private set; }

        /// <summary>
        /// create ShiftList and add all the ShiftBL objects 
        /// </summary>
        public ShiftsBL()
        {
            this.shifts = ShiftsDAL.GetTable();
        }

        /// <summary>
        /// create ShiftList and add ShiftBL objects by "start date time"
        /// </summary>
        public ShiftsBL(DateTime fromDate)
        {
            this.shifts = ShiftsDAL.GetTable(new FieldValue<ShiftsField>(ShiftsField.DateOfShift, fromDate, Table.Shifts, FieldType.Date, OperatorType.GreaterAndEquals));
        }

        /// <summary>
        /// create ShiftList and add ShiftBL objects by "start date time" and "finish date"
        /// </summary>
        public ShiftsBL(DateTime fromDate, DateTime toDate)
        {
            Queue<FieldValue<ShiftsField>> parameters = new Queue<FieldValue<ShiftsField>>();
            parameters.Enqueue(new FieldValue<ShiftsField>(ShiftsField.DateOfShift, fromDate, Table.Shifts, FieldType.Date, OperatorType.GreaterAndEquals));
            parameters.Enqueue(new FieldValue<ShiftsField>(ShiftsField.DateOfShift, toDate, Table.Shifts, FieldType.Date, OperatorType.Lower));
            this.shifts = ShiftsDAL.GetTable(parameters, true);
        }

        /// <summary>
        /// creates ShiftList and add ShiftBL objects by specific parameters
        /// </summary>
        public ShiftsBL(Queue<FieldValue<ShiftsField>> parameters, bool operation)
        {
            this.shifts = ShiftsDAL.GetTable(parameters, operation);
        }

        /// <summary>
        /// return table of the important details of all the shifts + shift type
        /// </summary>
        public DataSet GetDetails()
        {
            shifts.Tables[0].Columns.Add("ShiftType", typeof(string));
            foreach (DataRow shift in shifts.Tables[0].Rows)
            {
                shift["ShiftType"] = (new ShiftTypesBL(int.Parse(shift["TypeCode"].ToString()))).TypeName;
            }
            //remove not necessary colomns
            //shifts.Columns.Remove("ShiftCode");
            shifts.Tables[0].Columns.Remove("TypeCode");
            return shifts;
        }
    }
}

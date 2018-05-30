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
        public ShiftTypesBL TypeCode { get; set; }
        public DateTime DateOfShift { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Place { get; set; }

        /// <summary>
        /// build and adding to database
        /// </summary>
        public ShiftBL(int TypeCode, DateTime DateOfShif, DateTime StartTime, DateTime FinishTime, string Place)
        {
            this.TypeCode = new ShiftTypesBL(TypeCode);
            this.DateOfShift = DateOfShift;
            this.StartTime = StartTime;
            this.FinishTime = FinishTime;
            this.Place = Place;
            ShiftsDAL.AddShift(TypeCode, DateOfShif, StartTime, FinishTime, Place);
            this.ShiftCode = (int)ShiftsDAL.GetTable().Tables[0].Rows[ShiftsDAL.GetTable().Tables[0].Rows.Count - 1]["ShiftCode"];
        }

        /// <summary>
        /// build from the database
        /// </summary>
        public ShiftBL(int ShiftCode)
        {
            this.ShiftCode = ShiftCode;
            DataSet ShiftsDataSet = ShiftsDAL.GetTable(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, ShiftCode, Table.Shifts, FieldType.Number, OperatorType.Equals));
            this.TypeCode = new ShiftTypesBL((int)ShiftsDataSet.Tables[0].Rows[0]["TypeCode"]);
            this.DateOfShift = (DateTime)ShiftsDataSet.Tables[0].Rows[0]["DateOfShift"];
            this.StartTime = (DateTime)ShiftsDataSet.Tables[0].Rows[0]["StartTime"];
            this.FinishTime = (DateTime)ShiftsDataSet.Tables[0].Rows[0]["FinishTime"];
            this.Place = (string)ShiftsDataSet.Tables[0].Rows[0]["Place"];
        }

        /// <summary>
        /// return the period of shift time in hours
        /// </summary>
        public double GetShiftTotalHours()
        {
            return (FinishTime - StartTime).TotalHours;
        }

        /// <summary>
        /// return table of all the phone numbers of the participants in this specific shift
        /// </summary>
        public DataTable GetParticipantsPhoneNumbers()
        {
            DataTable shiftsToVolunteer = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode.ToString(), Table.ShiftsToVolunteer, FieldType.Number, OperatorType.Equals)).Tables[0];
            shiftsToVolunteer.Columns.Remove("shiftCode");
            shiftsToVolunteer.Columns.Remove("comments");
            return shiftsToVolunteer;
        }
    }
}

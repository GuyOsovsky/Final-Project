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
    public class ShiftToVolunteerBL
    {
        public int ShiftCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }

        public ShiftToVolunteerBL(int ShiftCode, string PhoneNumber, string Comments)
        {
            this.ShiftCode = ShiftCode;
            this.PhoneNumber = PhoneNumber;
            this.Comments = Comments;
            ShiftsToVolunteerDAL.AddShift(ShiftCode, PhoneNumber, Comments);
        }

        public ShiftToVolunteerBL(int ShiftCode, string PhoneNumber)
        {
            this.ShiftCode = ShiftCode;
            this.PhoneNumber = PhoneNumber;
            Queue<FieldValue<ShiftsToVolunteerField>> searchParams = new Queue<FieldValue<ShiftsToVolunteerField>>();
            searchParams.Enqueue(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber,PhoneNumber, FieldType.String, OperatorType.Equals));
            searchParams.Enqueue(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode.ToString(), FieldType.Number, OperatorType.Equals));
            DataSet ds = ShiftsToVolunteerDAL.GetTable(searchParams, true);
            this.Comments = (string)ds.Tables[0].Rows[0]["Comments"];
        }
    }
}

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
    public class VolunteerToValidityBL
    {
        public string PhoneNumber { get; set; }
        public int ValidityCode { get; set; }
        public bool Status { get; set; }
        public DateTime EndDate { get; set; }

        public VolunteerToValidityBL(string PhoneNumber, int ValidityCode, DateTime EndDate)
        {
            this.PhoneNumber = PhoneNumber;
            this.ValidityCode = ValidityCode;
            this.EndDate = EndDate;
            Status = true;
            VolunteerToValidityDAL.Add(PhoneNumber, ValidityCode, EndDate);
        }

        public VolunteerToValidityBL(string PhoneNumber, int ValidityCode)
        {
            Queue<FieldValue<VolunteerToValidityField>> searchParams = new Queue<FieldValue<VolunteerToValidityField>>();
            searchParams.Enqueue(new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.PhoneNumber, PhoneNumber, FieldType.String));
            searchParams.Enqueue(new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.ValidityCode, ValidityCode.ToString(), FieldType.Number));
            DataRow dr = VolunteerToValidityDAL.GetTable(searchParams, true).Tables[0].Rows[0];
            this.Status = (bool)dr["Status"];
            this.EndDate = (DateTime)dr["EndDate"];
            this.ValidityCode = ValidityCode;
            this.PhoneNumber = PhoneNumber;
        }

        public System.TimeSpan TimeToValidityEnd()
        {
            if (EndDate.Subtract(DateTime.Now).ToString()[0] == '-')
                return new TimeSpan(0, 0, 0, 0, 0);
            return EndDate.Subtract(DateTime.Now);
        }

        /*
         * System.DateTime date1 = new System.DateTime(1996, 6, 3, 22, 15, 0);
System.DateTime date2 = new System.DateTime(1996, 12, 6, 13, 2, 0);
System.DateTime date3 = new System.DateTime(1996, 10, 12, 8, 42, 0);

// diff1 gets 185 days, 14 hours, and 47 minutes.
System.TimeSpan diff1 = date2.Subtract(date1);

// date4 gets 4/9/1996 5:55:00 PM.
System.DateTime date4 = date3.Subtract(diff1);

// diff2 gets 55 days 4 hours and 20 minutes.
System.TimeSpan diff2 = date2 - date3;

// date5 gets 4/9/1996 5:55:00 PM.
System.DateTime date5 = date1 - diff2;
         */
    }
}

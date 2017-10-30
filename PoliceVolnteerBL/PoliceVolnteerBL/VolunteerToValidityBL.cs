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
        }

    }
}

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
    public class CoursesToVolunteerBL
    {
        public string PhoneNumber { get; set; }
        public int CourseCode { get; set; }
        public bool Status { get; set; }

        public CoursesToVolunteerBL(string PhoneNumber, int CourseCode, bool isNew)
        {
            this.PhoneNumber = PhoneNumber;
            this.CourseCode = CourseCode;
            if (isNew)
            {
                this.Status = false;
                CoursesToVolunteerDAL.AddCoursesToVolunteer(PhoneNumber, CourseCode);
            }
            else
            {
                Queue<FieldValue<CoursesToVolunteerField>> searchParams = new Queue<FieldValue<CoursesToVolunteerField>>();
                searchParams.Enqueue(new FieldValue<CoursesToVolunteerField>(CoursesToVolunteerField.PhoneNumber, PhoneNumber, FieldType.String));
                searchParams.Enqueue(new FieldValue<CoursesToVolunteerField>(CoursesToVolunteerField.CourseCode, CourseCode.ToString(), FieldType.Number));
                DataSet ds = CoursesToVolunteerDAL.GetTable(searchParams, true);
                this.Status = (bool)ds.Tables[0].Rows[0]["Status"];
            }
        }

    }
}

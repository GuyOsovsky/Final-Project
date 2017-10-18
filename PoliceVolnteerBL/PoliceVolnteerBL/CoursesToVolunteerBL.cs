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
                Queue<FieldValue<CoursesToVolunteerField>> qfvctvf = new Queue<FieldValue<CoursesToVolunteerField>>();
                qfvctvf.Enqueue(new FieldValue<CoursesToVolunteerField>(CoursesToVolunteerField.PhoneNumber, PhoneNumber, 2));
                qfvctvf.Enqueue(new FieldValue<CoursesToVolunteerField>(CoursesToVolunteerField.CourseCode, CourseCode.ToString(), 1));
                DataSet ds = CoursesToVolunteerDAL.GetTable(qfvctvf, true);
                this.Status = (bool)ds.Tables[0].Rows[0]["Status"];
            }
        }

    }
}

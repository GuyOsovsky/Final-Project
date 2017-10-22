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
    public class Program
    {
        static void Main(string[] args)
        {
            //יוצר משתמש רגיל בלי משתמש משטרה! לתקן!
            Console.WriteLine(VolunteerInfoDAL.AddVolunteer("t", "a", "a", "a", new DateTime(), "b", "b", "b", "b", "b", "b", "b", "b", new DateTime(), 1, "1"));

            /*CoursesToVolunteersBL ctvsbl = new CoursesToVolunteersBL();
            foreach (CoursesToVolunteerBL ctvbl in ctvsbl.CoursesToVolunteerList)
            {
                Console.WriteLine(ctvbl.PhoneNumber + " " + ctvbl.CourseCode + " " + ctvbl.Status);
            }*/

            /*MediaBL mbl = new MediaBL("nis2");
            Console.WriteLine(mbl.ActivityCode);
            Console.WriteLine(mbl.FileName);
            Console.WriteLine(mbl.FilePath);
            Console.WriteLine(mbl.FileType);*/

            /*MediasBL msbl = new MediasBL();
            foreach (MediaBL mbl in msbl.MediaList)
            {
                Console.WriteLine(mbl.ActivityCode + " " + mbl.FileName + " " + mbl.FilePath + " " + mbl.FileType);
            }*/

            //ReportBL rbl = new ReportBL("b");

            /*ReportsBL rsbl = new ReportsBL();
            foreach (ReportBL rbl in rsbl.ReportList)
            {
                Console.WriteLine(rbl.ActivityCode + " " + rbl.Description + " " + rbl.PhoneNumber + " " + rbl.ReportDate);
            }*/

            //Console.WriteLine(ShiftsDAL.AddShift(5, 1, new DateTime(), new DateTime(), new DateTime(), "place1"));

            //Console.WriteLine(ShiftsDAL.AddShift(1, new DateTime(), new DateTime(), new DateTime(), "place2"));

            /*
            Console.WriteLine(sbl.ShiftCode);
            Console.WriteLine(sbl.TypeCode);
            Console.WriteLine(sbl.Place);*/

            /*ShiftsBL ssbl = new ShiftsBL();
            foreach (ShiftBL sbl in ssbl.ShiftList)
            {
                Console.WriteLine(sbl.DateOfShift + " " + sbl.FinishTime + " " + sbl.Place + " " + sbl.ShiftCode + " " + sbl.StartTime + " " + sbl.TypeCode);
            }*/
            
            Console.WriteLine("work");
            Console.ReadKey();
        }
    }
}

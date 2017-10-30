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
            //Console.WriteLine(VolunteerInfoDAL.AddVolunteer("t", "a", "a", "a", new DateTime(), "b", "b", "b", "b", "b", "b", "b", "b", new DateTime(), 1, "1"));
            //ShiftsTypesBL a = new ShiftsTypesBL();
            /*CoursesToVolunteersBL ctvsbl = new CoursesToVolunteersBL();
            foreach (CoursesToVolunteerBL ctvbl in ctvsbl.CoursesToVolunteerList)
            {
                Console.WriteLine(ctvbl.PhoneNumber + " " + ctvbl.CourseCode + " " + ctvbl.Status);
            }*/
            /*StockToVolunteerBL a = new StockToVolunteerBL("b", 4, 2, DateTime.Now);
            StockToVolunteerBL b = new StockToVolunteerBL(4);
            StockToVolunteersBL c = new StockToVolunteersBL();*/

            //ShiftsTypesBL a = new ShiftsTypesBL();
            //StockBL a = new StockBL("bil", 5, true);
            //StockBL b = new StockBL(4);
            //StocksBL a = new StocksBL();
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

            /*ValidityTypesBL check = new ValidityTypesBL();
            foreach (ValidityTypeBL vtbl in check.ValidityTypeList)
            {
                Console.WriteLine(vtbl.ValidityCode + " " + vtbl.ValidityName);
            }*/
            /*
            VolunteerTypeBL vtbl = new VolunteerTypeBL("a", true, false, true, false);
            Console.WriteLine(vtbl.TypeCode + " " + vtbl.TypeName + " " + vtbl.PermmisionStock + " " + vtbl.PermmisionShifts + " " + vtbl.PermmisionActivity + " " + vtbl.Independent);
            */

            //TypeToActivityBL a = new TypeToActivityBL("c");
            //TypeToActivityBL b = new TypeToActivityBL("b");
            //TypeToActivityBL c = new TypeToActivityBL(3);
            //TypesToActivityBL d = new TypesToActivityBL();
	    //
            //VolunteerBL a = new VolunteerBL("0547742946", "0527467729", "yoske", "cohen", new DateTime(2016, 7, 24), "yoske123", "123456", "benei zion 3", "hod hasharon", "yoske@gmail.com", "497536881", "748951475", "kfar saba", new DateTime(2019, 6, 2), 1, "1577392");
            //VolunteerBL b = new VolunteerBL("0547742946");
            //VolunteersBL c = new VolunteersBL();
            Console.WriteLine("work");
            Console.ReadKey();
        }
    }
}

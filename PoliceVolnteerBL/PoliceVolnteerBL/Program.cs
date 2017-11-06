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
            /*
            VolunteerTypesBL vtsbl = new VolunteerTypesBL();
            foreach (VolunteerTypeBL vtbl in vtsbl.VolunteerTypeList)
            {
                Console.WriteLine(vtbl.TypeCode + " " + vtbl.TypeName + " " + vtbl.PermmisionStock + " " + vtbl.PermmisionShifts + " " + vtbl.PermmisionActivity + " " + vtbl.Independent);
            }*/

            /*VolunteerToValidityBL vtvbl = new VolunteerToValidityBL("a", 44, new DateTime());
            Console.WriteLine(vtvbl.EndDate + " " + vtvbl.PhoneNumber + " " + vtvbl.Status + " " + vtvbl.ValidityCode);*/

            /*VolunteerToValiditysBL vtvsbl = new VolunteerToValiditysBL();
            foreach (VolunteerToValidityBL vtvbl in vtvsbl.VolunteerToValidityList)
            {
                Console.WriteLine(vtvbl.EndDate + " " + vtvbl.PhoneNumber + " " + vtvbl.Status + " " + vtvbl.ValidityCode);
            }*/

            //VolunteersBL vsbl = new VolunteersBL();
            //Console.WriteLine(vsbl.SumOfActivesVolunteers()+"/"+vsbl.VolunteerList.Count);
            /*foreach (VolunteerBL vbl in vsbl.VolunteerList)
            {
                Console.WriteLine(vbl.FName+" "+vbl.BirthDate+" "+vbl.HaveBirthDay());
            }*/

            //VolunteerToValidityBL vtvbl = new VolunteerToValidityBL("b", 46, new DateTime(2018, 10, 10));
            //Console.WriteLine(vtvbl.TimeToValidityEnd());
            //VolunteerToValidityBL vtvbl1 = new VolunteerToValidityBL("a", 44);
            //Console.WriteLine(vtvbl1.TimeToValidityEnd());
            //b.ShiftSignUp(2);
            //a.ShiftSignUp(2);
            //var c = a.GetValidities();
            //a.ChangeRank(6);
            //a.CourseSignUp(13);
            //c = a.GetCourses(DateTime.Now, OperatorType.Lower);
            //c = a.ItemsInPossession();
            //a.TakeItem(5, 2, DateTime.Now);
            //c = a.ItemsInPossession();

            //DataTable c = a.GetShifts(new DateTime(2001, 1, 18), OperatorType.LowerAndEquals);
            //c = a.GetShifts(new DateTime(2001, 1, 18), OperatorType.LowerAndEquals);
            //c = a.GetShifts(new DateTime(2001, 1, 18), OperatorType.Lower);
            //c = b.GetShifts(new DateTime(2001, 1, 1), OperatorType.Equals);
            //c = b.GetShifts(new DateTime(2001, 1, 18), OperatorType.NotEquals);
            //c = a.GetShifts(new DateTime(2001, 1, 5), OperatorType.Greater);
            //c = b.GetShifts(new DateTime(2001, 1, 1), OperatorType.GreaterAndEquals);
            //c = b.GetActivitys(new DateTime(2001, 1, 1), OperatorType.Equals);
            //Queue<string> c = a.GetCars();
            //a.AddNewCar(1994028.ToString());
            //c = a.GetCars();
            //a.DeleteCar(1994028.ToString());
            //c = a.GetCars();
            /*
            VolunteerTypesBL vtsbl = new VolunteerTypesBL();
            DataTable dt = vtsbl.GetAllPermmisions();*/

            /*CourseBL cbl = new CourseBL(13);
            DataTable dt = cbl.GetDetails();*/

            /*DateTime dt1 = new DateTime(2000, 10, 10);
            DateTime dt2 = new DateTime(2000, 10, 11);
            Console.WriteLine(dt1.CompareTo(dt2));*/

            /*CoursesBL csbl = new CoursesBL();
            Console.WriteLine(csbl.SumOfParticipantsAllInPeriod(new DateTime(1000, 1, 2), new DateTime(1999, 1, 15)));
            Console.WriteLine(csbl.SumOfCoursesInPeriod(new DateTime(2000, 1, 2), new DateTime(2000, 1, 15)));*/

            /*CourseBL cbl = new CourseBL(13);
            Console.WriteLine(cbl.SumOfParticipants());*/

            //ShiftsBL a = new ShiftsBL();
            //a = new ShiftsBL(new DateTime(2017, 11, 14));
            //ShiftsBL a = new ShiftsBL();
            //a.GetDetails();
            ShiftsTypesBL a = new ShiftsTypesBL();
            a.GetTypes();

            Console.WriteLine("work");
            Console.ReadKey();
        }
    }
}

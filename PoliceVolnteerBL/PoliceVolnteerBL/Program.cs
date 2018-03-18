using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceVolnteerDAL;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace PoliceVolnteerBL
{
    public class Program
    {
        [STAThread]
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
            //c = b.GetActivitys(new DateTime(2001, 1, 1), OperatorType.Equals);AS
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
            //ShiftsTypesBL a = new ShiftsTypesBL();
            //a.GetTypes();

            //VolunteerBL a = new VolunteerBL("a");
            //DataTable b = a.GetItemsInPossession();

            //StockBL stock = new StockBL();
            //DataTable data = stock.GetAllUnreturnedItems();
            //DataTable data = stock.GetAllTransference();

            //ReportsDAL.AddReport("a", new DateTime(1999, 1, 1), 7, "");
            //ReportBL a = new ReportBL("a", 7);
            //Console.WriteLine(a.getReport());

            //ActivityBL a = new ActivityBL(7);
            //a.GetAllReports();
            //a.GetAllVolunteers();
            //ActivitysBL b = new ActivitysBL();

            /*OpenFileDialog fileDialogBox = new OpenFileDialog();

            string filePath = "none";
            if (fileDialogBox.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialogBox.FileName;
                Console.WriteLine("File path : " + filePath);
            }
            
            string fileName = Path.GetFileName(filePath);
            Console.WriteLine("File name : " + fileName);

            string fileFormat = Path.GetExtension(fileName);
            Console.WriteLine("File format : " + fileFormat);

            string sourcePath = filePath.Substring(0, filePath.Length - fileName.Length);
            Console.WriteLine("Source path : " + sourcePath);
            
            string localPath = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine("Local path : " + localPath);
            localPath = localPath.Remove(localPath.Length - (5 + 3 + (2 * 1)));
            string targetPath = localPath + @"\Files";
            Console.WriteLine("Target path : " + targetPath);

            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);
            try
            {
                File.Copy(sourceFile, destFile, true);
            }
            catch (IOException e)
            {
                Console.WriteLine("{0}: The process can not access the file {1} because another process is using the file", e.GetType().Name, filePath);
            }*/

            //MediaBL m = new MediaBL(7);
            /*Console.Write("Name: ");
            string name = Console.ReadLine();
            
            OpenFileDialog fileDialogBox = new OpenFileDialog();

            string filePath = "none";
            if (fileDialogBox.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialogBox.FileName;
                Console.WriteLine("File path : " + filePath);
            }
            FileStream file = File.Open(filePath, FileMode.Open);
            byte[] byteArr2 = new byte[file.Length];
            file.Read(byteArr2, 0, int.Parse(file.Length.ToString()));
            file.Close();
            MediaBL m = new MediaBL(7, name+Path.GetExtension(file.Name), byteArr2);*/

            //Console.Write("Subfolder name : ");
            //string fileName = Console.ReadLine();

            //DateTime dt = new DateTime(2001, 2, 3, 4, 5, 6);
            //string folderName = fileName + " " + dt.ToString().Replace('/','-').Replace(':','-').Replace(' ','_').Substring(0,dt.ToString().Length-3);
            //Console.WriteLine(folderName); // 03/02/2001 04:05:06 -> 03-02-2001_04-05
            //string localPath = System.IO.Directory.GetCurrentDirectory();
            //localPath = localPath.Remove(localPath.Length - (5 + 3 + (2 * 1)));
            //string targetPath = localPath + @"\Files";
            //string pathString = System.IO.Path.Combine(targetPath, fileName/* + " " + dt*/);
            //System.IO.Directory.CreateDirectory(pathString);

            //ActivityDAL.AddActivity("a", new DateTime(2001, 2, 3), new DateTime(1, 1, 1, 4, 5, 6), new DateTime(1, 1, 1, 4, 5, 6), "a", 1, "a", 9);
            //Console.WriteLine(MediaBL.DeleteFile(9,"two.png"));

            /*MediasBL msbl = new MediasBL();
            foreach (MediaBL mbl in msbl.MediaList)
            {
                Console.WriteLine(mbl.ActivityCode + " " + mbl.FileName + " " + mbl.FileType);
            }
            
            Console.WriteLine();
            msbl = new MediasBL(new DateTime(1991, 8, 8), new DateTime(1991, 10, 10));
            foreach (MediaBL mbl in msbl.MediaList)
            {
                Console.WriteLine(mbl.ActivityCode + " " + mbl.FileName + " " + mbl.FileType);
            }

            MediasBL msbl = new MediasBL(2);
            foreach (MediaBL mbl in msbl.MediaList)
            {
                Console.WriteLine(mbl.ActivityCode + " " + mbl.FileName + " " + mbl.FileType);
            }*/

            //MediaBL mbl = new MediaBL(6, "menash.txt", new byte[] { 97, 98, 99 });

            /*VolunteersBL checkFunc = new VolunteersBL(false);
            DataTable dt = checkFunc.GetTrasfersNotReturned();*/

            //VolunteerInfoDAL.UpdateFrom("b", new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber, "g", FieldType.String, OperatorType.Equals));

            //ItemBL item = new ItemBL("plates", 0, true);

            //ValidityTypeBL vtbl = new ValidityTypeBL("Pistol");
            //vtbl = new ValidityTypeBL("Rifle");
            //vtbl = new ValidityTypeBL("Kojak");

            //VolunteerToValidityDAL.Add("0507986481", 52, DateTime.Now);
            //VolunteerToValidityDAL.Add("0524978648", 50, DateTime.Now);
            //VolunteerToValidityDAL.Add("0524978648", 51, DateTime.Now);
            //VolunteerToValidityDAL.Add("0524978648", 52, DateTime.Now);
            //VolunteerToValidityDAL.Add("0527948679", 51, DateTime.Now);

            //ShiftTypesBL stbl = new ShiftTypesBL("tour");
            //stbl = new ShiftTypesBL("guard");
            //DateTime now = DateTime.Now;
            //ShiftBL sbl = new ShiftBL(11, now, now, new DateTime(now.Year, now.Month, now.Day, now.Hour + 5, now.Minute, now.Second), "kfar saba");
            //now.AddDays(10);
            //sbl = new ShiftBL(12, now, now, new DateTime(now.Year, now.Month, now.Day, now.Hour + 2, now.Minute, now.Second), "kfar saba");
            //now.AddDays(2);
            //sbl = new ShiftBL(11, now, now, new DateTime(now.Year, now.Month, now.Day, now.Hour + 1, now.Minute, now.Second), "hodash");


            //DateTime now = new DateTime(2018,4,21,8,40,0);
            //ShiftBL sbl = new ShiftBL(11, now, now, new DateTime(now.Year, now.Month, now.Day, now.Hour + 5, now.Minute, now.Second), "kfar saba");
            //now = now.AddDays(3);
            //sbl = new ShiftBL(12, now, now, new DateTime(now.Year, now.Month, now.Day, now.Hour + 2, now.Minute, now.Second), "kfar saba");
            //now = now.AddDays(2);
            //sbl = new ShiftBL(11, now, now, new DateTime(now.Year, now.Month, now.Day, now.Hour + 1, now.Minute, now.Second), "hodash");

            //CarsReportsDAL.AddCarReport(8, "1254321", 23.1);
            //CarsReportsDAL.AddCarReport(11, "8833388", 1.23);
            //CarsReportsDAL.AddCarReport(13, "1485236", 45.52);
            //CarsReportsDAL.AddCarReport(10, "9625847", 35.15);
            //CarsReportsDAL.AddCarReport(9, "1254321", 15.5);
            //CourseBL cbl = new CourseBL("pistol licence", new DateTime(2018, 4, 21, 10, 0, 0), new DateTime(2018, 4, 21, 10, 0, 0), new DateTime(2018, 4, 21, 13, 0, 0), "dvir osher", true, "kfar saba", "get hand gun validity", 50);
            //cbl = new CourseBL("rifle licence", new DateTime(2018, 5, 12, 10, 0, 0), new DateTime(2018, 5, 12, 10, 0, 0), new DateTime(2018, 5, 12, 15, 0, 0), "guy osov", true, "kfar saba", "get hand rifle validity", 51);
            //cbl = new CourseBL("Kojak licence", new DateTime(2019, 5, 12, 10, 0, 0), new DateTime(2019, 5, 12, 10, 0, 0), new DateTime(2019, 5, 12, 15, 0, 0), "moshe kafia", true, "kfar saba", "get Kojak validity", 52);

            Console.WriteLine("work");
            Console.ReadKey();
        }
    }
}

using PoliceVolnteerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerBL
{
    public class VolunteerBL
    {
        public string PhoneNumber { get; set; }
        public string EmergencyNumber { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string EmailAddress { get; set; }
        public string ID { get; set; }
        public string PoliceID { get; set; }
        public string ServeCity { get; set; }
        public DateTime StartDate { get; set; }
        public VolunteerTypeBL Type { get; set; }
        public bool Status { get; set; }

        public VolunteerBL(string phoneNumber, string emergencyNumber, string fName, string lName, DateTime birthDate, string userName, string password, string homeAddress, string homeCity, string emailAddress, string iD, string policeID, string serveCity, DateTime startDate, int type)
        {
            VolunteerInfoDAL.AddVolunteer(phoneNumber, emergencyNumber, fName, lName, birthDate, userName, password, homeAddress, homeCity, emailAddress, iD, policeID, serveCity, startDate, type);
            this.PhoneNumber = phoneNumber;
            this.EmergencyNumber = emergencyNumber;
            this.FName = fName;
            this.LName = lName;
            this.BirthDate = birthDate;
            this.UserName = userName;
            this.Password = password;
            this.HomeAddress = homeAddress;
            this.HomeCity = homeCity;
            this.EmailAddress = emailAddress;
            this.ID = iD;
            this.PoliceID = policeID;
            this.StartDate = startDate;
            this.Type = new VolunteerTypeBL(type);
            this.Status = true;
        }

        public VolunteerBL(string phoneNumber)
        {
            DataRow dr = VolunteerInfoDAL.GetTable(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber, phoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            this.PhoneNumber = phoneNumber;
            this.EmergencyNumber = dr["EmergencyNumber"].ToString();
            this.FName = dr["FName"].ToString();
            this.LName = dr["LName"].ToString();
            this.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
            this.UserName = dr["UserName"].ToString();
            this.Password = dr["Password"].ToString();
            this.HomeAddress = dr["HomeAddress"].ToString();
            this.HomeCity = dr["HomeCity"].ToString();
            this.EmailAddress = dr["EmailAddress"].ToString();
            this.ID = dr["ID"].ToString();
            this.Status = bool.Parse(dr["status"].ToString());
            dr = VolunteerPoliceInfoDAL.GetTable(new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.PhoneNumber, phoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            this.PoliceID = dr["PoliceID"].ToString();
            this.ServeCity = dr["ServeCity"].ToString();
            this.StartDate = DateTime.Parse(dr["StartDate"].ToString());
            this.Type = new VolunteerTypeBL(int.Parse(dr["Type"].ToString()));
        }

        public bool HasBirthDay()
        {
            return (this.BirthDate.Day == DateTime.Now.Day && this.BirthDate.Month == DateTime.Now.Month);
        }

        public void ChangeStatus(bool newStatus)
        {
            //is the new status different?
            if(this.Status != newStatus)
            {
                //change status
                VolunteerInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.status, newStatus, FieldType.Boolean, OperatorType.Equals));
                this.Status = newStatus;
            }
        }
        
        public void AddNewCar(string CarID)
        {
            CarToVolunteerDAL.AddCar(PhoneNumber, CarID);
        }

        public Queue<string> GetCars()
        {
            Queue<string> ret = new Queue<string>();
            //get all cars of the volunteer
            DataRowCollection Cars = CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            foreach(DataRow car in Cars)
            {
                //add the carid to the queue
                ret.Enqueue(car["CarID"].ToString());
            }
            return ret;
        }

        public void DeleteCar(string CarID)
        {
            CarToVolunteerDAL.DelUser(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, CarID, FieldType.String, OperatorType.Equals));
        }

        public DataTable GetCarReports(string carID)
        {
            //get all reports of this car
            DataTable reports = CarsReportsDAL.GetTable(new FieldValue<CarsReportsField>(CarsReportsField.CarID, carID, FieldType.String, OperatorType.Equals)).Tables[0];
            return reports;
        }

        public DataTable GetCarReports()
        {
            //get all reports of volunteer
            Queue<FieldValue<CarsReportsField>> filter = new Queue<FieldValue<CarsReportsField>>();
            foreach (string id in GetCars())
            {
                filter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.CarID, id, FieldType.String, OperatorType.Equals));
            }
            DataTable reports = CarsReportsDAL.GetTable(filter, false).Tables[0];
            return reports;
        }

        public DataTable GetCarReports(DateTime date, OperatorType Operator, string carID)
        {
            //get all reports of the car
            DataTable reports = this.GetCarReports(carID);
            //go to shift table
            Queue<FieldValue<ShiftsField>> shiftFilter = new Queue<FieldValue<ShiftsField>>();
            foreach(DataRow report in reports.Rows)
            {
                shiftFilter.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, report["ShiftCode"].ToString(), FieldType.Number, OperatorType.Equals));
            }
            DataTable shifts = ShiftsDAL.GetTable(shiftFilter, false).Tables[0];
            //filter unwanted shifts
            FieldValue<ShiftsField> mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, date, FieldType.DateTime, Operator);
            shifts.DefaultView.RowFilter = mask.ToString();
            shifts = (shifts.DefaultView).ToTable();
            //go to reports table
            Queue<FieldValue<CarsReportsField>> reportsFilter = new Queue<FieldValue<CarsReportsField>>();
            foreach(DataRow shift in shifts.Rows)
            {
                reportsFilter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shift["ShiftCode"].ToString(), FieldType.Number, OperatorType.Equals));
            }
            reports = CarsReportsDAL.GetTable(reportsFilter, false).Tables[0];
            return reports;
        }

        public DataTable GetCarReports(DateTime date, OperatorType Operator)
        {
            //get all reports of the volunteer
            DataTable reports = this.GetCarReports();
            //go to shift table
            Queue<FieldValue<ShiftsField>> shiftFilter = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow report in reports.Rows)
            {
                shiftFilter.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, report["ShiftCode"].ToString(), FieldType.Number, OperatorType.Equals));
            }
            DataTable shifts = ShiftsDAL.GetTable(shiftFilter, false).Tables[0];
            //filter unwanted shifts
            FieldValue<ShiftsField> mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, date, FieldType.DateTime, Operator);
            shifts.DefaultView.RowFilter = mask.ToString();
            shifts = (shifts.DefaultView).ToTable();
            //go to reports table
            Queue<FieldValue<CarsReportsField>> reportsFilter = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow shift in shifts.Rows)
            {
                reportsFilter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shift["ShiftCode"].ToString(), FieldType.Number, OperatorType.Equals));
            }
            reports = CarsReportsDAL.GetTable(reportsFilter, false).Tables[0];
            return reports;
        }

        public DataTable GetCourses(DateTime Date, OperatorType Operator)
        {
            //create filter
            FieldValue<CourseField> Mask = new FieldValue<CourseField>(CourseField.CourseDate, Date.ToShortDateString(), FieldType.DateTime, Operator);
            //get all reports of volunteer
            DataRowCollection Rows = CoursesToVolunteerDAL.GetTable(new FieldValue<CoursesToVolunteerField>(CoursesToVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<CourseField>> CourseCode = new Queue<FieldValue<CourseField>>();
            foreach (DataRow Row in Rows)
            {
                CourseCode.Enqueue(new FieldValue<CourseField>(CourseField.CourseCode, Row["CourseCode"], FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = CourseDAL.GetTable(CourseCode, false);
            //filter unwanted reports
            ds.Tables[0].DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();
            return FilteredTable;
        }



        public void CourseSignUp(int CourseCode)
        {
            CoursesToVolunteerDAL.AddCoursesToVolunteer(this.PhoneNumber, CourseCode);
        }

        public DataTable ItemsInPossession()
        {
            return StockToVolunteerDAL.GetTable(new FieldValue<StockToVolunteerField>(StockToVolunteerField.PhoneVolunteer, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0];
        }
        public void TakeItem(int ItemCode, int Amount, DateTime Date)
        {
            StockToVolunteerDAL.AddTransference(this.PhoneNumber, ItemCode, Amount, Date);
        }

        public void ChangeRank(int RankCode)
        {
            try
            {
                //change object in code
                VolunteerTypeBL NewType = new VolunteerTypeBL(RankCode);
                this.Type = NewType;
                //change object in database
                VolunteerPoliceInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.Type, RankCode, FieldType.Number, OperatorType.Equals));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable GetValidities()
        {
            return VolunteerToValidityDAL.GetTable(new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0];
        }

        public DataTable GetExpiraedValidities() //לאפשר למנהל לשנות אופסט של ימים שיתריע לו מתי נגמר למתנדב הרשיונות
        {
            //get all user's validities
            DataTable AllValidities = this.GetValidities();
            //create mask to filter validities by todays date
            FieldValue<VolunteerToValidityField> Mask = new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.EndDate, DateTime.Now.ToShortDateString(), FieldType.DateTime, OperatorType.LowerAndEquals);
            //filter unnececery validities
            AllValidities.DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredValidities = (AllValidities.DefaultView).ToTable();
            return FilteredValidities;
        }



        public void ShiftSignUp(int ShiftCode)
        {
            //is able to go on car shifts alone
            if(this.Type.Independent)
            {
                //get all the volunteers in the shift
                DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
                if (VolunteersInShift.Count > 1)//if there is no more place in the shift exit
                    return;
                //add him in the shift
                ShiftsToVolunteerDAL.AddShift(ShiftCode, this.PhoneNumber, "");
            }
            else
            {
                //get all volunteers in the shifts
                DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
                //if there is no more place or there is already a non independent volunteer in the shift exit
                if (VolunteersInShift.Count > 2 || VolunteersInShift.Count < 1)
                    return;
                foreach(DataRow VolunteerInShift in VolunteersInShift)
                {
                    VolunteerBL volunteer = new VolunteerBL(VolunteerInShift["PhoneNumber"].ToString());
                    if (!volunteer.Type.Independent)
                        return;
                }
                //add him to the shift
                ShiftsToVolunteerDAL.AddShift(ShiftCode, this.PhoneNumber, "");
            }
        }

        public void ActivitySignUp(int ActivityCode)
        {
            ReportsDAL.AddReport(this.PhoneNumber, ActivityCode, "");
        }

        public void ShiftReport(ShiftBL shift, string comment, string carID = "", double distance = 0)
        {
            if(distance != 0)//if the volunteer has entered Car shift info so add the car shift to the database
            {
                //authenticate CarID for volunteer
                if (CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, carID, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["PhoneNumber"].ToString() != this.PhoneNumber)
                    return;
                CarsReportsDAL.AddCarReport(shift.ShiftCode, carID, distance);
            }
            //add shift
            ShiftsToVolunteerDAL.AddComment(this.PhoneNumber, shift.ShiftCode, comment);
        }

        public double GetDistance(DateTime FromDate, DateTime ToDate)
        {
            //get all the volunteer's car
            DataRowCollection AllCars = CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            //search for all known shifts the car has been used in
            Queue<FieldValue<CarsReportsField>> ReportParameters = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow Car in AllCars)
            {
                ReportParameters.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.CarID, Car["CarID"], FieldType.String, OperatorType.Equals));
            }
            if (ReportParameters.Count == 0)
                return 0;
            DataTable shifts = CarsReportsDAL.GetTable(ReportParameters, false).Tables[0];
            //filter shifts by date
            Queue<FieldValue<ShiftsField>> ShiftParameters = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow Shift in shifts.Rows)
            {
                ShiftParameters.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, Shift["ShiftCode"], FieldType.Number, OperatorType.Equals));
            }
            if (ShiftParameters.Count == 0)
                return 0;
            FieldValue<ShiftsField> Mask1 = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, FromDate, FieldType.DateTime, OperatorType.GreaterAndEquals);
            FieldValue<ShiftsField> Mask2 = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, ToDate, FieldType.DateTime, OperatorType.LowerAndEquals);
            DataTable Filter = ShiftsDAL.GetTable(ShiftParameters, false).Tables[0];
            Filter.DefaultView.RowFilter = Mask1.ToString();
            Filter = (Filter.DefaultView).ToTable();
            Filter.DefaultView.RowFilter = Mask2.ToString();
            Filter = (Filter.DefaultView).ToTable();
            //get all needed reports
            ReportParameters = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow shiftCode in Filter.Rows)
            {
                ReportParameters.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shiftCode["ShiftCode"], FieldType.Number, OperatorType.Equals));
            }
            if (ReportParameters.Count == 0)
                return 0;
            shifts = CarsReportsDAL.GetTable(ReportParameters, false).Tables[0];
            //sum distance
            double totalDistance = 0;
            foreach (DataRow Distance in shifts.Rows)
            {
                totalDistance += double.Parse(Distance["Distance"].ToString());
            }
            return totalDistance;
        }

        public DataTable GetShifts(DateTime Date, OperatorType Operator)
        {
            //mask for later filtration
            FieldValue<ShiftsField> Mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, Date.ToShortDateString(), FieldType.DateTime, Operator);
            //get all shifts regarding the volunteer
            DataRowCollection Rows = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ShiftsField>> ShiftsCode = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow Row in Rows)
            {
                ShiftsCode.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, Row["shiftCode"], FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = ShiftsDAL.GetTable(ShiftsCode, false);
            //filter shifts using date and operator
            ds.Tables[0].DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();

            return FilteredTable;

        }

        public DataTable GetShiftReports()
        {
            return ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0];
        }


        public DataTable GetShiftReports(DateTime date, OperatorType Operator)
        {
            //get all shifts by date
            DataTable Shifts = GetShifts(date, Operator);
            //get the desired reports
            Queue<FieldValue<ShiftsToVolunteerField>> shiftReportFilter = new Queue<FieldValue<ShiftsToVolunteerField>>();
            foreach(DataRow shift in Shifts.Rows)
            {
                shiftReportFilter.Enqueue(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, shift["ShiftCode"], FieldType.Number, OperatorType.Equals));
            }
            DataTable reports = ShiftsToVolunteerDAL.GetTable(shiftReportFilter, false).Tables[0];
            FieldValue<ShiftsToVolunteerField> Mask = new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, Operator);
            reports.DefaultView.RowFilter = Mask.ToString();
            reports = (reports.DefaultView).ToTable();
            return reports;
        }

        public DataTable GetActivitys(DateTime Date, OperatorType Operator)
        {
            FieldValue<ActivityField> Mask = new FieldValue<ActivityField>(ActivityField.ActivityDate, Date.ToShortDateString(), FieldType.DateTime, Operator);
            DataRowCollection Rows = ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ActivityField>> ActivitysCode = new Queue<FieldValue<ActivityField>>();
            foreach (DataRow Row in Rows)
            {
                ActivitysCode.Enqueue(new FieldValue<ActivityField>(ActivityField.ActivityCode, Row["ActivityCode"], FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = ActivityDAL.GetTable(ActivitysCode, false);
            ds.Tables[0].DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();
            return FilteredTable;

        }

        public DataTable GetActivityReports()
        {
            return ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0];
        }

        public DataTable GetActivityReports(DateTime date, OperatorType Operator)
        {
            //get all activities
            DataTable Activities = GetActivitys(date, Operator);
            //filter reports by activities
            Queue<FieldValue<ReportsField>> ActivityFilter = new Queue<FieldValue<ReportsField>>();
            foreach (DataRow activity in Activities.Rows)
            {
                ActivityFilter.Enqueue(new FieldValue<ReportsField>(ReportsField.ActivityCode, activity["ActivityCode"], FieldType.Number, OperatorType.Equals));
            }
            DataTable reports = ReportsDAL.GetTable(ActivityFilter, false).Tables[0];
            FieldValue<ReportsField> Mask = new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals);
            reports.DefaultView.RowFilter = Mask.ToString();
            reports = (reports.DefaultView).ToTable();
            return reports;
        }


    }
}

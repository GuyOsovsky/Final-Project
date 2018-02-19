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
        public string PhoneNumber { get; private set; }
        public string EmergencyNumber { get; private set; }
        public string FName { get; private set; }
        public string LName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string HomeAddress { get; private set; }
        public string HomeCity { get; private set; }
        public string EmailAddress { get; private set; }
        public string ID { get; private set; }
        public string PoliceID { get; private set; }
        public string ServeCity { get; private set; }
        public DateTime StartDate { get; private set; }
        public VolunteerTypeBL Type { get; private set; }
        public bool Status { get; private set; }

        public VolunteerBL(string phoneNumber, string emergencyNumber, string fName, string lName, DateTime birthDate, string userName, string password, string homeAddress, string homeCity, string emailAddress, string iD, string policeID, string serveCity, int type)
        {
            try
            {
                VolunteerInfoDAL.AddVolunteer(phoneNumber, emergencyNumber, fName, lName, birthDate, userName, password, homeAddress, homeCity, emailAddress, iD, policeID, serveCity, DateTime.Now.Date, type);
            }
            catch (Exception e)
            {
                this.PhoneNumber = "";
                return;
            }
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
            this.StartDate = DateTime.Now.Date;
            this.Type = new VolunteerTypeBL(type);
            this.Status = true;
        }

        public VolunteerBL(string phoneNumber)
        {
            DataRow dr;
            try
            {
                dr = VolunteerInfoDAL.GetTable(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber, phoneNumber, FieldType.String, OperatorType.Equals), true).Tables[0].Rows[0];
            }
            catch (Exception e)
            {
                this.PhoneNumber = "";
                return;
            }
            DataRow volunteerInfoRow = VolunteerInfoDAL.GetTable(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber, phoneNumber, FieldType.String, OperatorType.Equals), false).Tables[0].Rows[0];
            this.PhoneNumber = phoneNumber;
            this.EmergencyNumber = volunteerInfoRow["EmergencyNumber"].ToString();
            this.FName = volunteerInfoRow["FName"].ToString();
            this.LName = volunteerInfoRow["LName"].ToString();
            this.BirthDate = DateTime.Parse(volunteerInfoRow["BirthDate"].ToString());
            this.UserName = volunteerInfoRow["UserName"].ToString();
            this.Password = volunteerInfoRow["Password"].ToString();
            this.HomeAddress = volunteerInfoRow["HomeAddress"].ToString();
            this.HomeCity = volunteerInfoRow["HomeCity"].ToString();
            this.EmailAddress = volunteerInfoRow["EmailAddress"].ToString();
            this.ID = volunteerInfoRow["ID"].ToString();
            this.Status = bool.Parse(volunteerInfoRow["status"].ToString());
            volunteerInfoRow = VolunteerPoliceInfoDAL.GetTable(new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.PhoneNumber, phoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            this.PoliceID = volunteerInfoRow["PoliceID"].ToString();
            this.ServeCity = volunteerInfoRow["ServeCity"].ToString();
            this.StartDate = DateTime.Parse(volunteerInfoRow["StartDate"].ToString());
            this.Type = new VolunteerTypeBL(int.Parse(volunteerInfoRow["Type"].ToString()));
        }

        public VolunteerBL(string userName, string password)
        {
            DataRow dr;
            try
            {
                Queue<FieldValue<VolunteerInfoDALField>> parameters = new Queue<FieldValue<VolunteerInfoDALField>>();
                parameters.Enqueue(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.UserName, userName, FieldType.String, OperatorType.Equals));
                parameters.Enqueue(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.Password, password, FieldType.String, OperatorType.Equals));
                dr = VolunteerInfoDAL.GetTable(parameters, true).Tables[0].Rows[0];
            }
            catch (Exception e)
            {
                this.PhoneNumber = "";
                return;
            }
            this.PhoneNumber = dr["PhoneNumber"].ToString();
            this.EmergencyNumber = dr["EmergencyNumber"].ToString();
            this.FName = dr["FName"].ToString();
            this.LName = dr["LName"].ToString();
            this.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());
            this.UserName = userName;
            this.Password = password;
            this.HomeAddress = dr["HomeAddress"].ToString();
            this.HomeCity = dr["HomeCity"].ToString();
            this.EmailAddress = dr["EmailAddress"].ToString();
            this.ID = dr["ID"].ToString();
            this.Status = bool.Parse(dr["status"].ToString());
            dr = VolunteerPoliceInfoDAL.GetTable(new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            this.PoliceID = dr["PoliceID"].ToString();
            this.ServeCity = dr["ServeCity"].ToString();
            this.StartDate = DateTime.Parse(dr["StartDate"].ToString());
            this.Type = new VolunteerTypeBL(int.Parse(dr["Type"].ToString()));
        }
        
        public static DataSet GetAllTable()
        {
            return VolunteerInfoDAL.GetTable();
        }

        public void UpdateVolunteer(object field, object value)
        {
            FieldType valueType;
            if (field is VolunteerInfoDALField)
            {
                switch ((VolunteerInfoDALField)field)
                {
                    case VolunteerInfoDALField.EmailAddress:
                    case VolunteerInfoDALField.EmergencyNumber:
                    case VolunteerInfoDALField.FName:
                    case VolunteerInfoDALField.HomeAddress:
                    case VolunteerInfoDALField.HomeCity:
                    case VolunteerInfoDALField.ID:
                    case VolunteerInfoDALField.LName:
                    case VolunteerInfoDALField.Password:
                    case VolunteerInfoDALField.PhoneNumber:
                    case VolunteerInfoDALField.UserName:
                        valueType = FieldType.String;
                        break;
                    case VolunteerInfoDALField.BirthDate:
                        valueType = FieldType.Date;
                        break;
                    case VolunteerInfoDALField.status:
                        valueType = FieldType.Boolean;
                        break;
                    default:
                        valueType = FieldType.String; //change to throw exeption
                        break;
                }
                VolunteerInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerInfoDALField>((VolunteerInfoDALField)field, value, valueType, OperatorType.Equals));
            }
            if (field is VolunteerPoliceInfoDALField)
            {
                switch ((VolunteerPoliceInfoDALField)field)
                {
                    case VolunteerPoliceInfoDALField.PoliceID:
                    case VolunteerPoliceInfoDALField.ServeCity:
                        valueType = FieldType.String;
                        break;
                    case VolunteerPoliceInfoDALField.StartDate:
                        valueType = FieldType.Date;
                        break;
                    case VolunteerPoliceInfoDALField.Type:
                        valueType = FieldType.Number;
                        break;
                    default:
                        valueType = FieldType.String; //change to throw exeption
                        break;
                }
                VolunteerPoliceInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerPoliceInfoDALField>((VolunteerPoliceInfoDALField)field, value, valueType, OperatorType.Equals));
            }
        }

        public DataSet VolunteerToDataSet()
        {
            DataSet volunteer = new DataSet();
            volunteer.Tables.Add();
            volunteer.Tables[0].Columns.Add("מספר טלפון", typeof(String));
            volunteer.Tables[0].Columns.Add("מספר חירום", typeof(String));
            volunteer.Tables[0].Columns.Add("שם פרטי", typeof(String));
            volunteer.Tables[0].Columns.Add("שם משפחה", typeof(String));
            volunteer.Tables[0].Columns.Add("יום הולדת", typeof(DateTime));
            volunteer.Tables[0].Columns.Add("כתובת מגורים", typeof(String));
            volunteer.Tables[0].Columns.Add("כתובת אימייל", typeof(String));
            volunteer.Tables[0].Columns.Add("תעודת זהות", typeof(String));
            volunteer.Tables[0].Columns.Add("מספר זיהוי משטרתי", typeof(String));
            volunteer.Tables[0].Columns.Add("עיר שירות", typeof(String));
            volunteer.Tables[0].Columns.Add("תאריך התחלה", typeof(DateTime));
            volunteer.Tables[0].Rows.Add();
            volunteer.Tables[0].Rows[0][0] = this.PhoneNumber;
            volunteer.Tables[0].Rows[0][1] = this.EmergencyNumber;
            volunteer.Tables[0].Rows[0][2] = this.FName;
            volunteer.Tables[0].Rows[0][3] = this.LName;
            volunteer.Tables[0].Rows[0][4] = this.BirthDate;
            volunteer.Tables[0].Rows[0][5] = this.HomeAddress;
            volunteer.Tables[0].Rows[0][6] = this.EmergencyNumber;
            volunteer.Tables[0].Rows[0][7] = this.ID;
            volunteer.Tables[0].Rows[0][8] = this.PoliceID;
            volunteer.Tables[0].Rows[0][9] = this.ServeCity;
            volunteer.Tables[0].Rows[0][10] = this.StartDate;
            return volunteer;
        }

        public bool HasBirthDay()
        {
            return (this.BirthDate.Day == DateTime.Now.Day && this.BirthDate.Month == DateTime.Now.Month);
        }

        public void ChangeStatus(bool newStatus)
        {
            //Is the new status different?
            if (this.Status != newStatus)
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
            foreach (DataRow car in Cars)
            {
                //add the carid to the queue
                ret.Enqueue(car["CarID"].ToString());
            }
            return ret;
        }

        public void DeleteCar(string CarID)
        {
            CarToVolunteerDAL.DelCar(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, CarID, FieldType.String, OperatorType.Equals));
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
            foreach (DataRow report in reports.Rows)
            {
                shiftFilter.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, report["ShiftCode"], FieldType.Number, OperatorType.Equals));
            }
            DataTable shifts = ShiftsDAL.GetTable(shiftFilter, false).Tables[0];
            //filter unwanted shifts
            FieldValue<ShiftsField> mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, date, FieldType.Date, Operator);
            shifts.DefaultView.RowFilter = mask.ToString();
            shifts = (shifts.DefaultView).ToTable();
            //go to reports table
            Queue<FieldValue<CarsReportsField>> reportsFilter = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow shift in shifts.Rows)
            {
                reportsFilter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shift["ShiftCode"], FieldType.Number, OperatorType.Equals));
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
                shiftFilter.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, report["ShiftCode"], FieldType.Number, OperatorType.Equals));
            }
            DataTable shifts = ShiftsDAL.GetTable(shiftFilter, false).Tables[0];
            //filter unwanted shifts
            FieldValue<ShiftsField> mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, date, FieldType.Date, Operator);
            shifts.DefaultView.RowFilter = mask.ToString();
            shifts = (shifts.DefaultView).ToTable();
            //go to reports table
            Queue<FieldValue<CarsReportsField>> reportsFilter = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow shift in shifts.Rows)
            {
                reportsFilter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shift["ShiftCode"], FieldType.Number, OperatorType.Equals));
            }
            reports = CarsReportsDAL.GetTable(reportsFilter, false).Tables[0];
            return reports;
        }

        public DataTable GetCourses(DateTime Date, OperatorType Operator)
        {
            //create filter
            FieldValue<CourseField> Mask = new FieldValue<CourseField>(CourseField.CourseDate, Date, FieldType.Date, Operator);
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

        public void CourseSignOut(int courseCode)
        {
            CourseBL course = new CourseBL(courseCode);
            if (course.IsRequeired)
                return;
            CoursesToVolunteerDAL.DelCourse(this.PhoneNumber, courseCode);
        }

        public DataSet GetItemsInPossession()
        {
            //create parameters for searching the StockToVolunteer table
            Queue<FieldValue<StockToVolunteerField>> parameters = new Queue<FieldValue<StockToVolunteerField>>();
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.PhoneVolunteer, this.PhoneNumber, FieldType.String, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.ReturnDate, new DateTime(1999, 1, 1), FieldType.Date, OperatorType.Equals));
            DataRowCollection Recycable = StockDAL.GetTable(new FieldValue<StockField>(StockField.Recyclable, true, FieldType.Boolean, OperatorType.Equals)).Tables[0].Rows;
            foreach (DataRow item in Recycable)
            {
                parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.ItemID, item["ItemID"], FieldType.Number, OperatorType.NotEquals));
            }
            return StockToVolunteerDAL.GetTable(parameters, true);

        }

        public void TakeItem(int ItemCode, int Amount, DateTime Date)
        {
            StockToVolunteerDAL.AddTransference(this.PhoneNumber, ItemCode, Amount, Date);
        }

        public void ReturnItem(int TransactionCode)
        {
            StockToVolunteerDAL.ReturnItem(TransactionCode);
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

        public DataSet GetValidities()
        {
            return VolunteerToValidityDAL.GetTable(new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals));
        }

        public DataTable GetExpiraedValidities() //לאפשר למנהל לשנות אופסט של ימים שיתריע לו מתי נגמר למתנדב הרשיונות
        {
            //get all user's validities
            DataTable AllValidities = this.GetValidities().Tables[0];
            //create mask to filter validities by todays date
            FieldValue<VolunteerToValidityField> Mask = new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.EndDate, DateTime.Now.ToShortDateString(), FieldType.Date, OperatorType.LowerAndEquals);
            //filter unnececery validities
            AllValidities.DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredValidities = (AllValidities.DefaultView).ToTable();
            return FilteredValidities;
        }

        public void ShiftSignUp(int ShiftCode)
        {
            if (this.Type.Independent)
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
                foreach (DataRow VolunteerInShift in VolunteersInShift)
                {
                    VolunteerBL volunteer = new VolunteerBL(VolunteerInShift["PhoneNumber"].ToString());
                    if (!volunteer.Type.Independent)
                        return;
                }
                //add him to the shift
                ShiftsToVolunteerDAL.AddShift(ShiftCode, this.PhoneNumber, "");
            }
        }

        public void ShiftSignOut(int shiftCode)
        {
            ShiftsToVolunteerDAL.DelShiftToVolunteer(this.PhoneNumber, shiftCode);
        }

        public void ActivitySignUp(int ActivityCode)
        {
            ReportsDAL.AddReport(this.PhoneNumber, new DateTime(1999, 1, 1), ActivityCode, "");
        }

        public void ActivitySignOut(int ActivityCode)
        {
            ReportsDAL.DelReport(this.PhoneNumber, ActivityCode);
        }

        public void ShiftReport(ShiftBL shift, string comment, string carID = "", double distance = 0)
        {
            if (distance != 0)//if the volunteer has entered Car shift info so add the car shift to the database
            {
                //authenticate CarID for volunteer
                if (CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, carID, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["PhoneNumber"].ToString() != this.PhoneNumber)
                    return;
                CarsReportsDAL.AddCarReport(shift.ShiftCode, carID, distance);
            }
            //update comment
            ShiftsToVolunteerDAL.UpdateComment(this.PhoneNumber, shift.ShiftCode, comment);
        }

        public void ActivityReport(ActivityBL activity, string description, DateTime reportDate)
        {
            ReportBL report = new ReportBL(this.PhoneNumber, activity.ActivityCode);
            report.UpdateDescription(description, reportDate);
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
            FieldValue<ShiftsField> Mask1 = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, FromDate, FieldType.Date, OperatorType.GreaterAndEquals);
            FieldValue<ShiftsField> Mask2 = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, ToDate, FieldType.Date, OperatorType.LowerAndEquals);
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
            FieldValue<ShiftsField> Mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, Date, FieldType.Date, Operator);
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

        public DataTable GetShifts()
        {
            //get all shifts regarding the volunteer
            DataRowCollection Rows = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ShiftsField>> ShiftsCode = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow Row in Rows)
            {
                ShiftsCode.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, Row["shiftCode"], FieldType.Number, OperatorType.Equals));
            }
            return ShiftsDAL.GetTable(ShiftsCode, false).Tables[0];
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
            foreach (DataRow shift in Shifts.Rows)
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
            //create parameters to filter the table
            FieldValue<ActivityField> Mask = new FieldValue<ActivityField>(ActivityField.ActivityDate, Date, FieldType.Date, Operator);
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

        public DataTable GetActivitys()
        {
            //create parameters to filter the table
            DataRowCollection Rows = ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ActivityField>> ActivitysCode = new Queue<FieldValue<ActivityField>>();
            foreach (DataRow Row in Rows)
            {
                ActivitysCode.Enqueue(new FieldValue<ActivityField>(ActivityField.ActivityCode, Row["ActivityCode"], FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = ActivityDAL.GetTable(ActivitysCode, false);
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

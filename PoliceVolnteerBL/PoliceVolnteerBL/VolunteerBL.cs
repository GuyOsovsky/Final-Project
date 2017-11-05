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
        public DataTable GetShifts(DateTime Date,  OperatorType Operator)
        {
            //mask for later filtration
            FieldValue<ShiftsField> Mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, Date.ToShortDateString(), FieldType.DateTime, Operator);
            //get all shifts regarding the volunteer
            DataRowCollection Rows = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ShiftsField>> ShiftsCode = new Queue<FieldValue<ShiftsField>>();
            foreach(DataRow Row in Rows)
            {
                ShiftsCode.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, Row["shiftCode"], FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = ShiftsDAL.GetTable(ShiftsCode, false);
            //filter shifts using date and operator
            ds.Tables[0].DefaultView.RowFilter = Mask.ToString();
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();

            return FilteredTable;

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

        public DataTable GetCourses(DateTime Date, OperatorType Operator)
        {
            FieldValue<CourseField> Mask = new FieldValue<CourseField>(CourseField.CourseDate, Date.ToShortDateString(), FieldType.DateTime, Operator);
            DataRowCollection Rows = CoursesToVolunteerDAL.GetTable(new FieldValue<CoursesToVolunteerField>(CoursesToVolunteerField.PhoneNumber, this.PhoneNumber, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<CourseField>> CourseCode = new Queue<FieldValue<CourseField>>();
            foreach (DataRow Row in Rows)
            {
                CourseCode.Enqueue(new FieldValue<CourseField>(CourseField.CourseCode, Row["CourseCode"], FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = CourseDAL.GetTable(CourseCode, false);
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

        public DataTable GetExpiraedValidities() //לבדוק אפשרות של שינוי תאריכי התראה על ידי המנהל
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
            if(this.Type.Independent == true)
            {
                DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
                if (VolunteersInShift.Count > 1)
                    return;
                ShiftsToVolunteerDAL.AddShift(ShiftCode, this.PhoneNumber, "");
            }
            else
            {
                DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
                if (VolunteersInShift.Count > 2 || VolunteersInShift.Count < 1)
                    return;
                foreach(DataRow VolunteerInShift in VolunteersInShift)
                {
                    VolunteerBL volunteer = new VolunteerBL(VolunteerInShift["PhoneNumber"].ToString());
                    if (volunteer.Type.Independent == false)
                        return;
                }
                ShiftsToVolunteerDAL.AddShift(ShiftCode, this.PhoneNumber, "");
            }
        }
    }
}

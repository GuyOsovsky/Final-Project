﻿using PoliceVolnteerDAL;
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

        /// <summary>
        /// creates a new object and adds it to the db
        /// </summary>
        public VolunteerBL(string phoneNumber, string emergencyNumber, string fName, string lName, DateTime birthDate, string userName, 
            string password, string homeAddress, string homeCity, string emailAddress, string iD, string policeID, string serveCity, int type)
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

        /// <summary>
        /// creates a new object according to a registered volunteer
        /// </summary>
        public VolunteerBL(string phoneNumber)
        {
            DataRow dr;
            try
            {
                dr = VolunteerInfoDAL.GetTable(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber, phoneNumber, Table.VolunteerInfo, FieldType.String, OperatorType.Equals), true).Tables[0].Rows[0];
            }
            catch (Exception e)
            {
                this.PhoneNumber = "";
                return;
            }
            DataRow volunteerInfoRow = VolunteerInfoDAL.GetTable(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber, phoneNumber, Table.VolunteerInfo, FieldType.String, OperatorType.Equals), false).Tables[0].Rows[0];
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
            volunteerInfoRow = VolunteerPoliceInfoDAL.GetTable(new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.PhoneNumber, phoneNumber, Table.VolunteerPoliceInfo, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            this.PoliceID = volunteerInfoRow["PoliceID"].ToString();
            this.ServeCity = volunteerInfoRow["ServeCity"].ToString();
            this.StartDate = DateTime.Parse(volunteerInfoRow["StartDate"].ToString());
            this.Type = new VolunteerTypeBL(int.Parse(volunteerInfoRow["Type"].ToString()));
        }

        /// <summary>
        /// creates a new object according a registered volunteer
        /// </summary>
        public VolunteerBL(string userName, string password)
        {
            DataRow dr;
            try
            {
                Queue<FieldValue<VolunteerInfoDALField>> parameters = new Queue<FieldValue<VolunteerInfoDALField>>();
                parameters.Enqueue(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.UserName, userName, Table.VolunteerInfo, FieldType.String, OperatorType.Equals));
                parameters.Enqueue(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.Password, password, Table.VolunteerInfo, FieldType.String, OperatorType.Equals));
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
            dr = VolunteerPoliceInfoDAL.GetTable(new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.PhoneNumber, this.PhoneNumber, Table.VolunteerPoliceInfo, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            this.PoliceID = dr["PoliceID"].ToString();
            this.ServeCity = dr["ServeCity"].ToString();
            this.StartDate = DateTime.Parse(dr["StartDate"].ToString());
            this.Type = new VolunteerTypeBL(int.Parse(dr["Type"].ToString()));
        }

        /// <summary>
        /// updates a certain field of the volunteer in the db
        /// </summary>
        /// <param name="field">enum of the field in the db</param>
        /// <param name="value">the value to be put in the db</param>
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
                VolunteerInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerInfoDALField>((VolunteerInfoDALField)field, value, Table.VolunteerInfo, valueType, OperatorType.Equals));
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
                VolunteerPoliceInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerPoliceInfoDALField>((VolunteerPoliceInfoDALField)field, value, Table.VolunteerPoliceInfo, valueType, OperatorType.Equals));
            }
        }

        /// <summary>
        /// generates a new dataset of the volunteer
        /// </summary>
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

        /// <summary>
        /// does have birth day today
        /// </summary>
        public bool HasBirthDay()
        {
            return (this.BirthDate.Day == DateTime.Now.Day && this.BirthDate.Month == DateTime.Now.Month);
        }

        /// <summary>
        /// changes the status of a volunteer(active/not active)
        /// </summary>
        public void ChangeStatus(bool newStatus)
        {
            //Is the new status different?
            if (this.Status != newStatus)
            {
                //change status
                VolunteerInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.status, newStatus, Table.VolunteerInfo, FieldType.Boolean, OperatorType.Equals));
                this.Status = newStatus;
            }
        }

        /// <summary>
        /// registers a new car of the volunteer
        /// </summary>
        public void AddNewCar(string CarID)
        {
            CarToVolunteerDAL.AddCar(PhoneNumber, CarID);
        }

        /// <summary>
        /// returns all the registered cars of the volunteer
        /// </summary>
        public DataSet GetCars()
        {
            //get all cars of the volunteer
            return CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, this.PhoneNumber, Table.CarToVolunteer, FieldType.String, OperatorType.Equals));
        }

        /// <summary>
        /// deletes a car from the colunteer collection
        /// </summary>
        public void DeleteCar(string CarID)
        {
            CarToVolunteerDAL.DelCar(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, CarID, Table.CarToVolunteer, FieldType.String, OperatorType.Equals));
        }

        /// <summary>
        /// returns a patrol shift report of a specific car
        /// </summary>
        public DataTable GetCarReports(string carID)
        {
            //get all reports of this car
            DataTable reports = CarsReportsDAL.GetTable(new FieldValue<CarsReportsField>(CarsReportsField.CarID, carID, Table.CarsReports, FieldType.String, OperatorType.Equals)).Tables[0];
            return reports;
        }

        /// <summary>
        /// returns all the reports of the cars
        /// </summary>
        public DataTable GetCarReports()
        {
            //get all reports of volunteer
            Queue<FieldValue<CarsReportsField>> filter = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow id in GetCars().Tables[0].Rows)
            {
                filter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.CarID, id["CarID"].ToString(), Table.CarsReports, FieldType.String, OperatorType.Equals));
            }
            DataTable reports = CarsReportsDAL.GetTable(filter, false).Tables[0];
            return reports;
        }

        /// <summary>
        /// returns all car reports of a specific car with a date and an operand
        /// </summary>
        public DataTable GetCarReports(DateTime date, OperatorType Operator, string carID)
        {
            //get all reports of the car
            DataTable reports = this.GetCarReports(carID);
            //go to shift table
            Queue<FieldValue<ShiftsField>> shiftFilter = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow report in reports.Rows)
            {
                shiftFilter.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, report["ShiftCode"], Table.Shifts, FieldType.Number, OperatorType.Equals));
            }
            DataTable shifts = ShiftsDAL.GetTable(shiftFilter, false).Tables[0];
            //filter unwanted shifts
            FieldValue<ShiftsField> mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, date, Table.Shifts, FieldType.Date, Operator);
            shifts.DefaultView.RowFilter = mask.ToSql();
            shifts = (shifts.DefaultView).ToTable();
            //go to reports table
            Queue<FieldValue<CarsReportsField>> reportsFilter = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow shift in shifts.Rows)
            {
                reportsFilter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shift["ShiftCode"], Table.CarsReports, FieldType.Number, OperatorType.Equals));
            }
            reports = CarsReportsDAL.GetTable(reportsFilter, false).Tables[0];
            return reports;
        }

        /// <summary>
        /// returns all reports of all the cars of the volunteer according to a date and an operand
        /// </summary>
        public DataTable GetCarReports(DateTime date, OperatorType Operator)
        {
            //get all reports of the volunteer
            DataTable reports = this.GetCarReports();
            //go to shift table
            Queue<FieldValue<ShiftsField>> shiftFilter = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow report in reports.Rows)
            {
                shiftFilter.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, report["ShiftCode"], Table.Shifts, FieldType.Number, OperatorType.Equals));
            }
            DataTable shifts = ShiftsDAL.GetTable(shiftFilter, false).Tables[0];
            //filter unwanted shifts
            FieldValue<ShiftsField> mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, date, Table.Shifts, FieldType.Date, Operator);
            shifts.DefaultView.RowFilter = mask.ToSql();
            shifts = (shifts.DefaultView).ToTable();
            //go to reports table
            Queue<FieldValue<CarsReportsField>> reportsFilter = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow shift in shifts.Rows)
            {
                reportsFilter.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shift["ShiftCode"], Table.CarsReports, FieldType.Number, OperatorType.Equals));
            }
            reports = CarsReportsDAL.GetTable(reportsFilter, false).Tables[0];
            return reports;
        }

        /// <summary>
        /// returns all courses participated by the volunteer according to a date and an operand
        /// </summary>
        public DataTable GetCourses(DateTime Date, OperatorType Operator)
        {
            //create filter
            FieldValue<CourseField> Mask = new FieldValue<CourseField>(CourseField.CourseDate, Date, Table.Course, FieldType.Date, Operator);
            //get all reports of volunteer
            DataRowCollection Rows = CoursesToVolunteerDAL.GetTable(new FieldValue<CoursesToVolunteerField>(CoursesToVolunteerField.PhoneNumber, this.PhoneNumber, Table.CoursesToVolunteer, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<CourseField>> CourseCode = new Queue<FieldValue<CourseField>>();
            foreach (DataRow Row in Rows)
            {
                CourseCode.Enqueue(new FieldValue<CourseField>(CourseField.CourseCode, Row["CourseCode"], Table.Course, FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = CourseDAL.GetTable(CourseCode, false);
            //filter unwanted courses
            ds.Tables[0].DefaultView.RowFilter = Mask.ToSql();
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();
            return FilteredTable;
        }

        /// <summary>
        /// signs up a volunteer to a course
        /// </summary>
        public void CourseSignUp(int CourseCode)
        {
            CoursesToVolunteerDAL.AddCoursesToVolunteer(this.PhoneNumber, CourseCode);
        }

        /// <summary>
        /// signs out a volunteer from a course
        /// </summary>
        public void CourseSignOut(int courseCode)
        {
            CourseBL course = new CourseBL(courseCode);
            CoursesToVolunteerDAL.DelCourse(this.PhoneNumber, courseCode);
        }

        /// <summary>
        /// returns all the items the vounteer have taken and not returned and need to return
        /// </summary>
        public DataSet GetItemsInPossession()
        {
            //create parameters for searching the StockToVolunteer table
            Queue<FieldValue<StockToVolunteerField>> parameters = new Queue<FieldValue<StockToVolunteerField>>();
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.PhoneVolunteer, this.PhoneNumber, Table.StockToVolunteer, FieldType.String, OperatorType.Equals));
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.ReturnDate, new DateTime(1999, 1, 1), Table.StockToVolunteer, FieldType.Date, OperatorType.Equals));
            DataRowCollection Recycable = StockDAL.GetTable(new FieldValue<StockField>(StockField.Recyclable, true, Table.Stock, FieldType.Boolean, OperatorType.Equals)).Tables[0].Rows;
            foreach (DataRow item in Recycable)
            {
                parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.ItemID, item["ItemID"], Table.StockToVolunteer, FieldType.Number, OperatorType.NotEquals));
            }
            return StockToVolunteerDAL.GetTable(parameters, true);

        }

        /// <summary>
        /// returns all the items the volunteerhave taken in the past
        /// </summary>
        public DataSet GetItems()
        {
            //create parameters for searching the StockToVolunteer table
            Queue<FieldValue<StockToVolunteerField>> parameters = new Queue<FieldValue<StockToVolunteerField>>();
            parameters.Enqueue(new FieldValue<StockToVolunteerField>(StockToVolunteerField.PhoneVolunteer, this.PhoneNumber, Table.StockToVolunteer, FieldType.String, OperatorType.Equals));
            return StockToVolunteerDAL.GetTable(parameters, true);
        }

        /// <summary>
        /// create a new take request of the volunteer to a specific item
        /// </summary>
        public void TakeItem(int ItemCode, int Amount, DateTime Date)
        {
            StockToVolunteerDAL.AddTransference(this.PhoneNumber, ItemCode, Amount, Date);
        }

        /// <summary>
        /// create a return request for returning an item the volunteer had taken
        /// </summary>
        public void ReturnItem(int TransactionCode)
        {
            StockToVolunteerDAL.ReturnItem(TransactionCode);
        }

        /// <summary>
        /// changes the rank of the volunteer
        /// </summary>
        public void ChangeRank(int RankCode)
        {
            try
            {
                //change object in code
                VolunteerTypeBL NewType = new VolunteerTypeBL(RankCode);
                this.Type = NewType;
                //change object in database
                VolunteerPoliceInfoDAL.UpdateFrom(this.PhoneNumber, new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.Type, RankCode, Table.VolunteerPoliceInfo, FieldType.Number, OperatorType.Equals));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// returns all the validitys the volunteer has
        /// </summary>
        /// <returns></returns>
        public DataSet GetValidities()
        {
            return VolunteerToValidityDAL.GetTable(new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.PhoneNumber, this.PhoneNumber, Table.VolunteerToValidity, FieldType.String, OperatorType.Equals));
        }

        /// <summary>
        /// returns all the validitys the volunteer has to renew
        /// </summary>
        public DataTable GetExpiraedValidities() 
        {
            //get all user's validities
            DataTable AllValidities = this.GetValidities().Tables[0];
            //create mask to filter validities by todays date
            FieldValue<VolunteerToValidityField> Mask = new FieldValue<VolunteerToValidityField>(VolunteerToValidityField.EndDate, DateTime.Now.ToShortDateString(), Table.VolunteerToValidity, FieldType.Date, OperatorType.LowerAndEquals);
            //filter unnececery validities
            AllValidities.DefaultView.RowFilter = Mask.ToSql();
            DataTable FilteredValidities = (AllValidities.DefaultView).ToTable();
            return FilteredValidities;
        }

        /// <summary>
        /// signs the volunteer to a shift
        /// </summary>
        public void ShiftSignUp(int shiftCode)
        {
            //if (this.Type.Independent)
            //{
            //    //get all the volunteers in the shift
            //    DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode, Table.ShiftsToVolunteer, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
            //    if (VolunteersInShift.Count > 1)//if there is no more place in the shift exit
            //        return;
            //    //add him in the shift
            //    ShiftsToVolunteerDAL.AddShift(ShiftCode, this.PhoneNumber, "");
            //}
            //else
            //{
            //    //get all volunteers in the shifts
            //    DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, ShiftCode, Table.ShiftsToVolunteer, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
            //    //if there is no more place or there is already a non independent volunteer in the shift exit
            //    if (VolunteersInShift.Count > 2 || VolunteersInShift.Count < 1)
            //        return;
            //    foreach (DataRow VolunteerInShift in VolunteersInShift)
            //    {
            //        VolunteerBL volunteer = new VolunteerBL(VolunteerInShift["PhoneNumber"].ToString());
            //        if (!volunteer.Type.Independent)
            //            return;
            //    }
            //    //add him to the shift
            //    ShiftsToVolunteerDAL.AddShift(ShiftCode, this.PhoneNumber, "");
            //}
            if (CanSignUpToShift(shiftCode))
            {
                if(this.Type.Independent)
                    ShiftsToVolunteerDAL.AddShift(shiftCode, this.PhoneNumber, "");
                else
                    ShiftsToVolunteerDAL.AddShift(shiftCode, this.PhoneNumber, "");
            }
        }

        public bool CanSignUpToShift(int shiftCode)
        {
            if (this.Type.Independent)
            {
                //get all the volunteers in the shift
                DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, shiftCode, Table.ShiftsToVolunteer, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
                if (VolunteersInShift.Count > 1)//if there is no more place in the shift exit
                    return false;
                //can add him to the shift
                return true;
            }
            else
            {
                //get all volunteers in the shifts
                DataRowCollection VolunteersInShift = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, shiftCode, Table.ShiftsToVolunteer, FieldType.Number, OperatorType.Equals)).Tables[0].Rows;
                //if there is no more place or there is already a non independent volunteer in the shift exit
                if (VolunteersInShift.Count > 2 || VolunteersInShift.Count < 1)
                    return false;
                foreach (DataRow VolunteerInShift in VolunteersInShift)
                {
                    VolunteerBL volunteer = new VolunteerBL(VolunteerInShift["PhoneNumber"].ToString());
                    //if there is a trainee, another trainee can not be added
                    if (!volunteer.Type.Independent)
                        return false;
                }
                //can add him to the shift
                return true;
            }
        }

        /// <summary>
        /// signs out the volunteer from a shift
        /// </summary>
        public void ShiftSignOut(int shiftCode)
        {
            ShiftsToVolunteerDAL.DelShiftToVolunteer(this.PhoneNumber, shiftCode);
        }

        /// <summary>
        /// signs the volunteer for an activitys
        /// </summary>
        public void ActivitySignUp(int ActivityCode)
        {
            ReportsDAL.AddReport(this.PhoneNumber, new DateTime(1999, 1, 1), ActivityCode, "");
        }

        /// <summary>
        /// signs out the volunteer from an activity
        /// </summary>
        public void ActivitySignOut(int ActivityCode)
        {
            ReportsDAL.DelReport(this.PhoneNumber, ActivityCode);
        }

        /// <summary>
        /// creates a new report of shift. if it was a patrol shift with a car the volunteer has to fill in some more data
        /// </summary>
        public void ShiftReport(ShiftBL shift, string comment = "", string carID = "", double distance = 0)
        {
            if (distance != 0)//if the volunteer has entered Car shift info so add the car shift to the database
            {
                //authenticate CarID for volunteer
                if (CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, carID, Table.CarToVolunteer, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["PhoneNumber"].ToString() != this.PhoneNumber)
                    return;
                CarsReportsDAL.AddCarReport(shift.ShiftCode, carID, distance);
            }
            if (comment == "")//if the volunteer has entered comment so add comment to the database
            {
                //update comment
                ShiftsToVolunteerDAL.UpdateComment(this.PhoneNumber, shift.ShiftCode, comment);
            }
        }

        /// <summary>
        /// creates a new activity report
        /// </summary>
        public void ActivityReport(ActivityBL activity, string description, DateTime reportDate)
        {
            ReportBL report = new ReportBL(this.PhoneNumber, activity.ActivityCode);
            report.UpdateDescription(description, reportDate);
        }

        /// <summary>
        /// gets the total distance the volunteer made with all his cars during patrol shifts
        /// </summary>
        public double GetDistance(DateTime FromDate, DateTime ToDate)
        {
            //get all the volunteer's car
            DataRowCollection AllCars = CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, this.PhoneNumber, Table.CarToVolunteer, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            //search for all known shifts the car has been used in
            Queue<FieldValue<CarsReportsField>> ReportParameters = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow Car in AllCars)
            {
                ReportParameters.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.CarID, Car["CarID"], Table.CarsReports, FieldType.String, OperatorType.Equals));
            }
            if (ReportParameters.Count == 0)
                return 0;
            DataTable shifts = CarsReportsDAL.GetTable(ReportParameters, false).Tables[0];
            //filter shifts by date
            Queue<FieldValue<ShiftsField>> ShiftParameters = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow Shift in shifts.Rows)
            {
                ShiftParameters.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, Shift["ShiftCode"], Table.Shifts, FieldType.Number, OperatorType.Equals));
            }
            if (ShiftParameters.Count == 0)
                return 0;
            FieldValue<ShiftsField> Mask1 = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, FromDate, Table.Shifts, FieldType.Date, OperatorType.GreaterAndEquals);
            FieldValue<ShiftsField> Mask2 = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, ToDate, Table.Shifts, FieldType.Date, OperatorType.LowerAndEquals);
            DataTable Filter = ShiftsDAL.GetTable(ShiftParameters, false).Tables[0];
            Filter.DefaultView.RowFilter = Mask1.ToSql();
            Filter = (Filter.DefaultView).ToTable();
            Filter.DefaultView.RowFilter = Mask2.ToSql();
            Filter = (Filter.DefaultView).ToTable();
            //get all needed reports
            ReportParameters = new Queue<FieldValue<CarsReportsField>>();
            foreach (DataRow shiftCode in Filter.Rows)
            {
                ReportParameters.Enqueue(new FieldValue<CarsReportsField>(CarsReportsField.ShiftCode, shiftCode["ShiftCode"], Table.CarsReports, FieldType.Number, OperatorType.Equals));
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

        /// <summary>
        /// returns all shifts the volunteer has done and will do according to a date and operand
        /// </summary>
        public DataTable GetShifts(DateTime Date, OperatorType Operator)
        {
            //mask for later filtration
            FieldValue<ShiftsField> Mask = new FieldValue<ShiftsField>(ShiftsField.DateOfShift, Date, Table.Shifts, FieldType.Date, Operator);
            //get all shifts regarding the volunteer
            DataRowCollection Rows = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, Table.ShiftsToVolunteer, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ShiftsField>> ShiftsCode = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow Row in Rows)
            {
                ShiftsCode.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, Row["shiftCode"], Table.Shifts, FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = ShiftsDAL.GetTable(ShiftsCode, false);
            //filter shifts using date and operator
            ds.Tables[0].DefaultView.RowFilter = Mask.ToSql();
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();

            return FilteredTable;
        }

        /// <summary>
        /// returns all shifts the volunteer has done and will do
        /// </summary>
        public DataTable GetShifts()
        {
            //get all shifts regarding the volunteer
            DataRowCollection Rows = ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, Table.ShiftsToVolunteer, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ShiftsField>> ShiftsCode = new Queue<FieldValue<ShiftsField>>();
            foreach (DataRow Row in Rows)
            {
                ShiftsCode.Enqueue(new FieldValue<ShiftsField>(ShiftsField.ShiftCode, Row["shiftCode"], Table.Shifts, FieldType.Number, OperatorType.Equals));
            }
            return ShiftsDAL.GetTable(ShiftsCode, false).Tables[0];
        }

        /// <summary>
        /// returns all the reports the volunteer did on shifts
        /// </summary>
        public DataTable GetShiftReports()
        {
            return ShiftsToVolunteerDAL.GetTable(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, Table.ShiftsToVolunteer, FieldType.String, OperatorType.Equals)).Tables[0];
        }

        /// <summary>
        /// returns all the reports the volunteer did on shifts according to a date and an operand
        /// </summary>
        public DataTable GetShiftReports(DateTime date, OperatorType Operator)
        {
            //get all shifts by date
            DataTable Shifts = GetShifts(date, Operator);
            //get the desired reports
            Queue<FieldValue<ShiftsToVolunteerField>> shiftReportFilter = new Queue<FieldValue<ShiftsToVolunteerField>>();
            foreach (DataRow shift in Shifts.Rows)
            {
                shiftReportFilter.Enqueue(new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.ShiftCode, shift["ShiftCode"], Table.ShiftsToVolunteer, FieldType.Number, OperatorType.Equals));
            }
            DataTable reports = ShiftsToVolunteerDAL.GetTable(shiftReportFilter, false).Tables[0];
            FieldValue<ShiftsToVolunteerField> Mask = new FieldValue<ShiftsToVolunteerField>(ShiftsToVolunteerField.PhoneNumber, this.PhoneNumber, Table.ShiftsToVolunteer, FieldType.String, OperatorType.Equals);
            reports.DefaultView.RowFilter = Mask.ToSql();
            reports = (reports.DefaultView).ToTable();
            return reports;
        }

        /// <summary>
        /// returns all activitys the volunteeer has done and will do according to a date and an operand
        /// </summary>
        public DataTable GetActivitys(DateTime Date, OperatorType Operator)
        {
            //create parameters to filter the table
            FieldValue<ActivityField> Mask = new FieldValue<ActivityField>(ActivityField.ActivityDate, Date, Table.Activity, FieldType.Date, Operator);
            DataRowCollection Rows = ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, Table.Reports, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ActivityField>> ActivitysCode = new Queue<FieldValue<ActivityField>>();
            foreach (DataRow Row in Rows)
            {
                ActivitysCode.Enqueue(new FieldValue<ActivityField>(ActivityField.ActivityCode, Row["ActivityCode"], Table.Activity, FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = ActivityDAL.GetTable(ActivitysCode, false);
            ds.Tables[0].DefaultView.RowFilter = Mask.ToSql();
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();
            return FilteredTable;
        }

        /// <summary>
        /// returns all activitys the volunteeer has done and will do
        /// </summary>
        public DataTable GetActivitys()
        {
            //create parameters to filter the table
            DataRowCollection Rows = ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, Table.Reports, FieldType.String, OperatorType.Equals)).Tables[0].Rows;
            Queue<FieldValue<ActivityField>> ActivitysCode = new Queue<FieldValue<ActivityField>>();
            foreach (DataRow Row in Rows)
            {
                ActivitysCode.Enqueue(new FieldValue<ActivityField>(ActivityField.ActivityCode, Row["ActivityCode"], Table.Activity, FieldType.Number, OperatorType.Equals));
            }
            DataSet ds = ActivityDAL.GetTable(ActivitysCode, false);
            DataTable FilteredTable = (ds.Tables[0].DefaultView).ToTable();
            return FilteredTable;
        }

        /// <summary>
        /// returns all the reports the volunteer did on activitys
        /// </summary>
        public DataTable GetActivityReports()
        {
            return ReportsDAL.GetTable(new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, Table.Reports, FieldType.String, OperatorType.Equals)).Tables[0];
        }

        /// <summary>
        /// returns all the reports the volunteer did on activitys according to a date and an operand
        /// </summary>
        public DataTable GetActivityReports(DateTime date, OperatorType Operator)
        {
            //get all activities
            DataTable Activities = GetActivitys(date, Operator);
            //filter reports by activities
            Queue<FieldValue<ReportsField>> ActivityFilter = new Queue<FieldValue<ReportsField>>();
            foreach (DataRow activity in Activities.Rows)
            {
                ActivityFilter.Enqueue(new FieldValue<ReportsField>(ReportsField.ActivityCode, activity["ActivityCode"], Table.Reports, FieldType.Number, OperatorType.Equals));
            }
            DataTable reports = ReportsDAL.GetTable(ActivityFilter, false).Tables[0];
            FieldValue<ReportsField> Mask = new FieldValue<ReportsField>(ReportsField.PhoneNumber, this.PhoneNumber, Table.Reports, FieldType.String, OperatorType.Equals);
            reports.DefaultView.RowFilter = Mask.ToSql();
            reports = (reports.DefaultView).ToTable();
            return reports;
        }

        public static DataRow GetVolunteerByCarID(string carID)
        {
            return CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, carID, Table.CarToVolunteer, FieldType.String)).Tables[0].Rows[0];
        }

    }
}

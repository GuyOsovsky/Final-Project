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
        public string CarID { get; set; }
        public VolunteerTypeBL Type { get; set; }
        public bool Status { get; set; }

        public VolunteerBL(string phoneNumber, string emergencyNumber, string fName, string lName, DateTime birthDate, string userName, string password, string homeAddress, string homeCity, string emailAddress, string iD, string policeID, string serveCity, DateTime startDate, int type, string carID = "-1")
        {
            VolunteerInfoDAL.AddVolunteer(phoneNumber, emergencyNumber, fName, lName, birthDate, userName, password, homeAddress, homeCity, emailAddress, iD, policeID, serveCity, startDate, type, carID);
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
            this.CarID = carID;
            this.Type = new VolunteerTypeBL(type);
            this.Status = true;
        }

        public VolunteerBL(string phoneNumber)
        {
            DataRow dr = VolunteerInfoDAL.GetTable(new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber, phoneNumber, FieldType.String)).Tables[0].Rows[0];
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
            dr = VolunteerPoliceInfoDAL.GetTable(new FieldValue<VolunteerPoliceInfoDALField>(VolunteerPoliceInfoDALField.PhoneNumber, phoneNumber, FieldType.String)).Tables[0].Rows[0];
            this.PoliceID = dr["PoliceID"].ToString();
            this.ServeCity = dr["ServeCity"].ToString();
            this.StartDate = DateTime.Parse(dr["StartDate"].ToString());
            this.Type = new VolunteerTypeBL(int.Parse(dr["Type"].ToString()));
            dr = CarToVolunteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.PhoneNumber, phoneNumber, FieldType.String)).Tables[0].Rows[0];
            this.CarID = dr["CarID"].ToString();
        }
    }
}

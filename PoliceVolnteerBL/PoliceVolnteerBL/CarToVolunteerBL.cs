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
    public class CarToVolunteerBL
    {
        public string PhoneNumber { get; set; }
        public string CarID { get; set; }

        public CarToVolunteerBL(string PhoneNumber, string CarID)
        {
            this.PhoneNumber = PhoneNumber;
            this.CarID = CarID;
            CarVolnteerDAL.AddCar(PhoneNumber, CarID);
        }

        public CarToVolunteerBL(string CarID)
        {

            DataSet ds = CarVolnteerDAL.GetTable(new FieldValue<CarVolunteerField>(CarVolunteerField.CarID, CarID, 2));
            this.CarID = CarID;
            this.PhoneNumber = (string)ds.Tables[0].Rows[0]["PhoneNumber"];
        }

    }
}

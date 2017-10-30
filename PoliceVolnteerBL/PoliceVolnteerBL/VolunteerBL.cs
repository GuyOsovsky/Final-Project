using System;
using System.Collections.Generic;
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

    }
}

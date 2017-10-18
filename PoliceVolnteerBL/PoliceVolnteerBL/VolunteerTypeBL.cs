using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerBL
{
    public class VolunteerTypeBL
    {
        public int typeCode { get; set; }
        public string typeName { get; set; }
        public bool permmisionShifts { get; set; }
        public bool permmisionActivity { get; set; }
        public bool permmisionStock { get; set; }
        public bool Independent { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerBL
{
    class VolunteerTypeBL
    {
        private int typeCode;
        private string typeName;
        private bool permmisionShifts;
        private bool permmisionActivity;
        private bool permmisionStock;
        public bool Independent { get; private set; }

        public VolunteerTypeBL()
        {

        }
    }
}

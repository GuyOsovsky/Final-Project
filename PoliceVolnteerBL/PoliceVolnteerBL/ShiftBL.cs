using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerBL
{
    class ShiftBL
    {
        public int ShiftCode { get; set; }
        public int TypeCode { get; set; }
        public DateTime DateOfShift { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Place { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerBL
{
    class StockToVolunteerBL
    {
        public int transferCode { get; set; }
        public string phoneNumber { get; set; }
        public int itemID { get; set; }
        public int amount { get; set; }
        public DateTime borrowDate { get; set; }
        public DateTime returnDate { get; set; }
    }
}

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
    public class TypesToActivityBL
    {
        public List<TypeToActivityBL> TypeToActivityList { get; set; }

        public TypesToActivityBL()
        {
            this.TypeToActivityList = new List<TypeToActivityBL>();
            DataRowCollection drc = TypeToActivityDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                TypeToActivityList.Add(new TypeToActivityBL((int)drc[i]["typeCode"]));
            }
        }
    }
}

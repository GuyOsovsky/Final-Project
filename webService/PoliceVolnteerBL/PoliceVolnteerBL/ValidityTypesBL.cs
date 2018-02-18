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
    public class ValidityTypesBL
    {
        public List<ValidityTypeBL> ValidityTypeList { get; set; }

        //create ValidityTypeList and add all ValidityTypeBL objects 
        public ValidityTypesBL()
        {
            this.ValidityTypeList = new List<ValidityTypeBL>();
            DataRowCollection validityTypesRows = ValidityTypesDAL.GetTable().Tables[0].Rows;
            for (int i = 0; i < validityTypesRows.Count; i++)
            {
                ValidityTypeList.Add(new ValidityTypeBL((int)validityTypesRows[i]["ValidityCode"]));
            }
        }
    }
}

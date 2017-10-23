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
    public class ShiftTypeBL
    {
        public int TypeCode { get; set; }
        public string TypeName { get; set; }

        public ShiftTypeBL(string typeName)
        {
            ShiftsTypesDAL.AddShift(typeName);
            this.TypeCode = (int)ShiftsTypesDAL.GetTable(new FieldValue<ShiftsTypesDAL.ShiftsTypeEnum>(ShiftsTypesDAL.ShiftsTypeEnum.TypeName, typeName, 2)).Tables[0].Rows[0]["typeCode"];
            this.TypeName = typeName;
        }

        public ShiftTypeBL(int typeCode)
        {
            DataRow obj = ShiftsTypesDAL.GetTable(new FieldValue<ShiftsTypesDAL.ShiftsTypeEnum>(ShiftsTypesDAL.ShiftsTypeEnum.typeCode, typeCode.ToString(), 1)).Tables[0].Rows[0];
            this.TypeCode = typeCode;
            this.TypeName = obj["TypeName"].ToString();
        }
    }
}

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
    public class ShiftTypesBL
    {
        public int TypeCode { get; set; }
        public string TypeName { get; set; }

        //build and adding to database
        public ShiftTypesBL(string typeName)
        {
            ShiftsTypesDAL.AddShift(typeName);
            this.TypeCode = (int)ShiftsTypesDAL.GetTable(new FieldValue<ShiftsTypeField>(ShiftsTypeField.TypeName, typeName, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["typeCode"];
            this.TypeName = typeName;
        }

        //build from the database
        public ShiftTypesBL(int typeCode)
        {
            DataRow ShiftsTypesRow = ShiftsTypesDAL.GetTable(new FieldValue<ShiftsTypeField>(ShiftsTypeField.typeCode, typeCode, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.TypeCode = typeCode;
            this.TypeName = ShiftsTypesRow["TypeName"].ToString();
        }
    }
}

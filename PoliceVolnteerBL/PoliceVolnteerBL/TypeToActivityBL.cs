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
    public class TypeToActivityBL
    {
        public int TypeCode { get; set; }
        public string TypeName { get; set; }


        public TypeToActivityBL(string typeName)
        {
            TypeToActivityDAL.AddTypeToActivity(typeName);
            DataRow obj = TypeToActivityDAL.GetTable(new FieldValue<TypeToActivityField>(TypeToActivityField.typeName, typeName, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0];
            TypeName = typeName;
            TypeCode = int.Parse(obj["typeCode"].ToString());
        }

        public TypeToActivityBL(int typeCode)
        {
            DataRow obj = TypeToActivityDAL.GetTable(new FieldValue<TypeToActivityField>(TypeToActivityField.typeCode, typeCode.ToString(), FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0];
            this.TypeCode = typeCode;
            this.TypeName = obj["typeName"].ToString();
        }
    }
}

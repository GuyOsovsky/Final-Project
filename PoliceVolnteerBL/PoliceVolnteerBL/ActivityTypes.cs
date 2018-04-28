using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PoliceVolnteerDAL;

namespace PoliceVolnteerBL
{
    public class ActivityTypes
    {
        public int TypeCode { get; set; }
        public string TypeName { get; set; }

        /// <summary>
        /// creates a new object with a certain code
        /// </summary>
        public ActivityTypes(int typeCode)
        {
            this.TypeCode = typeCode;
            this.TypeName = TypeToActivityDAL.GetTable(new FieldValue<TypeToActivityField>(TypeToActivityField.typeCode, typeCode, Table.TypeToActivity, FieldType.Number, OperatorType.Equals)).Tables[0].Rows[0]["typeCode"].ToString();
        }

        /// <summary>
        /// creates a new type to the db
        /// </summary>
        public ActivityTypes(string name)
        {
            TypeToActivityDAL.AddTypeToActivity(name);
            this.TypeName = name;
            this.TypeCode = int.Parse(TypeToActivityDAL.GetTable(new FieldValue<TypeToActivityField>(TypeToActivityField.typeName, name, Table.TypeToActivity, FieldType.String, OperatorType.Equals)).Tables[0].Rows[0]["typeName"].ToString());
        }
    }
}

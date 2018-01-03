using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerDAL
{
    //which type is the input
    public enum FieldType { Number, String, Boolean, Date, Time }
    //operator for filtertion
    public enum OperatorType { Equals, Greater, Lower, GreaterAndEquals, LowerAndEquals, NotEquals}
    ///
    public class FieldValue<T> //where T : enum
    {
        private T field;
        private object value;
        private FieldType typeDB;
        private OperatorType operatorType;

        public T Field
        {
            get { return this.field; }
        }
        public string Value
        {
            get { return this.value.ToString(); }
        }
        public FieldType TypeDB
        {
            get { return this.typeDB; }
        }
        public OperatorType OperatorType
        {
            get { return this.operatorType; }
        }

        public FieldValue(T e, object v, FieldType type, OperatorType Operator) 
        {
            field = e;
            value = v;
            typeDB = type;
            operatorType = Operator;
        }
        
        //to string in a format adaptive for SQL
        public override string ToString()
        {
            //add field
            string ret = "[" + this.field + "]";
            //add operator
            switch (operatorType)
            {
                case OperatorType.Equals:
                    ret += "=";
                    break;
                case OperatorType.Greater:
                    ret += ">";
                    break;
                case OperatorType.Lower:
                    ret += "<";
                    break;
                case OperatorType.GreaterAndEquals:
                    ret += ">=";
                    break;
                case OperatorType.LowerAndEquals:
                    ret += "<=";
                    break;
                case OperatorType.NotEquals:
                    ret += "<>";
                    break;
            }
            //add value according to format syntax
            if (typeDB == FieldType.String)
                ret += "'";
            if (typeDB == FieldType.Date | typeDB == FieldType.Time)
                ret += "#";
            if (typeDB == FieldType.Date)
            {
                ret += ((DateTime)value).Month + "/" + ((DateTime)value).Day + "/" + ((DateTime)value).Year;
            }
            else
            {
                ret += value.ToString();
            }
            if (typeDB == FieldType.String)
                ret += "'";
            if (typeDB == FieldType.Date | typeDB == FieldType.Time)
                ret += "#";
            return ret;
        }
    }
}

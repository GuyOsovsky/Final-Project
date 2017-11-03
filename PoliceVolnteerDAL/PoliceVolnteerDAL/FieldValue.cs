using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerDAL
{
    public enum FieldType { Number, String, Boolean, DateTime }
    public enum OperatorType { Equals, Greater, Lower, GreaterAndEquals, LowerAndEquals, NotEquals}
    ///
    public class FieldValue<T> //where T : enum
    {
        private T field;
        private string value;
        private FieldType typeDB;
        private OperatorType operatorType;
        public T Field
        {
            get { return this.field; }
        }
        public string Value
        {
            get { return this.value; }
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
            value = v.ToString();
            typeDB = type;
            operatorType = Operator;
        }
        
        public override string ToString()
        {
            string ret = "[" + this.field + "]";
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
            if (typeDB == FieldType.String)
                ret += "'";
            if (typeDB == FieldType.DateTime)
                ret += "#";
            ret += value;
            if (typeDB == FieldType.String)
                ret += "'";
            if (typeDB == FieldType.DateTime)
                ret += "#";
            return ret;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerDAL
{
    public enum FieldType { Number, String, Boolean, DateTime }
    public class FieldValue<T>
    {
        private T Field;
        private string Value;
        private FieldType TypeDB;

        /// <summary>
        /// 1 = number; 2 = string;3 = boolean; 4 = dateTime</summary>
        public FieldValue(T e, string v, FieldType type) //"[" + field + "]=" + value
        {
            Field = e;
            Value = v;
            TypeDB = type;
        }
        public override string ToString()
        {
            string ret = "[" + this.Field + "]=";
            if (TypeDB == FieldType.String)
                ret += "'";
            if (TypeDB == FieldType.DateTime)
                ret += "#";
            ret += Value;
            if (TypeDB == FieldType.String)
                ret += "'";
            if (TypeDB == FieldType.DateTime)
                ret += "#";
            return ret;
            //return "[" + this.Field + "]='" + Value + "'";
        }
    }
}

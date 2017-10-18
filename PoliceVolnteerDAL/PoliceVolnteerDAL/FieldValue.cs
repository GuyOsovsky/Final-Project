using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceVolnteerDAL
{
    public class FieldValue<T>
    {
        private T Field;
        private string Value;
        private int TypeDB;

        /// <summary>
        /// 1 = number; 2 = string;3 = boolean; 4 = dateTime</summary>
        public FieldValue(T e, string v, int type)
        {
            Field = e;
            Value = v;
            TypeDB = type;
        }
        public override string ToString()
        {
            string ret = "[" + this.Field + "]=";
            if (TypeDB == 2)
                ret += "'";
            if (TypeDB == 4)
                ret += "#";
            ret += Value;
            if (TypeDB == 2)
                ret += "'";
            if (TypeDB == 4)
                ret += "#";
            return ret;
            //return "[" + this.Field + "]='" + Value + "'";
        }
    }
}

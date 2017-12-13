using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    public class FileTypesDAL
    {
        public static DataSet GetTable()
        {
            return OleDbHelper2.Fill("select * from FileTypes", "FileTypes");
        }
    }
}

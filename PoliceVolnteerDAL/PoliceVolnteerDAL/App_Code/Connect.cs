using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoliceVolnteerDAL
{
    class Connect
    {
        private static string filename = "database police.accdb";
        public static string GetConnectionString()
        {
            //string path = System.IO.Directory.GetCurrentDirectory();
            //path = path.Remove(path.Length - 1 - 9 - 34);
            ////return @"provider=Microsoft.ACE.OLEDB.12.0; Data source=C:\Users\Guy.DESKTOP-AOI7D6B\Desktop\שרותי רשת\פרוייקט גמר\PoliceVolnteerDAL\PoliceVolnteerDAL\App_Data\database police.accdb";
            //return @"provider=Microsoft.ACE.OLEDB.12.0; Data source=" + path + @"\PoliceVolnteerDAL\PoliceVolnteerDAL\App_Data\database police.accdb";

            return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""|DataDirectory|\" + filename + @""";Persist Security Info=True";

            //***
            //return @"provider =Microsoft.ACE.OLEDB.12.0; Data source=C:\DrivingSchoolPro\DrivingSchoolDal\DrivingSchoolDal\App_Data\AtMaDb11.accdb";
            // return @"provider=Microsoft.ACE.OLEDB.12.0; Data source= C:\DrivingSchoolPro\DrivingSchoolDal\DrivingSchoolDal\App_Data\AtMaDb111.accdb";
            //  return @"provider=Microsoft.Jet.OLEDB.4.0; Data source= C:\DrivingSchoolPro\DrivingSchoolDal\DrivingSchoolDal\App_Data\AtMaDb111.mdb";

            //********* string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;data source=" + path;  //mdb  עבור סיומת
            //********* string connString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source=" + path;  // accdb עבור סיומת
        }
    }
}

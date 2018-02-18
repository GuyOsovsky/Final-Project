using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerDAL
{
    class Program
    {
        static void Main(string[] args)
        {
            //VolunteerInfoDAL.DelAllUsers();
            //ValidityTypesDAL.AddNewValidity("first");
            //ValidityTypesDAL.AddNewValidity("second");
            //ValidityTypesDAL.AddNewValidity("third");
            //VolunteerInfoDAL.AddVolunteer("a", "a", "a", "a", new DateTime(2000, 5, 5), "a", "a", "a", "a", "a", "a", "a", "a", new DateTime(2000, 5, 5), 1);
            //VolunteerToValidityDAL.AddValidityToVolunteer("a", 2);
            VolunteerInfoDAL.UpdateFrom("b",new FieldValue<VolunteerInfoDALField>(VolunteerInfoDALField.PhoneNumber,"g",FieldType.String,OperatorType.Equals));
            Console.WriteLine("work");
            Console.ReadKey();
        }
    }
}
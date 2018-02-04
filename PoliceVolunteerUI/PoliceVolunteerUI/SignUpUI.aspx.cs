using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using PoliceVolnteerBL;

namespace PoliceVolunteerUI
{
    public partial class SignUpUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCitiesHome();
                LoadCitiesServe();
                LoadTypes();
            }
        }

        private void LoadCitiesHome()
        {
            //load home city input xml
            string lang = Request.UserLanguages.Contains("he-IL") ? "Heb" : "En";
            XmlDocument doc = new XmlDocument();
            doc.Load("http://img2.timg.co.il/forums/1_102884894.xml");
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            HomeCityIN.DataSource = cities.Cast<XmlNode>().Select(node => node.Attributes[lang].Value).ToArray();
            HomeCityIN.DataBind();

            //load serve city input xml
            
        }
        private void LoadCitiesServe()
        {
            string lang = Request.UserLanguages.Contains("he-IL") ? "Heb" : "En";
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("\\Resources\\ServeCity.xml"));
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            ServeCityIN.DataSource = cities.Cast<XmlNode>().Select(node => node.Attributes[lang].Value).ToArray();
            ServeCityIN.DataBind();
        }

        private void LoadTypes()
        {
            //load types from BL
            Queue<string> types = new Queue<string>();
            VolunteerTypesBL typeQueue = new VolunteerTypesBL();
            //get all names of types
            foreach (VolunteerTypeBL type in typeQueue.VolunteerTypeList)
            {
                TypeIN.Items.Add(new ListItem(type.TypeName, type.TypeCode.ToString()));
            }
            //load types into control
            TypeIN.DataBind();
        }
        protected void Submit(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string phoneNumber = PhoneNumberIN.Text;
                string emergencyNumber = EmergencyNumberIN.Text;
                string fName = FNameIN.Text;
                string lName = LNameIN.Text;
                DateTime birthDate = DateTime.ParseExact(BirthDateIN.Text, "yyyy-M-d", System.Globalization.CultureInfo.InvariantCulture);
                string userName = UserNameIN.Text;
                string password = PasswordIN.Text;
                string homeAdress = HomeAdressIN.Text;
                string homeCity = HomeCityIN.Text;
                string email = EmailIN.Text;
                string id = IDIN.Text;
                string policeId = PoliceIDIN.Text;
                string serveCity = ServeCityIN.Text;
                int type = int.Parse(TypeIN.Text);
                new VolunteerBL(phoneNumber, emergencyNumber, fName, lName, birthDate, userName, password, homeAdress, homeCity, email, id, policeId, serveCity, type);
            }
        }
        
    }
}
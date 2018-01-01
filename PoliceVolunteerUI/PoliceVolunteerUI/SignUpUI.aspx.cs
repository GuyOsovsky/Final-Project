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
                LoadCities();
                LoadTypes();
            }
        }

        private void LoadCities()
        {
            //load home city input xml
            string lang = Request.UserLanguages.Contains("he-IL") ? "Heb" : "En";
            XmlDocument doc = new XmlDocument();
            doc.Load("http://img2.timg.co.il/forums/1_102884894.xml");
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            HomeCityIN.DataSource = cities.Cast<XmlNode>().Select(node => node.Attributes[lang].Value).ToArray();
            HomeCityIN.DataBind();

            //load serve city input xml
            doc = new XmlDocument();
            doc.Load(Server.MapPath("\\Resources\\ServeCity.xml"));
            cities = doc.DocumentElement.SelectNodes("City");
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
                types.Enqueue(type.TypeName);
            }
            //load types into control
            TypeIN.DataSource = types;
            TypeIN.DataBind();
        }
    }
}
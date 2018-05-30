using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PoliceVolnteerBL;
using PoliceVolnteerDAL;
using System.Data;
using System.Xml;

namespace PoliceVolunteerUI
{
    public partial class EditUserSettingsUI : System.Web.UI.Page
    {
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            if (!IsPostBack)
            {
                FillUsersToSearch();
            }
            LoadCitiesHome();
            LoadCitiesServe();
            Loadstatus();
            LoadTypes();
            FillUserSettings();
        }

        protected void FillUsersToSearch()
        {
            //clear list
            SearchUser.Items.Clear();
            //load activitys
            VolunteersBL volunteers = new VolunteersBL(false);
            //add a blank space
            SearchUser.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            //add all activitys to the list
            foreach (VolunteerBL volunteer in volunteers.VolunteerList)
            {
                SearchUser.Items.Add(new ListItem(volunteer.FName + " " + volunteer.LName, volunteer.PhoneNumber));
            }
            SearchUser.DataBind();
        }

        private void LoadTypes()
        {
            //load types from BL
            Queue<string> types = new Queue<string>();
            VolunteerTypesBL typeQueue = new VolunteerTypesBL();
            //get all names of types
            foreach (VolunteerTypeBL type in typeQueue.VolunteerTypeList)
            {
                Type.Items.Add(new ListItem(type.TypeName, type.TypeCode.ToString()));
            }
            //load types into control
            Type.DataBind();
        }

        protected void FillUserSettings()
        {
            if (SearchUser.SelectedValue == "")
            {
                PhoneNumber.Text = "";
                EmergencyPhoneNumber.Text = "";
                FName.Text = "";
                LName.Text = "";
                HomeAdress.Text = "";
                Email.Text = "";
                PoliceID.Text = "";
                ServeCity.SelectedIndex = -1;
                Status.SelectedIndex = -1;
                HomeCity.SelectedIndex = -1;
                return;
            }
            VolunteerBL volunteer = new VolunteerBL(SearchUser.SelectedValue);
            PhoneNumber.Text = volunteer.PhoneNumber;
            EmergencyPhoneNumber.Text = volunteer.EmergencyNumber;
            FName.Text = volunteer.FName;
            LName.Text = volunteer.LName;
            HomeAdress.Text = volunteer.HomeAddress;
            Email.Text = volunteer.EmailAddress;
            PoliceID.Text = volunteer.PoliceID;
            int serveCityIndex = -1;
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("\\Resources\\ServeCity.xml"));
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            string[] serveCitys = cities.Cast<XmlNode>().Select(node => node.Attributes["En"].Value).ToArray();
            for (int i = 0; i < serveCitys.Length; i++)
            {
                if (volunteer.HomeCity.Equals(serveCitys[i]))
                {
                    serveCityIndex = i;
                    break;
                }
            }
            ServeCity.SelectedIndex = serveCityIndex;
            Status.SelectedIndex = int.Parse(volunteer.Status ? "0" : "1");
            int homeCityIndex = -1;
            doc = new XmlDocument();
            doc.Load("http://img2.timg.co.il/forums/1_102884894.xml");
            cities = doc.DocumentElement.SelectNodes("City");
            string[] citys = cities.Cast<XmlNode>().Select(node => node.Attributes["En"].Value).ToArray();
            for (int i = 0; i < citys.Length; i++)
            {
                if (volunteer.HomeCity.Equals(citys[i]))
                {
                    homeCityIndex = i;
                    break;
                }
            }
            HomeCity.SelectedIndex = homeCityIndex;
            Type.Items.Clear();
            VolunteerTypesBL allTypes = new VolunteerTypesBL();
            int typeIndex;
            for(typeIndex = 0; typeIndex < allTypes.VolunteerTypeList.Count; typeIndex++)
            {
                Type.Items.Add(new ListItem(allTypes.VolunteerTypeList[typeIndex].TypeName, allTypes.VolunteerTypeList[typeIndex].TypeCode.ToString()));
                if (allTypes.VolunteerTypeList[typeIndex].TypeCode == volunteer.Type.TypeCode)
                    Type.SelectedIndex = typeIndex;
            }
            Type.DataBind();
        }

        //load serve city input xml
        private void LoadCitiesServe()
        {
            string lang = Request.UserLanguages.Contains("he-IL") ? "Heb" : "En";
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("\\Resources\\ServeCity.xml"));
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            ServeCity.DataSource = cities.Cast<XmlNode>().Select(node => node.Attributes[lang].Value).ToArray();
            ServeCity.DataBind();
        }

        private void LoadCitiesHome()
        {
            //load home city input xml
            string lang = "Heb";
            XmlDocument doc = new XmlDocument();
            doc.Load("http://img2.timg.co.il/forums/1_102884894.xml");
            XmlNodeList cities = doc.DocumentElement.SelectNodes("City");
            HomeCity.DataSource = cities.Cast<XmlNode>().Select(node => node.Attributes[lang].Value).ToArray();
            HomeCity.DataBind();
        }

        private void Loadstatus()
        {
            Status.Items.Clear();
            Status.Items.Add(new ListItem("כן", "true"));
            Status.Items.Add(new ListItem("לא", "false"));
            Status.DataBind();
        }

        protected void updateVolunteer(object sender, EventArgs e)
        {
            object field = null;
            string value = "";
            switch (((WebControl)sender).ID.ToString())
            {
                case "Status":
                    field = VolunteerInfoDALField.status;
                    break;
                case "ServeCity":
                    field = VolunteerPoliceInfoDALField.ServeCity;
                    break;
                case "Type":
                    field = VolunteerPoliceInfoDALField.Type;
                    break;
                default:
                    break;
            }
            if (sender is TextBox)
            {
                value = ((TextBox)sender).Text;
            }
            else if (sender is DropDownList)
            {
                value = ((DropDownList)sender).SelectedValue;
            }
            (new VolunteerBL(SearchUser.SelectedValue)).UpdateVolunteer(field, value);
        }
    }
}
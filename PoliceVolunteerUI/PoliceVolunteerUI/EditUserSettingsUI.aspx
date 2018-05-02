<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="EditUserSettingsUI.aspx.cs" Inherits="PoliceVolunteerUI.EditUserSettingsUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:DropDownList ID="SearchUser" runat="server"></asp:DropDownList>
        <asp:Button ID="UpdateGridView" runat="server" Text="חפש" OnClick="Page_LoadComplete"></asp:Button>
        <div class="col-sm-4">
            <asp:Panel ID="UserSettings" runat="server">
                <asp:Label ID="PhoneNumberLbl" runat="server" Text="מספר טלפון:"></asp:Label>
                <br />
                <asp:TextBox ID="PhoneNumber" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:Label ID="EmergencyPhoneNumberLbl" runat="server" Text="מספר חירום:"></asp:Label>
                <br />
                <asp:TextBox ID="EmergencyPhoneNumber" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:Label ID="FNameLbl" runat="server" Text="שם פרטי:"></asp:Label>
                <br />
                <asp:TextBox ID="FName" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:Label ID="LNameLbl" runat="server" Text="שם משפחה:"></asp:Label>
                <br />
                <asp:TextBox ID="LName" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:label ID="HomeAddresLbl" runat="server" Text="כתובת מגורים:"></asp:label>
                <br />
                <asp:TextBox ID="HomeAdress" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:label ID="HomeCityLbl" runat="server" Text="עיר מגורים:">עיר מגורים:</asp:label>
                <br />
                <asp:DropDownList ID="HomeCity" runat="server" dir="rtl" Enabled="false"></asp:DropDownList>
                <br />
                <asp:label ID="EmailLbl" runat="server" Text="אימייל:"></asp:label>
                <br />
                <asp:TextBox ID="Email" runat="server" TextMode="Email" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:label ID="StatusLbl" runat="server" Text="סטטוס פעילות:"></asp:label>
                <br />
                <asp:DropDownList ID="Status" runat="server" CssClass="form-control" dir="rtl" OnTextChanged="updateVolunteer"></asp:DropDownList>
                <br />
                <asp:label ID="PoliceIDLbl" runat="server" Text="מספר מזהה במשטרה"></asp:label>
                <br />
                <asp:TextBox ID="PoliceID" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:label ID="ServeCityLbl" runat="server" Text="עיר שירות:" OnTextChanged="updateVolunteer"></asp:label>
                <br />
                <asp:DropDownList ID="ServeCity" runat="server" CssClass="form-control" dir="rtl"></asp:DropDownList>
                <br />
                <asp:Button ID="updateButton" runat="server" Text="עדכן"></asp:Button>
            </asp:Panel>
        </div>
    </center>
</asp:Content>

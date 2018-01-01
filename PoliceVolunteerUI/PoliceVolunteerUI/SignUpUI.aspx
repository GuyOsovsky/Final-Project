<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" Inherits="PoliceVolunteerUI.SignUpUI" Codebehind="SignUpUI.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/signIn.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center" class="container-fluid">
        <form class="form-inline">
            <div class="form-group" >
                <label for="PhoneNumberIN">מספר טלפון:</label>
                <br />
                <asp:TextBox ID="PhoneNumberIN" runat="server" TextMode="Phone" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="EmergencyNumberIN">מספר חירום:</label>
                <br />
                <asp:TextBox ID="EmergencyNumberIN" runat="server" TextMode="Phone" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="FNameIN">שם פרטי:</label>
                <br />
                <asp:TextBox ID="FNameIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="LNameIN">שם משפחה:</label>
                <br />
                <asp:TextBox ID="LNameIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="BirthDateIN">תאריך לידה:</label>
                <br />
                <asp:TextBox ID="BirthDateIN" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="UserNameIN">שם משתמש:</label>
                <br />
                <asp:TextBox ID="UserNameIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="PasswordIN">סיסמא:</label>
                <br />
                <asp:TextBox ID="PasswordIN" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="HomeAdressIN">כתובת מגורים:</label>
                <br />
                <asp:TextBox ID="HomeAdressIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="HomeCityIN">עיר מגורים:</label>
                <br />
                <asp:DropDownList ID="HomeCityIN" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group" >
                <label for="EmailIN">אימייל:</label>
                <br />
                <asp:TextBox ID="EmailIN" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="IDIN">תעודת זהות:</label>
                <br />
                <asp:TextBox ID="IDIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="PoliceIDIN">מספר מזהה במשטרה:</label>
                <br />
                <asp:TextBox ID="PoliceIDIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" >
                <label for="ServeCityIN">עיר שירות:</label>
                <br />
                <asp:DropDownList ID="ServeCityIN" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group" >
                <label for="TypeIN">דרגה:</label>
                <br />
                <asp:DropDownList ID="TypeIN" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <%--<div>
                <label>
                    <input type="checkbox">
                    Remember me
                </label>
            </div>
            <button type="submit" class="btn btn-default">Submit</button>--%>
        </form>
    </div>
</asp:Content>
<%--    public string PhoneNumber { get; set; }
        public string EmergencyNumber { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string EmailAddress { get; set; }
        public string ID { get; set; }
        public string PoliceID { get; set; }
        public string ServeCity { get; set; }
        public DateTime StartDate { get; set; }
        public VolunteerTypeBL Type { get; set; }
        public bool Status { get; set; }--%>


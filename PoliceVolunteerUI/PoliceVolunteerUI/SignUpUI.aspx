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
                <asp:RequiredFieldValidator ID="PhoneNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PhoneNumberIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PhoneNumberRegularExpressionV" runat="server" ErrorMessage="זהו לא מספר טלפון תקין" ControlToValidate="PhoneNumberIN" ValidationExpression="\d[0-9]{9}"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="EmergencyNumberIN">מספר חירום:</label>
                <br />
                <asp:TextBox ID="EmergencyNumberIN" runat="server" TextMode="Phone" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmergencyNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="EmergencyNumberIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmergencyNumberRegularExpressionV" runat="server" ErrorMessage="זהו לא מספר טלפון תקין" ControlToValidate="EmergencyNumberIN" ValidationExpression="\d[0-9]{9}"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="FNameIN">שם פרטי:</label>
                <br />
                <asp:TextBox ID="FNameIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="FNameIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="FNameRegularExpressionV" runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w{2-20}" ControlToValidate="FNameIN"></asp:RegularExpressionValidator>

            </div>
            <div class="form-group" >
                <label for="LNameIN">שם משפחה:</label>
                <br />
                <asp:TextBox ID="LNameIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="LNameIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="LNameRegularExpressionV" runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w{2-20}" ControlToValidate="LNameIN"></asp:RegularExpressionValidator>

            </div>
            <div class="form-group" >
                <label for="BirthDateIN">תאריך לידה:</label>
                <br />
                <asp:TextBox ID="BirthDateIN" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="BirthDateRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="BirthDateIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="BirthDateRegularExpressionV" runat="server" ErrorMessage="זה לא תאריך תקין" ControlToValidate="BirthDateIN" ValidationExpression="\d{4}\-\d{2}\-\d{2}"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="UserNameIN">שם משתמש:</label>
                <br />
                <asp:TextBox ID="UserNameIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="UserNameIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="UserNameRegularExpressionV" runat="server" ErrorMessage="זה לא שם משתמש לא תקין" ControlToValidate="UserNameIN" ValidationExpression="\w[a-z,A-Z]+\d+\w[a-z,A-Z]*"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="PasswordIN">סיסמא:</label>
                <br />
                <asp:TextBox ID="PasswordIN" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PasswordIN"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PasswordRegularExpressionV" runat="server" ErrorMessage="זאת לא סיסמא תקינה" ControlToValidate="PasswordIN" ValidationExpression="\w[a-z,A-Z]+\d+\w[a-z,A-Z]*"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="passwordCMP">בבקשה חזור על הסיסמא:</label>
                <br />
                <asp:TextBox ID="passwordCMP" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="passwordCMPRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="passwordCMP"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="passwordCMPCompareV" runat="server" ErrorMessage="הסיסמאות אינן תואמות" ControlToCompare="PasswordIN" ControlToValidate="passwordCMP"></asp:CompareValidator>
            </div>
            <div class="form-group" >
                <label for="HomeAdressIN">כתובת מגורים:</label>
                <br />
                <asp:TextBox ID="HomeAdressIN" runat="server" TextMode="SingleLine" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="HomeAdressRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeAdressIN"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group" >
                <label for="HomeCityIN">עיר מגורים:</label>
                <br />
                <asp:DropDownList ID="HomeCityIN" runat="server" CssClass="form-control"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="HomeCityRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeCityIN"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group" >
                <label for="EmailIN">אימייל:</label>
                <br />
                <asp:TextBox ID="EmailIN" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeCityIN"></asp:RequiredFieldValidator>
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
            <div class="form-group" >
                <asp:Button ID="submit" runat="server" OnClick="Submit" Text="שלח" />
                <%--<asp:Button ID="submit" runat="server" Text="שלח" OnClick="Submit"></asp:Button>--%>
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
        public VolunteerTypeBL Type { get; set; }--%>


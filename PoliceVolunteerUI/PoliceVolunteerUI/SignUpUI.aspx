<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" Inherits="PoliceVolunteerUI.SignUpUI" Codebehind="SignUpUI.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/SignUpValidation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align:center" class="container-fluid">
        <form class="form-inline">
            <div class="form-group" >
                <label for="PhoneNumberIN">מספר טלפון:</label>
                <br />
                <asp:TextBox ID="PhoneNumberIN" runat="server" TextMode="Phone" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PhoneNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PhoneNumberIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PhoneNumberRegularExpressionV" runat="server" ErrorMessage="זהו לא מספר טלפון תקין" ControlToValidate="PhoneNumberIN" ValidationExpression="\d[0-9]{9}" ValidationGroup="SignIn"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="EmergencyNumberIN">מספר חירום:</label>
                <br />
                <asp:TextBox ID="EmergencyNumberIN" runat="server" TextMode="Phone" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmergencyNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="EmergencyNumberIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmergencyNumberRegularExpressionV" runat="server" ErrorMessage="זהו לא מספר טלפון תקין" ControlToValidate="EmergencyNumberIN" ValidationExpression="\d[0-9]{9}" ValidationGroup="SignIn"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="FNameIN">שם פרטי:</label>
                <br />
                <asp:TextBox ID="FNameIN" runat="server" TextMode="SingleLine" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="FNameIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="FNameRegularExpressionV" runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w{2-20}" ControlToValidate="FNameIN" ValidationGroup="SignIn"></asp:RegularExpressionValidator>

            </div>
            <div class="form-group" >
                <label for="LNameIN">שם משפחה:</label>
                <br />
                <asp:TextBox ID="LNameIN" runat="server" TextMode="SingleLine" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="LNameIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="LNameRegularExpressionV" runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w{2-20}" ControlToValidate="LNameIN" ValidationGroup="SignIn"></asp:RegularExpressionValidator>

            </div>
            <div class="form-group" >
                <label for="BirthDateIN">תאריך לידה:</label>
                <br />
                <asp:TextBox ID="BirthDateIN" runat="server" TextMode="Date" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="BirthDateRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="BirthDateIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="BirthDateRegularExpressionV" runat="server" ErrorMessage="זה לא תאריך תקין" ControlToValidate="BirthDateIN" ValidationExpression="\d{4}\-\d{2}\-\d{2}" ValidationGroup="SignIn"></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="DateCustomV" runat="server" ErrorMessage="תאריך לא חוקי" ControlToValidate="BirthDateIN" ClientValidationFunction="DateValidation" ValidationGroup="SignIn"></asp:CustomValidator>
            </div>
            <div class="form-group" >
                <label for="UserNameIN">שם משתמש:</label>
                <br />
                <asp:TextBox ID="UserNameIN" runat="server" TextMode="SingleLine" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="UserNameIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="UserNameRegularExpressionV" runat="server" ErrorMessage="זה לא שם משתמש לא תקין" ControlToValidate="UserNameIN" ValidationExpression="\w[a-z,A-Z]+\d+\w[a-z,A-Z]*" ValidationGroup="SignIn"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="PasswordIN">סיסמא:</label>
                <br />
                <asp:TextBox ID="PasswordIN" runat="server" TextMode="Password" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PasswordIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PasswordRegularExpressionV" runat="server" ErrorMessage="זאת לא סיסמא תקינה" ControlToValidate="PasswordIN" ValidationExpression="\w[a-z,A-Z]+\d+\w[a-z,A-Z]*" ValidationGroup="SignIn"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="PasswordCMP">בבקשה חזור על הסיסמא:</label>
                <br />
                <asp:TextBox ID="PasswordCMP" runat="server" TextMode="Password" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordCMPRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PasswordCMP" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="PasswordCMPCompareV" runat="server" ErrorMessage="הסיסמאות אינן תואמות" ControlToCompare="PasswordIN" ControlToValidate="PasswordCMP" ValidationGroup="SignIn"></asp:CompareValidator>
            </div>
            <div class="form-group" >
                <label for="HomeAdressIN">כתובת מגורים:</label>
                <br />
                <asp:TextBox ID="HomeAdressIN" runat="server" TextMode="SingleLine" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="HomeAdressRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeAdressIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group" >
                <label for="HomeCityIN">עיר מגורים:</label>
                <br />
                <asp:DropDownList ID="HomeCityIN" runat="server" CssClass="form-control" dir="rtl"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="HomeCityRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeCityIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group" >
                <label for="EmailIN">אימייל:</label>
                <br />
                <asp:TextBox ID="EmailIN" runat="server" TextMode="Email" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="EmailIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group" >
                <label for="IDIN">תעודת זהות:</label>
                <br />
                <asp:TextBox ID="IDIN" runat="server" TextMode="SingleLine" CssClass="form-control" ClientIDMode="Static" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="IDRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="IDIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="IDRegularExpressionV" runat="server" ErrorMessage="אורך תעודת זהות לא מתאים" ValidationGroup="SignIn" ValidationExpression="\d{9}" ControlToValidate="IDIN"></asp:RegularExpressionValidator> <%--fix the validation group--%>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="תעודת זהות לא תקינה" ControlToValidate="IDIN" ClientValidationFunction="idValidation" ValidationGroup="SignIn"></asp:CustomValidator>
            </div>
            <div class="form-group" >
                <label for="PoliceIDIN">מספר מזהה במשטרה:</label>
                <br />
                <asp:TextBox ID="PoliceIDIN" runat="server" TextMode="SingleLine" CssClass="form-control" dir="rtl"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PoliceIDRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PoliceIDIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PoliceIDRegularExpressionV" runat="server" ErrorMessage="זה לא מספר מזהה תקין" ControlToValidate="PoliceIDIN" ValidationExpression="\d+" ValidationGroup="SignIn"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group" >
                <label for="ServeCityIN">עיר שירות:</label>
                <br />
                <asp:DropDownList ID="ServeCityIN" runat="server" CssClass="form-control" dir="rtl"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="ServeCityRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="ServeCityIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group" >
                <label for="TypeIN">דרגה:</label>
                <br />
                <asp:DropDownList ID="TypeIN" runat="server" CssClass="form-control" dir="rtl"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="TypeRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="TypeIN" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group" >
                <asp:Button ID="submit" runat="server" OnClick="Submit" Text="שלח" ValidationGroup="SignIn" />
            </div>
        </form>
    </div>
    
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="UserSettingsUI.aspx.cs" Inherits="PoliceVolunteerUI.UserSettingsUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="js/SignUpValidation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <center>
            <h1>
                פרטים אישיים
            </h1>
        </center>
    </div>
    <center>
        <%--<asp:GridView ID="UserInformation" runat="server" AutoGenerateColumns="False" OnRowEditing="UserInformationRowEditing"
            Style="z-index: 101; left: 18px; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
            GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true"
            OnRowCancelingEdit="UserInformationRowEditingRowCancelingEdit" 
            OnRowUpdating="UserInformationRowUpdating">
            <AlternatingRowStyle BackColor="#dbffe5" />
            <Columns>
                <asp:BoundField DataField="FieldName" HeaderText="קטגוריה" ReadOnly="True" />
                <asp:TemplateField>   
                    <ItemTemplate>   
                        <asp:Label ID="lbl_FieldValue" runat="server" Text='<%#Eval("FieldValue") %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:TextBox ID="txt_FieldValue" runat="server" Text='<%#Eval("FieldValue") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="fieldValueRequiredV" runat="server" ErrorMessage="לא הוכנס ערך"></asp:RequiredFieldValidator>
                    </EditItemTemplate>  
                </asp:TemplateField>   
                <asp:CommandField ShowEditButton="True" />
            </Columns>
            <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>--%>
        <div class="col-sm-4">
            <asp:Panel ID="UserSettings" runat="server">
                <asp:Label ID="PhoneNumberLbl" runat="server" Text="מספר טלפון:"></asp:Label>
                <br />
                <asp:TextBox ID="PhoneNumber" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PhoneNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PhoneNumber" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="PhoneNumberCustomV" runat="server" ControlToValidate="PhoneNumber" ClientValidationFunction="phoneNumberValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:Label ID="EmergencyPhoneNumberLbl" runat="server" Text="מספר חירום:"></asp:Label>
                <br />
                <asp:TextBox ID="EmergencyPhoneNumber" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmergencyPhoneNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="EmergencyPhoneNumber" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="EmergencyPhoneNumberCustomV" runat="server" ControlToValidate="EmergencyPhoneNumber" ClientValidationFunction="phoneNumberValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:Label ID="FNameLbl" runat="server" Text="שם פרטי:"></asp:Label>
                <br />
                <asp:TextBox ID="FName" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="FName" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="FNameCustomV" runat="server" ErrorMessage="זה לא שם תקין" ControlToValidate="FName" ClientValidationFunction="nameValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:Label ID="LNameLbl" runat="server" Text="שם משפחה:"></asp:Label>
                <br />
                <asp:TextBox ID="LName" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="LName" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="LNameCustomV" runat="server" ErrorMessage="זה לא שם תקין" ControlToValidate="LName" ClientValidationFunction="nameValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:label ID="HomeAddresLbl" runat="server" Text="כתובת מגורים:"></asp:label>
                <br />
                <asp:TextBox ID="HomeAdress" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <asp:RequiredFieldValidator ID="HomeAdressRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeAdress" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <br />
                <asp:label ID="HomeCityLbl" runat="server" Text="עיר מגורים:">עיר מגורים:</asp:label>
                <br />
                <asp:DropDownList ID="HomeCity" runat="server" dir="rtl" OnTextChanged="updateVolunteer"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="HomeCityRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeCity" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <br />
                <asp:label ID="EmailLbl" runat="server" Text="אימייל:"></asp:label>
                <br />
                <asp:TextBox ID="Email" runat="server" TextMode="Email" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="Email" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <br />
                <asp:label ID="PoliceIDLbl" runat="server" Text="מספר מזהה במשטרה"></asp:label>
                <br />
                <asp:TextBox ID="PoliceID" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PoliceIDRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PoliceID" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="PoliceIDRegularExpressionV" runat="server" ErrorMessage="זה לא מספר מזהה תקין" ControlToValidate="PoliceID" ValidationExpression="\d+" ValidationGroup="UserSettings"></asp:RegularExpressionValidator>
                <br />
                <asp:label ID="ServeCityLbl" runat="server" Text="עיר שירות:"></asp:label>
                <br />
                <asp:TextBox ID="ServeCity" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:Button ID="updateButton" runat="server" Text="עדכן"></asp:Button>
            </asp:Panel>
        </div>
    </center>
</asp:Content>

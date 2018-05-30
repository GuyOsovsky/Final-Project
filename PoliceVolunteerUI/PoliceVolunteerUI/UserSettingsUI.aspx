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
        <div class="col-sm-4">
            <asp:Panel ID="UserSettings" runat="server">
                <asp:Label ID="PhoneNumberLbl" runat="server" Text="מספר טלפון:"></asp:Label>
                <br />
                <asp:TextBox ID="PhoneNumber" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="PhoneNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="PhoneNumber" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="PhoneNumberCustomV" runat="server" ControlToValidate="PhoneNumber" ClientValidationFunction="phoneNumberValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:Label ID="EmergencyPhoneNumberLbl" runat="server" Text="מספר חירום:"></asp:Label>
                <br />
                <asp:TextBox ID="EmergencyPhoneNumber" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="EmergencyPhoneNumberRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="EmergencyPhoneNumber" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="EmergencyPhoneNumberCustomV" runat="server" ControlToValidate="EmergencyPhoneNumber" ClientValidationFunction="phoneNumberValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:Label ID="FNameLbl" runat="server" Text="שם פרטי:"></asp:Label>
                <br />
                <asp:TextBox ID="FName" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="FNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="FName" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="FNameCustomV" runat="server" ErrorMessage="זה לא שם תקין" ControlToValidate="FName" ClientValidationFunction="nameValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:Label ID="LNameLbl" runat="server" Text="שם משפחה:"></asp:Label>
                <br />
                <asp:TextBox ID="LName" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="LNameRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="LName" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="LNameCustomV" runat="server" ErrorMessage="זה לא שם תקין" ControlToValidate="LName" ClientValidationFunction="nameValidation" ValidationGroup="UserSettings"></asp:CustomValidator>
                <br />
                <asp:label ID="HomeAddresLbl" runat="server" Text="כתובת מגורים:"></asp:label>
                <br />
                <asp:TextBox ID="HomeAdress" runat="server" TextMode="SingleLine" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="HomeAdressRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeAdress" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <br />
                <asp:label ID="HomeCityLbl" runat="server" Text="עיר מגורים:">עיר מגורים:</asp:label>
                <br />
                <asp:DropDownList ID="HomeCity" runat="server" dir="rtl" OnTextChanged="updateVolunteer"></asp:DropDownList>
                <br />
                <asp:RequiredFieldValidator ID="HomeCityRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="HomeCity" ValidationGroup="SignIn"></asp:RequiredFieldValidator>
                <br />
                <asp:label ID="EmailLbl" runat="server" Text="אימייל:"></asp:label>
                <br />
                <asp:TextBox ID="Email" runat="server" TextMode="Email" dir="rtl" OnTextChanged="updateVolunteer"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="EmailRequiredV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="Email" ValidationGroup="UserSettings"></asp:RequiredFieldValidator>
                <br />
                <asp:label ID="PoliceIDLbl" runat="server" Text="מספר מזהה במשטרה"></asp:label>
                <br />
                <asp:TextBox ID="PoliceID" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:label ID="ServeCityLbl" runat="server" Text="עיר שירות:"></asp:label>
                <br />
                <asp:TextBox ID="ServeCity" runat="server" TextMode="SingleLine" dir="rtl" Enabled="false"></asp:TextBox>
                <br />
                <asp:Button ID="updateButton" runat="server" Text="עדכן"></asp:Button>
            </asp:Panel>
        </div>
        <br /><br /><br />
        <div class="page-header">
        <center>
            <h1>
                המכוניות שלך
            </h1>
        </center>
        </div>
        <div class="col-sm-4">
            <asp:GridView ID="carsInformation" runat="server" AutoGenerateColumns="False"
            Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
            GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
            <AlternatingRowStyle BackColor="#dbffe5" />
            <Columns>
                <asp:TemplateField HeaderText="מכונית מספר">   
                    <ItemTemplate>   
                        <asp:Label ID="lblRowNumber" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1 %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:Label ID="lblRowNumber" runat="server" Text='<%# ((GridViewRow) Container).RowIndex + 1 %>'></asp:Label> 
                    </EditItemTemplate>  
                </asp:TemplateField>   
                <asp:TemplateField HeaderText="מספר זיהוי של המכונית">   
                    <ItemTemplate>   
                        <asp:Label ID="lblCarID" runat="server" Text='<%# Eval("CarID") %>'></asp:Label>   
                    </ItemTemplate>   
                    <EditItemTemplate>   
                        <asp:TextBox ID="InputCarID" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CarIDRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newCar" ControlToValidate="InputCarID"></asp:RequiredFieldValidator>
                    </EditItemTemplate>  
                </asp:TemplateField>   
                <asp:TemplateField>   
                    <ItemTemplate>   
                        <asp:Button ID="DeleteBtn" runat="server" Text="מחק מכונית" OnClientClick="return confirm('are you sure you want delete this car to your collection?')" OnClick="deleteCar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/> 
                    </ItemTemplate> 
                    <EditItemTemplate>   
                        <asp:Button ID="UpdateBtn" runat="server" ValidationGroup="newCar" Text="הוסף מכונית חדשה" OnClientClick="return confirm('are you sure you want add this car to your collection?')" OnClick="AddNewCar" /> 
                    </EditItemTemplate>  
                </asp:TemplateField>  
            </Columns>
            <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        </asp:GridView>
        </div>
    </center>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="ActivitysAdminUI.aspx.cs" Inherits="PoliceVolunteerUI.ActivitysAdminUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>כל הפעילויות</h1>
    </center>
    
    <asp:GridView ID="ActivitysInformation" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; left: 12px; position: relative; top: 9px; width: 100%;"
        BorderColor="#000099" CellPadding="4" ShowFooter="True"
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblActivityCode" runat="server" Text='<%# Eval("ActivityCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="שם הפעילות">
                <ItemTemplate>
                    <asp:Label ID="lblActivityName" runat="server" Text='<%#Eval("ActivityName") %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputActivityName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ActivityNameRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newActivity" ControlToValidate="InputActivityName"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="תאריך">
                <ItemTemplate>
                    <asp:Label ID="lblActivityDate" runat="server" Text='<% #Eval("ActivityDate").ToString() != "" ? ((DateTime)Eval("ActivityDate")).ToShortDateString() : "" %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputActivityDate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ActivityDateRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newActivity" ControlToValidate="InputActivityDate"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="שעת התחלה">
                <ItemTemplate>
                    <asp:Label ID="lblActivityStartTime" runat="server" Text='<%# Eval("StartTime").ToString() != "" ? ((DateTime)Eval("StartTime")).ToShortTimeString() : "" %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputActivityStartTime" runat="server" TextMode="Time"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ActivityStartTimeRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newActivity" ControlToValidate="InputActivityStartTime"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="שעת סיום">
                <ItemTemplate>
                    <asp:Label ID="lblActivityFinishTime" runat="server" Text='<%#Eval("FinishTime").ToString() != "" ? ((DateTime)Eval("FinishTime")).ToShortTimeString() : "" %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputActivityFinishTime" runat="server" TextMode="Time"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ActivityFinishTimeRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newActivity" ControlToValidate="InputActivityFinishTime"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="מקום">
                <ItemTemplate>
                    <asp:Label ID="lblActivityPlace" runat="server" Text='<%#Eval("Place") %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputActivityPlace" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ActivityPlaceRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newActivity" ControlToValidate="InputActivityPlace"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="סוג">
                <ItemTemplate>
                    <asp:Label ID="lblActivityTypeName" runat="server" Text='<%#Eval("typeName") %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="InputActivityTypeName" runat="server" OnLoad="FillActivityTypesList"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="מנהל פעילות">
                <ItemTemplate>
                    <asp:Label ID="lblActivityManeger" runat="server" Text='<%#Eval("ActivityManager") %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="InputActivityManeger" runat="server" OnLoad="FillActivityManagerList"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="מספר משתתפים מינימלי">
                <ItemTemplate>
                    <asp:Label ID="lblActivityMinParticipents" runat="server" Text='<%#Eval("MinNumberOfVolunteer") %>' /> 
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="InputActivityMinParticipents" runat="server" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ActivityMinParticipentsRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ValidationGroup="newActivity" ControlToValidate="InputActivityMinParticipents"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:Button ID="UpdateButton" runat="server" ValidationGroup="newActivity" Text="עדכן" OnClick="AddNewActivity"/>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br /><br /><br /><br /><br />
    <center>
        <h1>דיווחים</h1>
        פעילות:&nbsp;&nbsp;<asp:DropDownList ID="ActivitysChooseReports" runat="server"></asp:DropDownList>
        <br />
        מתנדב:&nbsp;&nbsp; <asp:DropDownList ID="VolunteerChooseReports" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="ReportsUpdate" runat="server" Text="עדכן" OnClick="Page_LoadComplete" CausesValidation="False" />
    </center>
    <br />
    <asp:GridView ID="Reports" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; left: 18px; position: relative; top: 9px"
        BorderColor="#000099" CellPadding="4" ShowFooter="True"
        ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblActivityCode" runat="server" Text='<%# Eval("ActivityCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblUserPhoneNumber" runat="server" Text='<%# Eval("PhoneNumber")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FName" HeaderText="שם פרטי של המתנדב" ReadOnly="True" />
            <asp:BoundField DataField="LName" HeaderText="שם משפחה של המתנדב" ReadOnly="True" />
            <asp:BoundField DataField="ActivityName" HeaderText="שם הפעילות" ReadOnly="True" />
            <asp:BoundField DataField="ActivityDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="דוח" ReadOnly="True" />
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>

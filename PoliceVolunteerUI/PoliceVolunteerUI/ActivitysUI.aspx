<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="ActivitysUI.aspx.cs" Inherits="PoliceVolunteerUI.ActivitysUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>אירועים מתוכננים</h1>
    </center>
    <asp:GridView ID="ActivitysInformation" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101;  position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblActivityCode" runat="server" Text='<%# Eval("ActivityCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ActivityName" HeaderText="שם הפעילות" ReadOnly="True" />
            <asp:BoundField DataField="ActivityDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="הירשם לפעילות" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ActivitySignUp" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br /><br /><br /><br /><br />
    <center>
        <h1>אירועים שנרשמת אליהם</h1>
    </center>
    <asp:GridView ID="SignedActivitys" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101;  position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblActivityCode" runat="server" Text='<%# Eval("ActivityCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ActivityName" HeaderText="שם הפעילות" ReadOnly="True" />
            <asp:BoundField DataField="ActivityDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="בטל הגעה" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ActivitySignOut" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
    
    <br /><br /><br /><br /><br />
    <center>
        <h1>דיווחים ריקים שצריך למלא</h1>
    </center>

    <asp:GridView ID="blankReports" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101;  position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
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
            
            <asp:TemplateField HeaderText="תיאור אירוע / דיווח">
                <ItemTemplate>
                    <div dir="rtl">
                        <asp:TextBox ID="ReportText" Text="" runat="server" Width="80%" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnUpdateDescription" Text="הוסף" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="UpdateDescription" ValidationGroup="newDescription"/>                    
                    <asp:RequiredFieldValidator ID="ReportTextRequiredFieldV" runat="server" ErrorMessage="שדה זה הינו חובה" ControlToValidate="ReportText" ValidationGroup="newDescription"></asp:RequiredFieldValidator>                    
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
    <br /><br /><br /><br /><br />

</asp:Content>

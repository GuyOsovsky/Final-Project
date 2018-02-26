<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="ActivitysAdminUI.aspx.cs" Inherits="PoliceVolunteerUI.ActivitysAdminUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>כל הפעילויות</h1>
    </center>
    <asp:GridView ID="ActivitysInformation" runat="server" AutoGenerateColumns="False"
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
            <asp:BoundField DataField="ActivityName" HeaderText="שם הפעילות" ReadOnly="True" />
            <asp:BoundField DataField="ActivityDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br /><br /><br /><br /><br />
    <center>
        <h1>דיווחים</h1>
    </center>
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
            <asp:BoundField DataField="ActivityDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="דוח" ReadOnly="True" />
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>

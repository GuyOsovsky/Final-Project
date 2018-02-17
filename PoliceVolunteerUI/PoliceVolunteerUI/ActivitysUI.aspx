<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="ActivitysUI.aspx.cs" Inherits="PoliceVolunteerUI.ActivitysUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <asp:BoundField DataField="ActivityDate" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="הירשם לפעילות" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ActivitySignUp" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <asp:GridView ID="SignedActivitys" runat="server" AutoGenerateColumns="False"
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
            <asp:BoundField DataField="ActivityDate" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="בטל הגעה" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ActivitySignOut" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>

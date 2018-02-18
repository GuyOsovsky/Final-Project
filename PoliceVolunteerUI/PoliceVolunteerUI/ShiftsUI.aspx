﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="ShiftsUI.aspx.cs" Inherits="PoliceVolunteerUI.ShiftsUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>משמרות עתידיות</h1>
    </center>
    <asp:GridView ID="ShiftsInformation" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; left: 18px; position: relative; top: 9px"
        BorderColor="#000099" CellPadding="4" ShowFooter="True"
        ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblShiftCode" runat="server" Text='<%# Eval("ShiftCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ShiftType" HeaderText="סוג המשמרת" ReadOnly="True" />
            <asp:BoundField DataField="DateOfShift" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="הירשם למשמרת" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ShiftsSignUp" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br /><br /><br /><br /><br />
    <center>
        <h1>משמרות שנרשמת אליהן</h1>
    </center>
    <asp:GridView ID="SignedShifts" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; left: 18px; position: relative; top: 9px"
        BorderColor="#000099" CellPadding="4" ShowFooter="True"
        ForeColor="#333333" GridLines="None" Width="100%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblShiftCode" runat="server" Text='<%# Eval("ShiftCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ShiftType" HeaderText="סוג המשמרת" ReadOnly="True" />
            <asp:BoundField DataField="DateOfShift" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="בטל הגעה" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="ShiftsSignOut" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>

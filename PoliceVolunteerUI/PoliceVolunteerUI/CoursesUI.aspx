﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="CoursesUI.aspx.cs" Inherits="PoliceVolunteerUI.CoursesUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>הקורסים שלך</h1>
        <p>אלו הקורסים אליהם אתה צריך להגיע. במידה ואינך יכול נא בטל הגעה.</p>
    </center>
    <asp:GridView ID="CoursesInformation" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("CourseCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CourseName" HeaderText="שם הקורס" ReadOnly="True" />
            <asp:BoundField DataField="CourseDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="תיאור" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="בטל הגעה" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="CourseSignOut" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
    <br />
    <br />
    <br />
    <br />
    <br />
    <center>
        <h1>קורסים עתידיים</h1>
        <p>אלו הקורסים אליהם אתה אינו רשום. במידה והינך רוצה להירשם לחץ על כפתור ההרשמה.</p>
    </center>
    <asp:GridView ID="CoursesInformation" runat="server" AutoGenerateColumns="False"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("CourseCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CourseName" HeaderText="שם הקורס" ReadOnly="True" />
            <asp:BoundField DataField="CourseDate" DataFormatString="{0:d}" HeaderText="תאריך" ReadOnly="True" />
            <asp:BoundField DataField="StartTime" DataFormatString="{0:t}" HeaderText="שעת התחלה" ReadOnly="True" />
            <asp:BoundField DataField="FinishTime" DataFormatString="{0:t}" HeaderText="שעת סיום" ReadOnly="True" />
            <asp:BoundField DataField="Place" HeaderText="מקום" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="תיאור" ReadOnly="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="signUpButton" Text="הירשם לקורס" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClick="CourseSignUp" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>

    <br />
    <br />
    <br />
    <br />
    <br />
    <center>
        <h1>רשיונות</h1>
        <p>כאן מוצגים כל הרשיונות שלך. אנא שים לב איזה רשיון אינו תקף ודאג לחדשו.</p>
    </center>
    <asp:GridView ID="validities" runat="server" AutoGenerateColumns="False"
        onrowdatabound="Validities_RowDataBound"
        Style="z-index: 101; position: relative; top: 9px" BorderColor="#000099" CellPadding="4"
        GridLines="None" Width="100%" ForeColor="Black" BackColor="LightBlue" Font-Bold="true">
        <AlternatingRowStyle BackColor="#dbffe5" />
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblValidityCode" runat="server" Text='<%# Eval("validityCode")  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="EndDate" DataFormatString="{0:d}" HeaderText="תאריך פג תוקף" ReadOnly="True" />
            <asp:BoundField DataField="ValidityName" HeaderText="רשיון" ReadOnly="True" />
        </Columns>
        <RowStyle BackColor="#f4fbff" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>
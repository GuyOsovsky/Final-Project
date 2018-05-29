<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" CodeBehind="MediaUI.aspx.cs" Inherits="PoliceVolunteerUI.MediaUI" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div align="center">
        <u>
            <h1>מדיה</h1>
        </u>
    </div>

    <div align="right">
        <asp:Label ID="ErrorsLabel1" runat="server" align="right" dir="rtl"></asp:Label>
        <br />
    </div>

    <div align="right">

        <h2>:העלאות</h2>
        <div id="hey" runat="server" align="right" dir="rtl"></div>
        <br />
    </div>



    <form id="updateForm1" enctype="multipart/form-data">
        <div align="right" dir="rtl">

            <h3>העלאת קובץ:</h3>

            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Label id="limitationLabel" runat="server" ></asp:Label>
            <br />
            <br />

            בחר פעילות משויכת לקובץ :&nbsp;&nbsp; <asp:DropDownList ID="ChooseActivity" runat="server"></asp:DropDownList>
            <br /><br />

            <asp:Button ID="Upload1" runat="server" OnClick="BtnUpload1" Text="העלה" />

            <br />
            <br />

        </div>
    </form>

</asp:Content>

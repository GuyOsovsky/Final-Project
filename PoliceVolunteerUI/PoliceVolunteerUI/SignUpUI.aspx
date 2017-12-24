<%@ Page Title="" Language="C#" MasterPageFile="~/MasterUI.master" AutoEventWireup="true" Inherits="PoliceVolunteerUI.SignUpUI" Codebehind="SignUpUI.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        &nbsp;
        <asp:Label ID="Label1" runat="server" Text="Label">First name : </asp:Label>
        &nbsp;
        <asp:DropDownList ID="HomeCityIN" runat="server"></asp:DropDownList>
        <%--<form class="form-inline">
            <div class="form-group" >
                <label for="email">Email address:</label>
                <input type="email" class="form-control" id="email">
            </div>
            <div class="form-group">
                <label for="pwd">Password:</label>
                <input type="password" class="form-control" id="pwd">
            </div>
            <div class="checkbox">
                <label>
                    <input type="checkbox">
                    Remember me
                </label>
            </div>
            <button type="submit" class="btn btn-default">Submit</button>
        </form>--%>
    </div>
</asp:Content>


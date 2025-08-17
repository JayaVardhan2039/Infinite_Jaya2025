<%@ Page Title="Admin Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ElectricityBoardBilling.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="login-container">
        <h2>Admin Login</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        <br />
        <asp:TextBox ID="txtUsername" runat="server" Placeholder="Username" />
        <br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password" />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Assignment_1.Products" %>

<!DOCTYPE html>
<%--Question-2:Create a web application that hosts a series of products (any) as a dropdown list.
Have a image control that can display the image of the selected item in the dropdown
have a button control that gets the price of the selected product and displays it in a Label contro--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 600px;
            margin: 50px auto;
            background-color: #ffffff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        h2 {
            color: #333;
            margin-bottom: 20px;
        }

        .dropdown, .button {
            margin: 15px 0;
        }

        .button {
            padding: 10px 20px;
            background-color: #0078D7;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .button:hover {
            background-color: #005fa3;
        }

        .price-label {
            font-size: 18px;
            font-weight: bold;
            color: #d9534f;
            margin-top: 20px;
            display: block;
        }

        img {
            margin-top: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Select a Product</h2>
            <asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" CssClass="dropdown"></asp:DropDownList>
            <br />
            <asp:Image ID="imgProduct" runat="server" Width="400px" Height="400px" />
            <br />
            <asp:Button ID="btnGetPrice" runat="server" Text="Get Price" OnClick="btnGetPrice_Click"  CssClass="button"/>
            <br />
            <asp:Label ID="lblPrice" runat="server"  CssClass="price-label"></asp:Label>
        </div>
    </form>
</body>
</html>
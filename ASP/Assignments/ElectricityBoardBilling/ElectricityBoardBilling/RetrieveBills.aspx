<%@ Page Title="Retrieve Bills" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="RetrieveBills.aspx.cs" Inherits="ElectricityBoardBilling.RetrieveBills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Retrieve Last N Bills</h2>

        <asp:TextBox ID="txtNumBills" runat="server" Placeholder="Enter number of bills" />
        <br /><br />

        <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" OnClick="btnRetrieve_Click" />
        <br /><br />

        <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ConsumerNumber" HeaderText="Consumer Number" />
                <asp:BoundField DataField="ConsumerName" HeaderText="Consumer Name" />
                <asp:BoundField DataField="UnitsConsumed" HeaderText="Units Consumed" />
                <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

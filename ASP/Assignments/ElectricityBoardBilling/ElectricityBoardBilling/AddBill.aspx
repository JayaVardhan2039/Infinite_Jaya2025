<%@ Page Title="Add Bill" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="AddBill.aspx.cs" Inherits="ElectricityBoardBilling.AddBill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Add Electricity Bills</h2>

        <asp:Label ID="lblPrompt" runat="server" Text="Enter Number of Bills To Be Added:" />
        <br />
        <asp:TextBox ID="txtNumberOfBills" runat="server" />
        <br /><br />

        <asp:Button ID="btnGenerateFields" runat="server" Text="Generate Fields"
            OnClientClick="return validateNumberOfBills();" OnClick="btnGenerateFields_Click" />
        <br /><br />

        <asp:PlaceHolder ID="phBillInputs" runat="server" />
        <br /><br />

        <asp:Button ID="btnSubmitAll" runat="server" Text="Submit All Bills" OnClick="btnSubmit_Click" />
        <br /><br />

        <asp:Label ID="lblMessage" runat="server" />
    </div>

    <script type="text/javascript">
        function validateNumberOfBills() {
            var txtBox = document.getElementById('<%= txtNumberOfBills.ClientID %>');
            var value = txtBox.value.trim();
            var number = parseInt(value);

            if (isNaN(number) || number <= 0) {
                alert("Please enter a valid number above 0.");
                return false; // Prevent server-side click
            }

            return true; // Allow server-side click
        }
    </script>
</asp:Content>

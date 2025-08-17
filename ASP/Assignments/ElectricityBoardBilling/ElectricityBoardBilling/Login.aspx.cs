using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ElectricityBoardBilling.Components;

namespace ElectricityBoardBilling
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string validUsername = "e-admin";
            string validPassword = "admin@567";

            if (username == validUsername && password == validPassword)
            {
                Session["Admin"] = username;
                Response.Redirect("AddBill.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid login credentials.";
            }
        }

    }
}

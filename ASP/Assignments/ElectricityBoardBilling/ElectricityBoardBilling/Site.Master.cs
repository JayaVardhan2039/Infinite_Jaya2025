using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace ElectricityBoardBilling
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page is ElectricityBoardBilling.Login)
            {

                lblWelcomeAdmin.Visible = false;
                hlAddBill.Visible = false;
                hlRetrieveBill.Visible = false;
                hlLogout.Visible = false;
            }
            else
            {
                lblWelcomeAdmin.Text = "Welcome " + Session["Admin"].ToString();
            }
        }
    }
}

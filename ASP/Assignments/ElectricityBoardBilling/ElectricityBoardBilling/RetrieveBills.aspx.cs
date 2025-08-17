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
    public partial class RetrieveBills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                int num = int.Parse(txtNumBills.Text);
                ElectricityBoard board = new ElectricityBoard();
                var bills = board.Generate_N_BillDetails(num);

                if (bills.Count == 0)
                {
                    throw new Exception("No bill records found.");
                }

                gvBills.DataSource = bills;
                gvBills.DataBind();
            }
            catch (FormatException)
            {
                // If input is not a valid integer
                Response.Write("<script>alert('Please enter a valid number.');</script>");
            }
            catch (SqlException sqlEx)
            {
                // SQL-related errors
                Response.Write("<script>alert('Database error: " + sqlEx.Message + "');</script>");
            }
            catch (Exception ex)
            {
                // General errors
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

    }
}
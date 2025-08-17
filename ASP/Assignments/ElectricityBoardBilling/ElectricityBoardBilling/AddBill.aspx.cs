using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElectricityBoardBilling.Components;
using System.Data.SqlClient;
namespace ElectricityBoardBilling
{
    public partial class AddBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
{
    if (Session["Admin"] == null)
    {
        Response.Redirect("Login.aspx");
    }

    

    if (IsPostBack && ViewState["NumberOfBills"] != null)
    {
        int numberOfBills = (int)ViewState["NumberOfBills"];
        GenerateBillInputFields(numberOfBills);
    }
}


        protected void btnGenerateFields_Click(object sender, EventArgs e)
        {
            try
            {
                int numberOfBills;
                if (int.TryParse(txtNumberOfBills.Text, out numberOfBills))
                {
                    ViewState["NumberOfBills"] = numberOfBills;
                    GenerateBillInputFields(numberOfBills);
                }
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

        private void GenerateBillInputFields(int numberOfBills)
        {
            try
            {
            phBillInputs.Controls.Clear();
            for (int i = 0; i < numberOfBills; i++)
            {
                phBillInputs.Controls.Add(new Literal { Text = $"<h4>Bill {i + 1}</h4>" });

                phBillInputs.Controls.Add(new Literal { Text = "Consumer Number: " });
                phBillInputs.Controls.Add(new TextBox { ID = "consumerNumber_" + i });
                phBillInputs.Controls.Add(new Literal { Text = "<br/>" });

                phBillInputs.Controls.Add(new Literal { Text = "Consumer Name: " });
                phBillInputs.Controls.Add(new TextBox { ID = "consumerName_" + i });
                phBillInputs.Controls.Add(new Literal { Text = "<br/>" });

                phBillInputs.Controls.Add(new Literal { Text = "Units Consumed: " });
                phBillInputs.Controls.Add(new TextBox { ID = "unitsConsumed_" + i });
                phBillInputs.Controls.Add(new Literal { Text = "<br/>" });

                // Add label for validation message
                Label lblUnitsValidation = new Label
                {
                    ID = "unitsValidation_" + i,
                    ForeColor = System.Drawing.Color.Red,
                    Text = ""
                };
                phBillInputs.Controls.Add(lblUnitsValidation);

                phBillInputs.Controls.Add(new Literal { Text = "<br/><br/>" });
            } }
            catch (FormatException)
            {
                // If input is not a valid integer
                Response.Write("<script>alert('Please enter a valid number.');</script>");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int numberOfBills = (int)ViewState["NumberOfBills"];
                ElectricityBoard board = new ElectricityBoard();
                HashSet<string> insertedBills = ViewState["InsertedBills"] as HashSet<string> ?? new HashSet<string>();

                for (int i = 0; i < numberOfBills; i++)
                {
                    TextBox txtConsumerNumber = (TextBox)phBillInputs.FindControl("consumerNumber_" + i);
                    TextBox txtConsumerName = (TextBox)phBillInputs.FindControl("consumerName_" + i);
                    TextBox txtUnitsConsumed = (TextBox)phBillInputs.FindControl("unitsConsumed_" + i);
                    Label lblUnitsValidation = (Label)phBillInputs.FindControl("unitsValidation_" + i);

                    string consumerNumber = txtConsumerNumber?.Text;
                    string consumerName = txtConsumerName?.Text;
                    string unitsText = txtUnitsConsumed?.Text;

                    lblUnitsValidation.Text = ""; // Reset label

                    if (string.IsNullOrWhiteSpace(consumerNumber) ||
                        string.IsNullOrWhiteSpace(consumerName) ||
                        string.IsNullOrWhiteSpace(unitsText))
                    {
                        lblMessage.Text += $"Bill {i + 1}: Missing input fields.<br/>";
                        continue;
                    }

                    // Consumer Number Validation
                    try
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(consumerNumber, @"^EB\d{5}$"))
                        {
                            throw new FormatException("Invalid Consumer Number");
                        }
                    }
                    catch (FormatException ex)
                    {
                        lblMessage.Text += $"Bill {i + 1}: {ex.Message}<br/>";
                        continue;
                    }

                    // Units Consumed Validation
                    int unitsConsumed;
                    if (!int.TryParse(unitsText, out unitsConsumed))
                    {
                        lblUnitsValidation.Text = "Invalid Units Consumed";
                        lblUnitsValidation.ForeColor = System.Drawing.Color.Red;
                        continue;
                    }

                    if (unitsConsumed < 0)
                    {
                        lblUnitsValidation.Text = "Given units is invalid";
                        lblUnitsValidation.ForeColor = System.Drawing.Color.Red;
                        continue;
                    }
                    else
                    {
                        lblUnitsValidation.Text = "Valid";
                        lblUnitsValidation.ForeColor = System.Drawing.Color.Green;
                    }

                    if (insertedBills.Contains(consumerNumber))
                    {
                        continue;
                    }

                    ElectricityBill bill = new ElectricityBill
                    {
                        ConsumerNumber = consumerNumber,
                        ConsumerName = consumerName,
                        UnitsConsumed = unitsConsumed
                    };

                    board.CalculateBill(bill);
                    board.AddBill(bill);

                    lblMessage.Text += $"Bill Added: {bill.ConsumerNumber} {bill.ConsumerName} {bill.UnitsConsumed} Bill Amount: {bill.BillAmount}<br/>";
                    insertedBills.Add(consumerNumber);
                }

                ViewState["InsertedBills"] = insertedBills;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

    }
}

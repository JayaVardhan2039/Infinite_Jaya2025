using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Assignment_1
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ValidateCity(object source, ServerValidateEventArgs args)
        {
            if (args.Value == "")
            {
                args.IsValid = false;
            }
            else if (args.Value.Length < 5)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void ValidateAddress(object source, ServerValidateEventArgs args)
        {
            if (args.Value == "")
            {
                args.IsValid = false;
            }
            else if (args.Value.Length < 5)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void ValidateZipCode(object source, ServerValidateEventArgs args)
        {
            string zipCode = args.Value;
            if (zipCode == "")
            {
                args.IsValid = false;
            }
            else if (Regex.IsMatch(zipCode, @"^\d{5}$"))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ValidatePhoneNumber(object source, ServerValidateEventArgs args)
        {
            string phoneNumber = args.Value;
            if (phoneNumber == "")
            {
                args.IsValid = false;
            }
            else if (Regex.IsMatch(phoneNumber, @"^\d{2}-\d{7}$|^\d{3}-\d{7}$"))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ValidateEmail(object source, ServerValidateEventArgs args)
        {
            string email = args.Value;
            if (email == "")
            {
                args.IsValid = false;
            }
            else if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void ValidateNameAndFamily(object source, ServerValidateEventArgs args)
        {
            string name = Txtname.Text.Trim();
            string familyName = args.Value.Trim();
            if (name != "" && familyName != "" && name.ToLower() != familyName.ToLower())
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void OnCheckButtonClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                txtmsg.Text = "Validation Success";
                txtmsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                txtmsg.Text = "Validation failed. Please check your input.";
                txtmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}

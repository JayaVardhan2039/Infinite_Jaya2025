using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
/*Quetion-2:.Create a web application that hosts a series of products (any) as adropdown list.
Have a image control that can display the image of the selected item in the dropdown
have a button control that gets the price of the selected product and displays it in a Label control*/
namespace Assignment_1
{
    public partial class Products : System.Web.UI.Page
    {
        Dictionary<string, (string ImageUrl, decimal Price)> products = new Dictionary<string, (string, decimal)>
        {
            {"Iphone",("~/images/Iphone.jpg", 98000m) },
            {"Laptops",("~/images/Laptop.jpg", 75000m) },
            {"Laptop_Bags",("~/images/Laptop_Bag.jpg", 10000m) },
            {"Shoes",("~/images/Shoe.jpg", 8000m) },
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (var product in products.Keys)
                {
                    ddlProducts.Items.Add(product);
                }
            }
        }

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProduct = ddlProducts.SelectedItem.Text;
            if (products.ContainsKey(selectedProduct))
            {
                imgProduct.ImageUrl = products[selectedProduct].ImageUrl;
            }
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            string selectedProduct = ddlProducts.SelectedItem.Text;
            if (products.ContainsKey(selectedProduct))
            {
                lblPrice.Text = "Price:" + products[selectedProduct].Price.ToString();
            }
        }
    }
}
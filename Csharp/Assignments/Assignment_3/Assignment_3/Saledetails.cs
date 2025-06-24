using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 3. Create a class called Saledetails which has data members like Salesno,  Productno,  Price, dateofsale, Qty, TotalAmount
///-Create a method called Sales() that takes qty, Price details of the object and updates the TotalAmount as  Qty *Price
///- Pass the other information like SalesNo, Productno, Price, Qty and Dateof sale through constructor
///- call the show data method to display the values without an object.
/// </summary>
namespace Assignment_3
{
    
    class Saledetails
    {
        public float Salesno, Productno, Price, Qty, TotalAmount;
        public DateTime Dateofsale;


        public Saledetails(int qty,int price,int salesno,int productno,DateTime dateofsale)
        {
            Salesno = salesno;
            Productno = productno;
            Price = price;
            Dateofsale = dateofsale;
            Qty = qty;
            Sales();
        }

        public void Sales()
        {
            TotalAmount = Qty * Price;
        }
        public void Show_Data_Function()
        {
            Console.WriteLine($"Sales No: {Salesno}");
            Console.WriteLine($"Product No: {Productno}");
            Console.WriteLine($"Price: {Price}");
            Console.WriteLine($"Date of Sale: {Dateofsale}");
            Console.WriteLine($"Quantity: {Qty}");
            Console.WriteLine($"Total Amount: {TotalAmount}");
        }
    }

    class Tester_Sales
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Question 3");
            Console.WriteLine();
            Saledetails saledetails = new Saledetails(3, 50, 1, 101, DateTime.Now);

            saledetails.Show_Data_Function();
            Console.Read();
        }
    }
    

}

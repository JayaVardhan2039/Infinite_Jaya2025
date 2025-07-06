using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelConcessionLibrary;
/*
 * 4.    Create a class library with a function CalculateConcession()  that takes age as an input and calculates concession for travel as below:
If age <= 5 then “Little Champs - Free Ticket” should be displayed
If age > 60 then calculate 30% concession on the totalfare(Which is a constant Eg:500/-) and Display “ Senior Citizen” + Calculated Fare
Else “Print Ticket Booked” + Fare. 
Create a Console application with a Class called Program which has TotalFare as Constant, Name, Age.  Accept Name, Age from the user and call the CalculateConcession() function to test the Classlibrary functionality
 */
namespace Assignment_7
{


    class Program
    {
        const int TotalFare = 500;

        static void Main()
        {
            bool continueBooking = true;

            while (continueBooking)
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine();

                Console.Write("Enter your age: ");
                int Age = -1;
                bool ValidInput = false;

                while (!ValidInput || Age < 0)
                {
                    Console.WriteLine("Please enter a valid non-negative age:");
                    try
                    {
                        Age = Convert.ToInt32(Console.ReadLine());
                        ValidInput = true;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        ValidInput = false;
                    }
                }

                string result = ConcessionCalculator.CalculateConcession(Age, TotalFare);
                Console.WriteLine($"\nHello {name}, {result}");

                yrr: Console.WriteLine("\nDo you want to book again? (Y/N): ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "N")
                {
                    continueBooking = false;
                    Console.WriteLine("\nThank you");
                }
                else if (choice != "Y" && choice!="N") 
                {
                    
                    Console.WriteLine("\nPlease enter correct choice");
                    goto yrr;
                }


            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 2. Create a class called Scholarship which has a function Public void Merit() that takes marks and fees as an input. 
If the given mark is >= 70 and <=80, then calculate scholarship amount as 20% of the fees
If the given mark is > 80 and <=90, then calculate scholarship amount as 30% of the fees
If the given mark is >90, then calculate scholarship amount as 50% of the fees.
In all the cases return the Scholarship amount, else throw an user exception
 */
namespace Assignment_5
{

    public class ScholarshipNotEligibleException : ApplicationException
    {
        public ScholarshipNotEligibleException(string message) : base(message) { }
    }

   
    public class Scholarship_Calculation
    {
        public double ELigiblity(int marks, double fees)
        {
            if (marks < 0 || marks > 100)
                throw new InvalidInputException("Marks must be between 0 and 100.");

            if (fees <= 0)
                throw new InvalidInputException("Fees must be a positive number.");

            double scholarshipAmount = 0;

            if (marks >= 70 && marks <= 80)
                scholarshipAmount = 0.2 * fees;
            else if (marks > 80 && marks <= 90)
                scholarshipAmount = 0.3 * fees;
            else if (marks > 90)
                scholarshipAmount = 0.5 * fees;
            else
                throw new ScholarshipNotEligibleException("Student does not qualify for scholarship based on marks.");

            return scholarshipAmount;
        }

        public static void Main(string[] args)
        {
            Scholarship_Calculation s = new Scholarship_Calculation();

            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter Marks (or 'S' to stop):");
                    string marksInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(marksInput))
                        throw new ArgumentNullException("Marks input cannot be empty.");

                    if (marksInput.Trim().ToUpper() == "S")
                        break;

                    int marks = Convert.ToInt32(marksInput);

                    Console.WriteLine("Enter Fees:");
                    string feesInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(feesInput))
                        throw new ArgumentNullException("Fees input cannot be empty.");

                    double fees = Convert.ToDouble(feesInput);

                    double result = s.ELigiblity(marks, fees);
                    Console.WriteLine($"Scholarship Amount: {result}");
                }
                catch (ScholarshipNotEligibleException ex)
                {
                    Console.WriteLine($"Eligibility Error: {ex.Message}");
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"Invalid Input: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input was not in correct format. Please enter numeric values for marks and fees.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Entered value is too large.");
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine($"Missing Input: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                }
            }

            Console.WriteLine("\nThank you for checking scholarship eligibility.");
            Console.Read();
        }

    }
}

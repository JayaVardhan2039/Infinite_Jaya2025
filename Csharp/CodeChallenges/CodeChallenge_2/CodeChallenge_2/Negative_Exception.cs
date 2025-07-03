using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
 * 3. Write a C# program to implement a method that takes an integer as input 
 * and throws an exception if the number is negative.
 * Handle the exception in the calling code.
 */

namespace CodeChallenge_2
{
    //My Custom exception
    public class NegativeNumberException : Exception
    {
        public NegativeNumberException(string message) : base(message) { }
    }

    class Negative_Exception
    {
        public static void CheckNumber(int number)
        {
            if (number < 0)
            {
                throw new NegativeNumberException("Negative Number Exception,Number cannot be negative.");
            }
            else
            {
                Console.WriteLine("Valid number entered: " + number);
            }
        }

        static void Main()
        {
            Console.Write("Enter an integer: ");
            try
            {
                string inputStr = Console.ReadLine();
                if (!int.TryParse(inputStr, out int input))
                {
                    throw new ArgumentException("Input is not a valid integer.");
                }

                CheckNumber(input);
            }
            catch (NegativeNumberException except)
            {
                Console.WriteLine("Caught Custom Exception: " + except.Message);
            }
            catch (ArgumentException except)
            {
                Console.WriteLine("Caught Argument Exception: " + except.Message);
            }

            Console.Read();
        }
    }
}





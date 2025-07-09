using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 4. Write a console program that uses delegate object as an argument
 * to call Calculator Functionalities like 
 * 1. Addition, 2. Subtraction and 3. Multiplication
 * by taking 2 integers and returning the output to the user.
 * You should display all the return values accordingly.
 */
namespace CodeChallenge_3
{
    class Calculator_Delegate
    {
        public delegate int CalculatorOperation(int a, int b);

        public static void PerformOperation(int num1, int num2, CalculatorOperation Operation, string OperationName)
        {
            try
            {
                int result = Operation(num1, num2);
                Console.WriteLine($"{OperationName} of {num1} and {num2} is: {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division or modulus by zero is not accepted as it doesnt give a finite value.");
            }
        }

        static void Main()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter first number: ");
                    int num1 = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter second number: ");
                    int num2 = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Choose operation:");
                    Console.WriteLine("1. Addition");
                    Console.WriteLine("2. Subtraction");
                    Console.WriteLine("3. Multiplication");
                    Console.WriteLine("4. Division");
                    Console.WriteLine("5. Modulus");
                    Console.Write("Enter your choice (1-5): ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    CalculatorOperation operation = null;
                    string OperationName = "";

                    switch (choice)
                    {
                        case 1:
                            operation = (a, b) => a + b;
                            OperationName = "Addition";
                            break;
                        case 2:
                            operation = (a, b) => a - b;
                            OperationName = "Subtraction";
                            break;
                        case 3:
                            operation = (a, b) => a * b;
                            OperationName = "Multiplication";
                            break;
                        case 4:
                            operation = (a, b) => a / b;
                            OperationName = "Division";
                            break;
                        case 5:
                            operation = (a, b) => a % b;
                            OperationName = "Modulus";
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            continue;
                    }

                    PerformOperation(num1, num2, operation, OperationName);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input from you. Please enter a valid integer.");
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Number is too large. Please enter a smaller integer.");
                    continue;
                }

                Console.Write("Do you want to calculate for another time? select one in (y/n): ");
                string select = Console.ReadLine().ToLower();
                if (select != "y")
                    break;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}


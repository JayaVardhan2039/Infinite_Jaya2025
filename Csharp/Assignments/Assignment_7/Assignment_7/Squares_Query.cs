using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 1.) 
Write a query that returns list of numbers and their squares only if square is greater than 20 

Example input [7, 2, 30]  
Expected output
→ 7 - 49, 30 - 900

 */
namespace Assignment_7
{
    class Squares_Query
    {
        static void Main(string[] args)
        {


            Console.Write("Enter how many numbers you want to input: ");
            int limit;
            while (!int.TryParse(Console.ReadLine(), out limit) || limit <= 0)
            {
                Console.Write("Please enter a valid positive number: ");
            }

            List<int> numbers = new List<int>();

            for (int i = 0; i < limit; i++)
            {
                Console.Write($"Enter number {i + 1}: ");
                int num;
                while (!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.Write("Invalid input. Please enter an integer: ");
                }
                numbers.Add(num);
            }

            //Query Expression
            var result = numbers
            .Select(n => new { Number = n, Square = n * n })
            .Where(x => x.Square > 20);


            Console.WriteLine("\nNumbers and their squares (only if square > 20):");
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Number} - {item.Square}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Swap_two_numbers
    {
        public static void function()
        {

            Console.Write("Enter the value of number 1: ");
            int a =Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the value of number 2: ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Before Swapping number 1 is {a}  and number 2 is {b}");
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
            Console.WriteLine($"After Swapping number 1 is {a} and number 2 is {b}");
        }
        

    }
}

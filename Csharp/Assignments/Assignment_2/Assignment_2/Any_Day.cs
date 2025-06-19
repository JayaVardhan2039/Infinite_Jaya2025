using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Any_Day
    {
        public static void function()
        {
            Console.Write("enter a number : ");
            int a = Convert.ToInt32(Console.ReadLine());
            if (a < 0 || a > 7)
            {
                Console.WriteLine("Wrong input,only 1 to 7 please");
                return;
            }
            string[] arr = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            Console.WriteLine(arr[a-1]);
        }
    }
}

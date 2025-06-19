using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Average_and_Extremes
    {
        public static void function()
        {
            int avg=0;
            int min=int.MaxValue, max=int.MinValue;
            Console.Write("Enter the size of the array please : ");
            int a = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[a];
            for (int i = 0; i < a; i++)
            {
                Console.Write($"Enter the element {i + 1} of arr1 : ");
                arr[i] = Convert.ToInt32(Console.ReadLine());
                avg += arr[i];
                if (arr[i] > max) max = arr[i];
                if (arr[i] < min) min = arr[i];

            }
            Console.WriteLine("Elements of 1st array:");
            for (int i = 0; i < a; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Average is {avg/a}, Min value is {min}, Max value is {max}");
        }
    }
}

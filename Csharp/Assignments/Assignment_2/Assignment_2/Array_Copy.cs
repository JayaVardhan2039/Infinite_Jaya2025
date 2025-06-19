using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Array_Copy
    {
        public static void function()
        {
            Console.Write("Enter the size of the array please : ");
            int a=Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[a];
            for(int i=0;i<a;i++)
            {
                Console.Write($"Enter the element {i+1} of arr1 : ");
                arr[i]= Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Elements of 1st array:");
            for(int i = 0; i < a; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            int[] arr2 = new int[a];
            Console.WriteLine("Elements of 2nd array before copying:");
            for (int i = 0; i < a; i++)
            {
                Console.Write(arr2[i] + " ");
            }
            Console.WriteLine();
            //array copying
            for (int i = 0; i < a; i++)
            {
                arr2[i] = arr[i];
            }
            Console.WriteLine("Elements of 2nd array after copying:");
            for (int i = 0; i < a; i++)
            {
                Console.Write(arr2[i] + " ");
            }
        }
    }
}

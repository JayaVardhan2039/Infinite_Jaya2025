using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Ten_Marks
    {
        public static void function()
        {
            int[] marks = new int[10];
            int total=0,min=int.MaxValue,max=int.MinValue;
            for(int i=0;i<10;i++)
            {
                Console.Write($"Entet the mark {i + 1} : ");
                marks[i] = Convert.ToInt32(Console.ReadLine());
                total += marks[i];
                if (marks[i] > max) max = marks[i];
                if (marks[i] < min) min = marks[i];
            }
            for(int i=0;i<10;i++)
            {
                //i used insertion sort
                int k = marks[i];
                int j = i - 1;
                while(j>=0 && marks[j]>k)
                {
                    marks[j + 1] = marks[j];
                    j--;
                }
                marks[j + 1] = k;
            }
            Console.WriteLine("Total Marks {0}\n Average is {1} \n Min value is {2} \n Max value is {3}", total,total/10,min,max);
            Console.Write("Ascending Order:");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(marks[i] + " ");
           
            }
            for (int i = 0; i < 10; i++)
            {
                int k = marks[i];
                int j = i - 1;
                while (j >= 0 && marks[j] < k)
                {
                    marks[j + 1] = marks[j];
                    j--;
                }
                marks[j + 1] = k;
            }
            Console.WriteLine();
            Console.Write("Descending Order:");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(marks[i] + " ");

            }

        }
    }
}

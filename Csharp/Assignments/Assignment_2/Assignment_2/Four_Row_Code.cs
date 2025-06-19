using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Four_Row_Code
    {
        public static void function()
        {
            Console.Write("Enter a digit : ");
            int a = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(a);
                    if (i % 2 == 0)
                        Console.Write(" ");
                }
                Console.WriteLine();
            }



        }
    }
}

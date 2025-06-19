using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    class Strings_All
    {
        public static void function()
        {
            Console.Write("Enter a word to find its lenght : ");
            string str = Console.ReadLine();
            Console.WriteLine("The length of the string you entered is {0}", str.Length);
            Console.Write("Enter a word to get its reversed : ");
            string str_rev = Console.ReadLine();
            string ste="";
            for (int i = str_rev.Length - 1; i >= 0; i--) ste += str_rev[i];
            ste += '\0';
            Console.WriteLine("The reverse of the string you entered is {0}", ste);
            Console.Write("Enter string 1: ");
            string str1 = Console.ReadLine();
            Console.Write("Enter string 2: ");
            string str2 = Console.ReadLine();
            string tr = str1.Equals(str2) == true ? "" : "not";
            Console.WriteLine($"The Boolean value of the strings equality is {str1.Equals(str2)} i.e {tr} same");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge_1
{
    /// <summary>
    /// 2. Write a C# Sharp program to exchange the first and last characters in a given string and return the new string.

    //    Sample Input:
    //"abcd"
    //"a"
    //"xy"
    //Expected Output:

    //dbca
    //a
    //yx
    /// </summary>
    class Question_2
    {
        public string Exchange_Char(string s)
        {
            int l = 0;
            foreach (char c in s)
            {
                l++;
            }

            if (l <= 1)
            {
                return s;
            }

            char[] r = new char[l];

            
            r[0] = s[l - 1];
            for (int i = 1; i < l - 1; i++)
            {
                r[i] = s[i];
            }
            r[l - 1] = s[0];

            string final = "";
            for (int i = 0; i < l; i++)
            {
                final += r[i];
            }

            return final;
        }

        public static void Main()
        {
            Question_2 q2 = new Question_2();
            //Dynamic Entry
            string s;
            Console.WriteLine("-------Dynamic Entry--------");
            Console.WriteLine("Enter the string :");
            s = Console.ReadLine();
            Console.WriteLine("First and Last chars Reversed string :" + q2.Exchange_Char(s));
            Console.WriteLine();
            //Compile Time Entry
            Console.WriteLine("-----Compiletime Entries------");
            Console.WriteLine(q2.Exchange_Char("abcd")); // Output: dbca
            Console.WriteLine(q2.Exchange_Char("a"));    // Output: a
            Console.WriteLine(q2.Exchange_Char("xy"));   // Output: yx
            Console.Read();
        }
    }

}

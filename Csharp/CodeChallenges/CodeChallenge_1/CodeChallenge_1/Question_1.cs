using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge_1
{
    /// <summary>
    /// 1. Write a C# Sharp program to remove the character at a given position in the string. The given position will be in the range 0..(string length -1) inclusive.

        //Sample Input:
        //"Python", 1
        //"Python", 0
        //"Python", 4
        //Expected Output:
        //Pthon
        //ython
        //Pythn
    /// </summary>

class Question_1
    {
        public static string Char_Removal(string s, int p)
        {
            if (p < 0 || p >= s.Length)
            {
                throw new ArgumentOutOfRangeException("Position is out of range.");
            }
            return s.Remove(p, 1);
        }
        static void Main(string[] args)
        {
            //Dynamic Entry
            string s;
            Console.WriteLine("-------Dynamic Entry--------");
            Console.WriteLine("Enter the string :");
            s = Console.ReadLine();
            Console.WriteLine("Enter the position :");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Charcter removed String :" + Char_Removal(s, a));
            Console.WriteLine();
            //Compile Time Entry
            Console.WriteLine("-----Compiletime Entries------");
            Console.WriteLine(Char_Removal("Python", 1)); // Pthon
            Console.WriteLine(Char_Removal("Python", 0)); // ython
            Console.WriteLine(Char_Removal("Python", 4)); // Pythn
            Console.Read();

        }
    }
}

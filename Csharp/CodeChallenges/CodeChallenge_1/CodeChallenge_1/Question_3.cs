using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge_1
{
 /// <summary>
 /// 
 ///  
    //3. Write a C# Sharp program to check the largest number among three given integers.
 
    //Sample Input:
    //1,2,3
    //1,3,2
    //1,1,1
    //1,2,2
    //Expected Output:
    //3
    //3
    //1
    //2
 /// </summary>
    class Question_3
    {
        public int FindLargest(int x, int y, int z)
        {
            int lar = x;

            if (y > lar)
            {
                lar= y;
            }

            if (z > lar)
            {
                lar = z;
            }

            return lar;
        }

        public static void Main()
        {
            Question_3 q3 = new Question_3();
            //Dynamic Entry
            int a, b, c;
            Console.WriteLine("-------Dynamic Entry--------");
            Console.WriteLine("Enter First number :");
            a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter second number :");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter third number :");
            c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("The largest is :" + q3.FindLargest(a, b, c));
            Console.WriteLine(); ;
            //Normal Entry
            Console.WriteLine("-------CompileTimeEntris--------");
            Console.WriteLine(q3.FindLargest(1, 2, 3)); // 3
            Console.WriteLine(q3.FindLargest(1, 3, 2)); // 3
            Console.WriteLine(q3.FindLargest(1, 1, 1)); // 1
            Console.WriteLine(q3.FindLargest(1, 2, 2)); // 2
            
            Console.Read();
        }
    }

}

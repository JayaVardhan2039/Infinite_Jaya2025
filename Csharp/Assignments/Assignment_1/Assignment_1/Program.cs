using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    /// <summary>
    /// All the Questions are divided into 5 classes and i wrote the 
    /// code accordingly evoking the objects in Program class Main function
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Question_1 obj1 = new Question_1();
            obj1.Equal_Integers();
            Question_2 obj2 = new Question_2();
            obj2.Positive_Number();
            Question_3 obj3 = new Question_3();
            obj3.Operations();
            Question_4 obj4 = new Question_4();
            obj4.Table();
            Question_5 obj5 = new Question_5();
            Console.WriteLine($"Printing the returned Value {obj5.Returning_Function()}");
            Console.Read();

        }
    }

    class Question_1
    {
        public void Equal_Integers()
        {
            Console.Write("Input 1st number : ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input 2nd number : ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(a == b ? $"{a} and {b} are equal": $"{a} and {b} are not equal");
        }
    }

    class Question_2
    {
        public void Positive_Number()
        {
            Console.Write("Input the number to check:");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(a < 0 ? $"{a} is a negative number" : $"{a} is a positive number");
        }
    }

    class Question_3
    {
        public void Operations()
        {
            Console.Write("Input first number : ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input operation : ");
            char o = Convert.ToChar(Console.ReadLine());
            Console.Write("Input second number : ");
            int b = Convert.ToInt32(Console.ReadLine());
            switch(o)
            {
                case '-':
                    Console.WriteLine($"{a} {o} {b} = {a - b}");
                    break;
                case '+':
                    Console.WriteLine($"{a} {o} {b} = {a + b}");
                    break;
                case '/':
                    Console.WriteLine($"{a} {o} {b} = {a / b}");
                    break;
                case '*':
                    Console.WriteLine($"{a} {o} {b} = {a * b}");
                    break;
                default:
                    Console.WriteLine("Invalid operator");
                    break;
            }
        }
    }

    class Question_4
    {
        public void Table()
        {
            Console.Write("Enter the number : ");
            int a = Convert.ToInt32(Console.ReadLine());
            for(int i=0;i<11;i++)
            {
                Console.WriteLine($"{a} * {i} = {a * i}");
            }

        }
    }

    class Question_5
    {
        //return type is int here okay
        public int Returning_Function()
        {
            Console.Write("Input 1st number : ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input 2nd number : ");
            int b = Convert.ToInt32(Console.ReadLine());
            int x = a == b ? 3 * (a + b) : a + b;
            return x;
        }
    }
}

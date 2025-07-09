using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 2. Write a class Box that has Length and breadth as its members. 
 * Write a function that adds 2 box objects and stores in the 3rd. 
 * Display the 3rd object details. 
 * Create a Test class to execute the above.
 */
namespace CodeChallenge_3
{
    class Box
    {
        public double Length { get; set; }
        public double Breadth { get; set; }

        public Box(double length, double breadth)
        {
            this.Length = length;
            this.Breadth = breadth;
        }

        public static Box AddBoxes(Box b1, Box b2)
        {
            double new_Length = b1.Length + b2.Length;
            double new_Breadth = b1.Breadth + b2.Breadth;
            return new Box(new_Length, new_Breadth);
        }

        public void Display()
        {
            Console.WriteLine($"New Box Dimensions are:\n Length: {Length}, Breadth: {Breadth}");
        }
    }

    class Test
    {
        static void Main()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("....Input can be a integer value or float value or double value....");
                    Console.Write("Enter length of Box 1: ");
                    double length1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter breadth of Box 1: ");
                    double breadth1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter length of Box 2: ");
                    double length2 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter breadth of Box 2: ");
                    double breadth2 = Convert.ToDouble(Console.ReadLine());

                    Box box1 = new Box(length1, breadth1);
                    Box box2 = new Box(length2, breadth2);

                    Box box3 = Box.AddBoxes(box1, box2);

                    Console.WriteLine("Resulting Box after Addition:");
                    box3.Display();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number (int, float, or double).");
                    continue;
                }
                

                Console.Write("Do you want to add another set of boxes? (y/n): ");
                string sewect = Console.ReadLine().ToLower();
                if (sewect != "y")
                    break;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}


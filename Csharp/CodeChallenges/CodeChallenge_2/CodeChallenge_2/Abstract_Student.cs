using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * 1. Create an Abstract class Student with  Name, StudentId, Grade as members and also an abstract method Boolean Ispassed(grade) which takes grade as an input and checks whether student passed the course or not.  
 
Create 2 Sub classes Undergraduate and Graduate that inherits all members of the student and overrides Ispassed(grade) method
 
For the UnderGrad class, if the grade is above 70.0, then isPassed returns true, otherwise it returns false. For the Grad class, if the grade is above 80.0, then isPassed returns true, otherwise returns false.
 
Test the above by creating appropriate objects
 */

namespace CodeChallenge_2
{

    abstract class Student
    {
        //properties
        public string Name { get; set; }
        public int StudentId { get; set; }
        public double Grade { get; set; }

        public Student(string name, int studentId, double grade)
        {
            this.Name = name;
            this.StudentId = studentId;
            this.Grade = grade;
        }

        public abstract bool IsPassed(double grade);
    }

    //inhertiance 1
    class Undergraduate : Student
    {
        public Undergraduate(string name, int studentId, double grade) : base(name, studentId, grade) { }

        public override bool IsPassed(double grade)
        {
            return grade > 70.0;
        }
    }

    //inheritance 2
    class Graduate : Student
    {
        public Graduate(string name, int studentId, double grade) : base(name, studentId, grade) { }

        public override bool IsPassed(double grade)
        {
            return grade > 80.0;
        }
    }

    class Abstract_Student
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Choose student type: 1. Undergraduate  2. Graduate  3. Exit");
                string ch = Console.ReadLine();

                if (ch == "3")
                    break;

                Console.Write("Enter Name: ");
                string Name = Console.ReadLine();

                Console.Write("Enter Student ID: ");
                int Studentid = int.Parse(Console.ReadLine());

                Console.Write("Enter Grade: ");
                double Grade = double.Parse(Console.ReadLine());

                Student student;

                if (ch == "1")
                    student = new Undergraduate(Name, Studentid, Grade);
                else if (ch == "2")
                    student = new Graduate(Name, Studentid, Grade);
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                    continue;
                }

                Console.WriteLine($"{student.Name} Passed: {student.IsPassed(student.Grade)}\n");
            }

            Console.WriteLine("Program ended.");
            Console.Read();
        }
    }

}

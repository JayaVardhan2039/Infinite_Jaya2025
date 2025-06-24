using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    class Student
    {
        /// <summary>
        /// 2. Create a class called student which has data members like rollno, name, class, Semester, branch, int [] marks=new int marks [5](marks of 5 subjects )

        //-Pass the details of student like rollno, name, class, SEM, branch in constructor

        //-For marks write a method called GetMarks() and give marks for all 5 subjects

        //-Write a method called displayresult, which should calculate the average marks

        //-If marks of any one subject is less than 35 print result as failed
        //-If marks of all subject is >35,but average is < 50 then also print result as failed
        //-If avg > 50 then print result as passed.

        //-Write a DisplayData() method to display all object members values.
        /// </summary>
        /// <param name="args"></param>
        

        
            public int Rollno, Class, Semester;
            public string Name, Branch;
            public int[] marks = new int[5];

            public Student(int rollno,string name,int classs,int semester,string branch)
            {
                Rollno = rollno;
                Name = name;
                Class = classs;
                Semester = semester;
                Branch = branch;
                GetMarks();
            }

            public void GetMarks()
            {
                Console.WriteLine("Enter the marks for the 5 subjects");
                for(int i=0;i<marks.Length;i++)
                {
                    Console.Write($"Enter the subject {i+1} marks : ");
                    marks[i]=Convert.ToInt32(Console.ReadLine());
                }
                DisplayData();
            }

            public void DisplayResult()
            {
                int avg = 0;
                for(int i=0;i<marks.Length;i++)
                {
                    avg += marks[i];
                    
                }
                for(int i = 0; i < marks.Length; i++)
                {
                    if(marks[i]<35)
                    {
                        Console.WriteLine("Result Status Failed");
                        break;
                    }
                }
                
                Console.WriteLine("Average : " + avg/5);

                if(avg/5 < 50)
                {
                    Console.WriteLine("Result Status Failed");
                }
                else
                {
                    Console.WriteLine("Result Status Passed");
                }
            }

            public void DisplayData()
            {
                Console.WriteLine("Name : " + Name);
                Console.WriteLine("RollNo : " + Rollno);
                Console.WriteLine("Class : " + Class);
                Console.WriteLine("Branch : " + Branch);
                Console.WriteLine("Semester : " + Semester);
                Console.WriteLine("The marks for all the 5 subjects");
                for (int i = 0; i < marks.Length; i++)
                {
                    Console.WriteLine($"subject {i + 1} marks {marks[i]}: ");
                }
                DisplayResult();
            } 
        
        
    }
    class Tester_Student
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Question 2");
            Console.WriteLine();
            Student student = new Student(6676, "Jaya", 9, 1, "Science");
            Console.Read();
        }
    }
}

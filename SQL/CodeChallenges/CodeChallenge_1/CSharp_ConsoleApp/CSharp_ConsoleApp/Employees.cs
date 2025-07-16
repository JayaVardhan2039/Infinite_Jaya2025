using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ConsoleApp
{
    class Employees
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }
    }

    class CSharp_Question_1
    {
        static void Main()
        {
            Console.Write("Enter the number of employees: ");
            int n = Convert.ToInt32(Console.ReadLine());

            List<Employees> employees = new List<Employees>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nEnter Details for Employee {i + 1}");

                Console.Write("Enter Employee Id: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter First Name: ");
                string fname = Console.ReadLine().ToUpper();

                Console.Write("Enter Last Name: ");
                string lname = Console.ReadLine().ToUpper();

                Console.Write("Enter Title: ");
                string title = Console.ReadLine().ToUpper();

                Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                DateTime dob = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Date of Joining (yyyy-mm-dd): ");
                DateTime doj = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter City: ");
                string city = Console.ReadLine().ToUpper();

                employees.Add(new Employees { EmployeeID = id, FirstName = fname, LastName = lname, Title = title, DOB = dob, DOJ = doj, City = city });
            }

            int choice;
            do
            {
                Console.WriteLine("\nSelect Menu choice:");
                Console.WriteLine("1. Display all employee details");
                Console.WriteLine("2. Display employees not in Mumbai");
                Console.WriteLine("3. Display employees with title 'AsstManager'");
                Console.WriteLine("4. Display employees whose last name starts with 'S'");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayEmployees(employees);
                        break;
                    case 2:
                        DisplayEmployees(employees.Where(e => e.City != "Mumbai"));
                        break;
                    case 3:
                        DisplayEmployees(employees.Where(e => e.Title == "AsstManager"));
                        break;
                    case 4:
                        DisplayEmployees(employees.Where(e => e.LastName.StartsWith("S")));
                        break;
                    case 5:
                        Console.WriteLine("Evit...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try to select again.");
                        break;
                }

            } while (choice != 5);
        }

        static void DisplayEmployees(IEnumerable<Employees> employees)
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.EmployeeID}, {emp.FirstName}, {emp.LastName}, {emp.Title}, {emp.DOB.ToShortDateString()}, {emp.DOJ.ToShortDateString()}, {emp.City}");
            }
        }
    }
}



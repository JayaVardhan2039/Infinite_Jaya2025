using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
3.	Create a list of employees with following property EmpId, EmpName, EmpCity, EmpSalary. Populate some data
Write a program for following requirement
a.	To display all employees data
b.	To display all employees data whose salary is greater than 45000
c.	To display all employees data who belong to Bangalore Region
d.	To display all employees data by their names is Ascending order
 */
namespace Assignment_7
{

    class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpCity { get; set; }
        public double EmpSalary { get; set; }

        public static void DisplayAll(List<Employee> employees)
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"ID: {emp.EmpId}, Name: {emp.EmpName}, City: {emp.EmpCity}, Salary: {emp.EmpSalary}");
            }
        }

        public static void DisplayBySalary(List<Employee> employees, double limit)
        {
            var filtered = employees.Where(e => e.EmpSalary > limit);
            DisplayAll(filtered.ToList());
        }

        public static void DisplayByCity(List<Employee> employees, string city)
        {

            var filtered = employees.Where(e => e.EmpCity.ToLower() == city.ToLower());
            DisplayAll(filtered.ToList());
        }

        public static void DisplaySortedByName(List<Employee> employees)
        {
            var sorted = employees.OrderBy(e => e.EmpName);
            DisplayAll(sorted.ToList());
        }
    }

    class Employee_Code
    {
        static void Main()
        {
            List<Employee> employees = new List<Employee>();
            Console.Write("How many employees do you want to enter? ");
            int count=0;
            bool ValidCount = false;

            while (!ValidCount || count <= 0)
            {
                Console.Write("Enter the number of employees: ");
                try
                {
                    count = Convert.ToInt32(Console.ReadLine());
                    ValidCount = true;
                    if (count <= 0)
                    {
                        Console.WriteLine("Please enter a valid positive number.");
                        ValidCount = false;
                    }
                }

                catch
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }


            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nEnter details for Employee {i + 1}:");


                int empId = 0;
                bool validEmpId = false;
                while (!validEmpId)
                {
                    Console.Write("EmpId: ");
                    try
                    {
                        empId = Convert.ToInt32(Console.ReadLine());
                        validEmpId = true;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input. Enter a valid EmpId (integer).");
                    }
                }


                Console.Write("EmpName: ");
                string empName = Console.ReadLine();

                Console.Write("EmpCity: ");
                string empCity = Console.ReadLine();


                double empSalary = 0;
                bool validSalary = false;
                while (!validSalary || empSalary < 0)
                {
                    Console.Write("EmpSalary: ");
                    try
                    {

                        empSalary = Convert.ToDouble(Console.ReadLine());
                        validSalary = true;
                        if (empSalary < 0)
                        {
                            Console.WriteLine("Salary must be a positive number.");
                            validSalary = false;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input. Enter a valid salary (positive number).");
                    }
                }


                employees.Add(new Employee
                {
                    EmpId = empId,
                    EmpName = empName,
                    EmpCity = empCity,
                    EmpSalary = empSalary
                });
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Display all employees");
                Console.WriteLine("2. Display employees with salary > 45000");
                Console.WriteLine("3. Display employees from Bangalore");
                Console.WriteLine("4. Display employees sorted by name (ascending)");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nAll Employees:");
                        Employee.DisplayAll(employees);
                        break;

                    case "2":
                        Console.WriteLine("\nEmployees with salary > 45000:");
                        Employee.DisplayBySalary(employees, 45000);
                        break;

                    case "3":
                        Console.WriteLine("\nEmployees from Bangalore:");
                        Employee.DisplayByCity(employees, "Bangalore");
                        break;

                    case "4":
                        Console.WriteLine("\nEmployees sorted by name:");
                        Employee.DisplaySortedByName(employees);
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting program...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }
        public static void DisplayEmployees(IEnumerable<Employee> employees)
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"EmployeeID:{emp.EmployeeID}  FirstName:{emp.FirstName}  LastName:{emp.LastName}  " +
                    $"Title:{emp.Title},  DOB:{emp.DOB.ToShortDateString()}  DOJ: {emp.DOJ.ToShortDateString()},  City: {emp.City}");
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = new List<Employee>
            {
                new Employee { EmployeeID = 1001, FirstName = "Malcolm", LastName = "Daruwalla", Title = "Manager", DOB = new DateTime(1984, 11, 16), DOJ = new DateTime(2011, 6, 8), City = "Mumbai" },
                new Employee { EmployeeID = 1002, FirstName = "Asdin", LastName = "Dhalla", Title = "AsstManager", DOB = new DateTime(1984, 8, 20), DOJ = new DateTime(2012, 7, 7), City = "Mumbai" },
                new Employee { EmployeeID = 1003, FirstName = "Madhavi", LastName = "Oza", Title = "Consultant", DOB = new DateTime(1987, 11, 14), DOJ = new DateTime(2015, 4, 12), City = "Pune" },
                new Employee { EmployeeID = 1004, FirstName = "Saba", LastName = "Shaikh", Title = "SE", DOB = new DateTime(1990, 6, 3), DOJ = new DateTime(2016, 2, 2), City = "Pune" },
                new Employee { EmployeeID = 1005, FirstName = "Nazia", LastName = "Shaikh", Title = "SE", DOB = new DateTime(1991, 3, 8), DOJ = new DateTime(2016, 2, 2), City = "Mumbai" },
                new Employee { EmployeeID = 1006, FirstName = "Amit", LastName = "Pathak", Title = "Consultant", DOB = new DateTime(1989, 11, 7), DOJ = new DateTime(2014, 8, 8), City = "Chennai" },
                new Employee { EmployeeID = 1007, FirstName = "Vijay", LastName = "Natrajan", Title = "Consultant", DOB = new DateTime(1989, 12, 2), DOJ = new DateTime(2015, 6, 1), City = "Mumbai" },
                new Employee { EmployeeID = 1008, FirstName = "Rahul", LastName = "Dubey", Title = "Associate", DOB = new DateTime(1993, 11, 11), DOJ = new DateTime(2014, 11, 6), City = "Chennai" },
                new Employee { EmployeeID = 1009, FirstName = "Suresh", LastName = "Mistry", Title = "Associate", DOB = new DateTime(1992, 8, 12), DOJ = new DateTime(2014, 12, 3), City = "Chennai" },
                new Employee { EmployeeID = 1010, FirstName = "Sumit", LastName = "Shah", Title = "Manager", DOB = new DateTime(1991, 4, 12), DOJ = new DateTime(2016, 1, 2), City = "Pune" }
            };

            // 1. Display a list of all the employee who have joined before 1/1/2015

            Console.WriteLine("Query1........................................................................\n");
            var employees_Joined_Before_01012015 = employeeList.Where(emp => emp.DOJ < new DateTime(2015, 1, 1));
            Console.WriteLine("Employees who all joined before 1/1/2015:");
            Employee.DisplayEmployees(employees_Joined_Before_01012015);
            Console.WriteLine();

            // 2. Display a list of all the employee whose date of birth is after 1/1/1990
            Console.WriteLine("Query2........................................................................\n");
            var employees_Born_After_01011990 = employeeList.Where(emp => emp.DOB > new DateTime(1990, 1, 1));
            Console.WriteLine("Employees whose date of birth is after 1/1/1990:");
            Employee.DisplayEmployees(employees_Born_After_01011990);
            Console.WriteLine();

            // 3. Display a list of all the employee whose designation is Consultant and Associate
            Console.WriteLine("Query3........................................................................\n");
            var consultant_Or_Associate_Employees = employeeList.Where(emp => emp.Title == "Consultant" || emp.Title == "Associate");
            Console.WriteLine("Employees whose designation is Consultant or Associate:");
            Employee.DisplayEmployees(consultant_Or_Associate_Employees);
            Console.WriteLine();

            // 4.Display total number of employees
            Console.WriteLine("Query4........................................................................\n");
            int total_Employees = employeeList.Count;
            Console.WriteLine($"Total number of employees: {total_Employees}");
            Console.WriteLine();

            // 5.Display total number of employees belonging to “Chennai”
            Console.WriteLine("Query5........................................................................\n");
            int employees_In_Chennai = employeeList.Count(emp => emp.City == "Chennai");
            Console.WriteLine($"Total number of employees in Chennai: {employees_In_Chennai}");
            Console.WriteLine();

            // 6.Display highest employee id from the list
            Console.WriteLine("Query6........................................................................\n");
            int highest_Employee_ID = employeeList.Max(emp => emp.EmployeeID);
            Console.WriteLine($"Highest Employee ID: {highest_Employee_ID}");
            Console.WriteLine();

            // 7. Display total number of employee who have joined after 1/1/2015
            Console.WriteLine("Query7........................................................................\n");
            int employees_Joined_After_01012015 = employeeList.Count(emp => emp.DOJ > new DateTime(2015, 1, 1));
            Console.WriteLine($"Total number of employees joined after 1/1/2015: {employees_Joined_After_01012015}");
            Console.WriteLine();

            // 8. Display total number of employee whose designation is not “Associate”
            Console.WriteLine("Query8........................................................................\n");
            int employees_Not_Associate = employeeList.Count(emp => emp.Title != "Associate");
            Console.WriteLine($"Total number of employees not designated as 'Associate': {employees_Not_Associate}");
            Console.WriteLine();

            // 9. Display total number of employee based on City
            Console.WriteLine("Query9........................................................................\n");
            var employees_By_City = employeeList.GroupBy(emp => emp.City)
                                         .Select(g => new { City = g.Key, Count = g.Count() });
            Console.WriteLine("Total number of employees based on City:");
            foreach (var group in employees_By_City)
            {
                Console.WriteLine($"{group.City}: {group.Count}");
            }
            Console.WriteLine();

            // 10. Display total number of employee based on city and title
            Console.WriteLine("Query10........................................................................\n");
            var employees_By_City_And_Title = employeeList.GroupBy(emp => new { emp.City, emp.Title })
                                                 .Select(g => new { City = g.Key.City, Title = g.Key.Title, Count = g.Count() });
            Console.WriteLine("Total number of employees based on City and Title:");
            foreach (var group in employees_By_City_And_Title)
            {
                Console.WriteLine($"{group.City} - {group.Title}: {group.Count}");
            }
            Console.WriteLine();

            // 11.  Display total number of employee who is youngest in the list
            Console.WriteLine("Query11........................................................................\n");
            DateTime youngest_DOB = employeeList.Max(emp => emp.DOB);
            var youngest_Employees = employeeList.Where(emp => emp.DOB == youngest_DOB);
            Console.WriteLine($"Total number of employees who are youngest in the list- (DOB: {youngest_DOB.ToShortDateString()})");
            Employee.DisplayEmployees(youngest_Employees);

            Console.Read();

        }
    }
}

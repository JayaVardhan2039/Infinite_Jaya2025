using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeChallenge_1
{
    class Question_1
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        static SqlDataReader dr = null;

        static string name, gender;
        static float givenSalary;
        static int generatedEmpId;
        static float calculatedSalary;

        static void Main(string[] args)
        {
            Console.WriteLine(".............Collecting Data.................");
            Ask_Employee_Data();
            Console.WriteLine(".............Inserting Data into the Table.................");
            Insert_Employee_Details();
            Console.WriteLine(".............Displaying Data.................");
            Display_Choice();
            Console.ReadLine();
        }

        static SqlConnection GetConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-7ZS9J84\\SQLEXPRESS;Initial Catalog=CodeChallenges;" +
                "user id=sa;password=Jayavardhan@2003;");
            con.Open();
            return con;
        }

        static void Ask_Employee_Data()
        {
            Console.WriteLine("Enter Employee Name:");
            name = Console.ReadLine();

            Console.WriteLine("Enter Gender:");
            gender = Console.ReadLine();

            Console.WriteLine("Enter Given Salary:");
            givenSalary = Convert.ToSingle(Console.ReadLine());
        }

        static void Insert_Employee_Details()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_employeedetailsinsert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@name";
                p1.Value = name;
                p1.DbType = DbType.String;
                p1.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@gender";
                p2.Value = gender;
                p2.DbType = DbType.String;
                p2.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@givensalary";
                p3.Value = givenSalary;
                p3.DbType = DbType.Single;
                p3.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p3);

                SqlParameter outEmpId = new SqlParameter();
                outEmpId.ParameterName = "@empid";
                outEmpId.DbType = DbType.Int32;
                outEmpId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outEmpId);

                SqlParameter outSalary = new SqlParameter();
                outSalary.ParameterName = "@salary";
                outSalary.DbType = DbType.Single;
                outSalary.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outSalary);

                cmd.ExecuteNonQuery();

                generatedEmpId = Convert.ToInt32(outEmpId.Value);
                calculatedSalary = Convert.ToSingle(outSalary.Value);

                Console.WriteLine($"Generated EmpId: {generatedEmpId}");
                Console.WriteLine($"Salary after 10% deduction: {calculatedSalary}");
                Console.WriteLine($"Net Salary (Salary - 10%): {calculatedSalary * 0.9}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while inserting employee details: " + ex.Message);
            }
        }

        static void Display_Choice()
        {
            try
            {
                Console.WriteLine("Type 'recent' for recent row or 'all' for entire table:");
                string choice = Console.ReadLine().ToLower();

                if (choice == "recent")
                {
                    cmd = new SqlCommand("select * from employee_details where empid = @eid", con);

                    SqlParameter p4 = new SqlParameter();
                    p4.ParameterName = "@eid";
                    p4.Value = generatedEmpId;
                    p4.DbType = DbType.Int32;
                    p4.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(p4);
                }
                else if (choice == "all")
                {
                    cmd = new SqlCommand("select * from employee_details", con);
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    return;
                }

                dr = cmd.ExecuteReader();
                Console.WriteLine("--- Employee Data ---");
                while (dr.Read())
                {
                    Console.WriteLine($"EmpId: {dr["EmpId"]}");
                    Console.WriteLine($"Name: {dr["Name"]}");
                    Console.WriteLine($"Gender: {dr["Gender"]}");
                    Console.WriteLine($"Salary: {dr["Salary"]}");
                    Console.WriteLine($"NetSalary: {dr["NetSalary"]}");
                }

                
                con.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error while displaying employee details: " + ex.Message);
            }
        }
    }
}

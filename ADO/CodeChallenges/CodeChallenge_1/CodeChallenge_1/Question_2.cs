using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CodeChallenge_1
{
    class Question_2
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        static SqlDataReader dr = null;

        static int empId;
        static float updatedSalary;

        static void Main(string[] args)
        {
            Console.WriteLine(".............Collecting Data.................");
            Ask_EmpId();
            Console.WriteLine(".............Displaying Data Before Update.................");
            Display_Employee_Before_Update();
            Console.WriteLine(".............Updating Data into the Table.................");
            Update_Salary();
            Console.WriteLine(".............Displaying Data.................");
            Display_Updated_Employee();
            Console.ReadLine();
        }

        static SqlConnection GetConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-7ZS9J84\\SQLEXPRESS;Initial Catalog=CodeChallenges;" +
                "user id=sa;password=Jayavardhan@2003;");
            con.Open();
            return con;
        }

        static void Ask_EmpId()
        {
            Console.WriteLine("Please Enter the Employee ID to update salary:");
            empId = Convert.ToInt32(Console.ReadLine());
        }

        static void Update_Salary()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_salarybyempid_update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@empid";
                p1.Value = empId;
                p1.DbType = DbType.Int32;
                p1.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@updatedsalary";
                p2.DbType = DbType.Single;
                p2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(p2);

                cmd.ExecuteNonQuery();

                updatedSalary = Convert.ToSingle(p2.Value);
                Console.WriteLine($"Updated Salary for EmpId {empId}: {updatedSalary}");
            }
            catch (SqlException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        static void Display_Employee_Before_Update()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from employee_details where empid = @eid", con);

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@eid";
                p.Value = empId;
                p.DbType = DbType.Int32;
                p.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p);

                dr = cmd.ExecuteReader();
                Console.WriteLine(".......Employee Details Before Update........");
                while (dr.Read())
                {
                    Console.WriteLine($"EmpId: {dr["EmpId"]}");
                    Console.WriteLine($"Name: {dr["Name"]}");
                    Console.WriteLine($"Gender: {dr["Gender"]}");
                    Console.WriteLine($"Salary: {dr["Salary"]}");
                    Console.WriteLine($"NetSalary: {dr["NetSalary"]}");
                }
                dr.Close();
            }
            catch (SqlException except)
            {
                Console.WriteLine(except.Message);
            }
        }

        static void Display_Updated_Employee()
        {
            try
            {
                cmd = new SqlCommand("select * from employee_details where empid = @eid", con);

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@eid";
                p3.Value = empId;
                p3.DbType = DbType.Int32;
                p3.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p3);

                dr = cmd.ExecuteReader();
                Console.WriteLine(".......Updated Employee Details........");
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
            catch (SqlException except)
            {
                Console.WriteLine(except.Message);
            }
        }
    }
}

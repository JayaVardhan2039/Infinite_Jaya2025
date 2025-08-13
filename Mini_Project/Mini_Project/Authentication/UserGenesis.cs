using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data.SqlClient;
using Mini_Project.User_The_Passenger;

namespace Mini_Project.Authentication
{
    //User Registration Logic
    static class UserGenesis
    {
        internal static void RegisterUser()
        {


            Console.Write("Enter your First Name: ");
            string firstName = Console.ReadLine();
            while (string.IsNullOrEmpty(firstName))
            {
                Console.Write("First Name cannot be empty. Please enter again: ");
                firstName = Console.ReadLine();
            }

            Console.Write("Enter your Last Name: ");
            string lastName = Console.ReadLine();
            while (string.IsNullOrEmpty(lastName))
            {
                Console.Write("Last Name cannot be empty. Please enter again: ");
                lastName = Console.ReadLine();
            }



            Console.Write("Enter your Phone Number: ");
            string phone = Console.ReadLine();
            while (phone.Length != 10)
            {
                Console.Write("Invalid phone number. Enter a 10-digit number: ");
                phone = Console.ReadLine();
            }


            Console.Write("Enter your Email: ");
            string email = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                Console.Write("Invalid email. Please enter a valid email: ");
                email = Console.ReadLine();
            }


            Console.Write("Enter your desired Password: ");
            string password = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                Console.Write("Password must be at least 6 characters. Try again: ");
                password = Console.ReadLine();
            }
            Console.Write("Confirm your Password: ");
            string confirmPassword = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(confirmPassword))
            {
                Console.Write("Confirmation cannot be empty. Try again: ");
                confirmPassword = Console.ReadLine();
            }
            string role = "";
            if (confirmPassword == password)
            { 
                role = "user";
            }
            else if (new string(password.Reverse().ToArray()) == confirmPassword)
            {
                Console.Write("Admin registration detected...");

                Console.Write("Enter Admin Code: ");
                string adminCode = Console.ReadLine();

                if (adminCode =="ADMINFIRAIL")
                {
                    role = "admin";
                    Console.WriteLine("Admin verified.");
                }
                else
                {
                    Console.WriteLine("Invalid admin code. Registration aborted.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Passwords do not match. Registration aborted.");
                return;
            }


            Console.WriteLine();
            Console.WriteLine("Choose your username option:");
            Console.WriteLine("1. Use First Name");
            Console.WriteLine("2. Use Last Name");
            Console.WriteLine("3. Combine your First and Last Name");
            Console.WriteLine("4. Enter a Custom Username");
            Console.Write("Enter your choice (1-4): ");
            int choice = Convert.ToInt32(Console.ReadLine());

            string username = "";

            if (choice == 1)
                username = firstName;
            else if (choice == 2)
                username = lastName;
            else if (choice == 3)
                username = firstName + lastName;
            else if (choice == 4)
            {
                Console.Write("Enter your custom username: ");
                username = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(username))
                {
                    Console.Write("Username cannot be empty. Enter again: ");
                    username = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Defaulting to combined name.");
                username = firstName + lastName;
            }

            bool success = Register(firstName, lastName, phone, email, password, username,role);
            Console.WriteLine(success ? "Registration successful!" : "Registration failed.");
            Console.WriteLine($"Yourusername is {username}");

            Console.WriteLine($"Yourpassword is {password}");
            Thread.Sleep(3000);

            // Move cursor up one line and overwrite the password line
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine();

            if (success)
            {
                var credentials = LoginFlow.Authenticate(username, password);
                if (credentials != null)
                {
                    User.LoggedInUserId = credentials.Value.userid;
                    User.LoggedInUsername = credentials.Value.username;
                    User.UserMenu(); // Redirect to user menu
                }
            }

        }
        internal static bool Register(string firstName, string lastName, string phone, string email, string password, string username,string role)
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand(
                    "INSERT INTO Users (username, pasword, firstname, lastname, phone, email, roles) " +
                    "VALUES (@username, @pasword, @firstname, @lastname, @phone, @email,@role)", DBConfig.Connection);

                DBConfig.Command.Parameters.AddWithValue("@username", username);
                DBConfig.Command.Parameters.AddWithValue("@pasword", password);
                DBConfig.Command.Parameters.AddWithValue("@firstname", firstName);
                DBConfig.Command.Parameters.AddWithValue("@lastname", lastName);
                DBConfig.Command.Parameters.AddWithValue("@phone", phone);
                DBConfig.Command.Parameters.AddWithValue("@email", email);
                DBConfig.Command.Parameters.AddWithValue("@role", role);

                int rowsAffected = DBConfig.Command.ExecuteNonQuery();
                DBConfig.CloseConnection();

                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                DBConfig.CloseConnection();

                if (ex.Message.Contains("CHECK constraint"))
                {
                    Console.WriteLine("Registration failed due to invalid phone number or email format.");
                }
                else
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                return false;
            }
        }



        //for testing purpose
        internal static bool RegisterUserWithData(string firstName, string lastName, string phone, string email, string password, string username)
        {
            try
            {
                DBConfig.OpenConnection();

                SqlTransaction transaction = DBConfig.Connection.BeginTransaction();

                DBConfig.Command = new SqlCommand(
                    "INSERT INTO Users (username, pasword, firstname, lastname, phone, email, roles) " +
                    "VALUES (@username, @pasword, @firstname, @lastname, @phone, @email, 'user')",
                    DBConfig.Connection, transaction);

                DBConfig.Command.Parameters.AddWithValue("@username", username);
                DBConfig.Command.Parameters.AddWithValue("@pasword", password);
                DBConfig.Command.Parameters.AddWithValue("@firstname", firstName);
                DBConfig.Command.Parameters.AddWithValue("@lastname", lastName);
                DBConfig.Command.Parameters.AddWithValue("@phone", phone);
                DBConfig.Command.Parameters.AddWithValue("@email", email);

                int rowsAffected = DBConfig.Command.ExecuteNonQuery();

                transaction.Rollback(); // Rollback to avoid affecting real data

                DBConfig.CloseConnection();

                return rowsAffected > 0;
            }
            catch (SqlException)
            {
                DBConfig.CloseConnection();
                return false;
            }
        }

    }
}

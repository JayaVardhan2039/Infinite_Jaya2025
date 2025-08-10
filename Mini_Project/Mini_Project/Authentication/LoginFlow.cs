using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Mini_Project.User_The_Passenger;
using Mini_Project.User_The_Admin;

namespace Mini_Project.Authentication
{
    //All Login Logics
    static class LoginFlow
    {
        internal static void UserLogin()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(username))
            {
                Console.Write("Username cannot be empty. Please enter again: ");
                username = Console.ReadLine();
            }

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                Console.Write("Password must be at least 6 characters. Try again: ");
                password = Console.ReadLine();
            }

            var credentials = Login("user", username, password);
            if (credentials != null)
            {
                User.LoggedInUserId = credentials.Value.userid;
                User.LoggedInUsername = credentials.Value.username;
                User.UserMenu();
            }
            else
            {
                Console.WriteLine("Login failed. Please try again.");
            }
        }
        internal static void AdminLogin()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(username))
            {
                Console.Write("Username cannot be empty. Please enter again: ");
                username = Console.ReadLine();
            }

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                Console.Write("Password must be at least 6 characters. Try again: ");
                password = Console.ReadLine();
            }

            var credentials = Login("admin", username, password);
            if (credentials != null)
            {
                Admin.LoggedInAdminId = credentials.Value.userid;
                Admin.LoggedInAdminUsername = credentials.Value.username;
                Admin.AdminMenu();
            }
            else
            {
                Console.WriteLine("Admin login failed. Please try again.");
            }
        }
        internal static (int userid, string username)? Login(string role, string username, string password)
        {
            DBConfig.OpenConnection();

            DBConfig.Command = new SqlCommand("SELECT userid, username FROM Users WHERE username = @username AND pasword = @password AND roles = @role", DBConfig.Connection);
            DBConfig.Command.Parameters.AddWithValue("@username", username);
            DBConfig.Command.Parameters.AddWithValue("@password", password);
            DBConfig.Command.Parameters.AddWithValue("@role", role);

            DBConfig.Reader = DBConfig.Command.ExecuteReader();
            if (DBConfig.Reader.Read())
            {
                int userid = (int)DBConfig.Reader["userid"];
                string uname = DBConfig.Reader["username"].ToString();
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                return (userid, uname);
            }

            DBConfig.CloseReader();
            DBConfig.CloseConnection();
            Console.WriteLine("Invalid credentials or role.");
            return null;
        }
        internal static (int userid, string username)? Authenticate(string username, string password)
        {
            DBConfig.OpenConnection();

            DBConfig.Command = new SqlCommand("SELECT userid, username FROM Users WHERE username = @username AND pasword = @password", DBConfig.Connection);
            DBConfig.Command.Parameters.AddWithValue("@username", username);
            DBConfig.Command.Parameters.AddWithValue("@password", password);

            DBConfig.Reader = DBConfig.Command.ExecuteReader();
            if (DBConfig.Reader.Read())
            {
                int userid = (int)DBConfig.Reader["userid"];
                string uname = DBConfig.Reader["username"].ToString();
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                return (userid, uname);
            }

            DBConfig.CloseReader();
            DBConfig.CloseConnection();
            return null;
        }
    }
}

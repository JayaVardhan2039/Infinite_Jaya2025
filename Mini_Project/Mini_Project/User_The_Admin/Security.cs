using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Admin
{
    //Admin Blocking
    class Security
    {
        internal static void BlockUser()
        {
            try
            {
                DBConfig.OpenConnection();

                // Display only users with role 'user'
                DBConfig.Command = new SqlCommand("SELECT userid, username, roles, blocked FROM Users WHERE roles = 'user'", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n--- USERS LIST (Role: user) ---");
                while (DBConfig.Reader.Read())
                {
                    Console.WriteLine($"User ID: {DBConfig.Reader["userid"]}, Username: {DBConfig.Reader["username"]}, Blocked: {(bool)DBConfig.Reader["blocked"]}");
                }

                DBConfig.CloseReader();

                Console.Write("\nEnter User ID to block/unblock: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter action (block/unblock): ");
                string action = Console.ReadLine().ToLower();

                bool blockValue = action == "block";

                DBConfig.Command = new SqlCommand("UPDATE Users SET blocked = @blocked WHERE userid = @userid AND roles = 'user'", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@blocked", blockValue);
                DBConfig.Command.Parameters.AddWithValue("@userid", userId);

                int rowsAffected = DBConfig.Command.ExecuteNonQuery();

                Console.WriteLine(rowsAffected > 0 ? $"User {(blockValue ? "blocked" : "unblocked")} successfully." : "Action failed. User not found or not a regular user.");

                DBConfig.CloseConnection();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        //for testing purpose
        internal static bool TestUpdateUserBlockStatus(int userId, bool blockValue)
        {
            try
            {
                DBConfig.OpenConnection();
                SqlTransaction transaction = DBConfig.Connection.BeginTransaction();

                DBConfig.Command = new SqlCommand("UPDATE Users SET blocked = @blocked WHERE userid = @userid AND roles = 'user'", DBConfig.Connection, transaction);
                DBConfig.Command.Parameters.AddWithValue("@blocked", blockValue);
                DBConfig.Command.Parameters.AddWithValue("@userid", userId);

                int rowsAffected = DBConfig.Command.ExecuteNonQuery();
                transaction.Rollback(); // Prevent actual change

                DBConfig.CloseConnection();
                return rowsAffected > 0;
            }
            catch
            {
                DBConfig.CloseConnection();
                return false;
            }
        }

    }
}

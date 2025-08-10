using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Mini_Project.User_The_Passenger
{
    //User Mailing Logics
    static class Mailing
    {
        internal static void MailingMenu(int userId, string role)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Mailing options..");
                Console.WriteLine("1. Send Mail to Admins");
                Console.WriteLine("2. View your Inbox");
                Console.WriteLine("3. Back");
                Console.Write("Enter choice: ");

                string input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                    continue;
                }

                switch (choice)
                {
                    case 1: SendMail(userId, role); break;
                    case 2: ViewInbox(userId, role); break;
                    case 3: return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }
        internal static void SendMail(int senderId, string senderRole)
        {
            try
            {
                DBConfig.OpenConnection();

                if (senderRole == "user")
                {
                    Console.Write("Enter your message to admin(s): ");
                    string message = Console.ReadLine();

                    DBConfig.Command = new SqlCommand("sp_SendMailToAdmins", DBConfig.Connection);
                    DBConfig.Command.CommandType = CommandType.StoredProcedure;
                    DBConfig.Command.Parameters.AddWithValue("@sender_id", senderId);
                    DBConfig.Command.Parameters.AddWithValue("@message_text", message);
                    DBConfig.Command.ExecuteNonQuery();

                    Console.WriteLine("Mail sent to all admins.");
                }
                else if (senderRole == "admin")
                {
                    Console.Write("Enter Receiver User ID: ");
                    int receiverId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter your message: ");
                    string message = Console.ReadLine();

                    DBConfig.Command = new SqlCommand("sp_SendMailToUser", DBConfig.Connection);
                    DBConfig.Command.CommandType = CommandType.StoredProcedure;
                    DBConfig.Command.Parameters.AddWithValue("@sender_id", senderId);
                    DBConfig.Command.Parameters.AddWithValue("@receiver_id", receiverId);
                    DBConfig.Command.Parameters.AddWithValue("@message_text", message);
                    DBConfig.Command.ExecuteNonQuery();

                    Console.WriteLine("Mail sent to user.");
                }

                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseConnection();
                Console.WriteLine("An error occurred while sending the mail.");
            }
        }
        internal static void ViewInbox(int userId, string role)
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("sp_ViewInbox", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;
                DBConfig.Command.Parameters.AddWithValue("@user_id", userId);
                DBConfig.Command.Parameters.AddWithValue("@role", role);

                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n--- Inbox ---");
                while (DBConfig.Reader.Read())
                {
                    Console.WriteLine($"From ({DBConfig.Reader["sender_role"]} ID {DBConfig.Reader["sender_id"]}) at {DBConfig.Reader["sent_at"]}: {DBConfig.Reader["message_text"]}");
                }

                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine("An error occurred while retrieving inbox messages.");
            }
        }



        //for testing purpose
        internal static bool TestSendMail(int senderId, string senderRole, int? receiverId, string message)
        {
            try
            {
                DBConfig.OpenConnection();
                SqlTransaction transaction = DBConfig.Connection.BeginTransaction();

                if (senderRole == "user")
                {
                    DBConfig.Command = new SqlCommand("sp_SendMailToAdmins", DBConfig.Connection, transaction);
                    DBConfig.Command.CommandType = CommandType.StoredProcedure;
                    DBConfig.Command.Parameters.AddWithValue("@sender_id", senderId);
                    DBConfig.Command.Parameters.AddWithValue("@message_text", message);
                }
                else if (senderRole == "admin" && receiverId.HasValue)
                {
                    DBConfig.Command = new SqlCommand("sp_SendMailToUser", DBConfig.Connection, transaction);
                    DBConfig.Command.CommandType = CommandType.StoredProcedure;
                    DBConfig.Command.Parameters.AddWithValue("@sender_id", senderId);
                    DBConfig.Command.Parameters.AddWithValue("@receiver_id", receiverId.Value);
                    DBConfig.Command.Parameters.AddWithValue("@message_text", message);
                }
                else
                {
                    DBConfig.CloseConnection();
                    return false;
                }

                DBConfig.Command.ExecuteNonQuery();
                transaction.Rollback(); // Prevent actual mail sending
                DBConfig.CloseConnection();

                return true;
            }
            catch
            {
                DBConfig.CloseConnection();
                return false;
            }
        }
    }
}

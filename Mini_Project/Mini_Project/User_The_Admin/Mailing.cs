using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Admin
{
    //Admin Mailing Logics
    class Mailing
    {
        internal static void MailingMenu(int adminId, string role)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Mailing..");
                Console.WriteLine("1. Send Mail to User");
                Console.WriteLine("2. View Inbox");
                Console.WriteLine("3. Back");
                Console.Write("Enter choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: SendMail(adminId, role); break;
                    case 2: ViewInbox(adminId, role); break;
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
                    // Send to all admins
                    DBConfig.Command = new SqlCommand("SELECT userid FROM Users WHERE roles = 'admin'", DBConfig.Connection);
                    DBConfig.Reader = DBConfig.Command.ExecuteReader();

                    List<int> adminIds = new List<int>();
                    while (DBConfig.Reader.Read())
                    {
                        adminIds.Add((int)DBConfig.Reader["userid"]);
                    }

                    DBConfig.CloseReader();

                    Console.Write("Enter your message to admin(s): ");
                    string message = Console.ReadLine();

                    foreach (int adminId in adminIds)
                    {
                        DBConfig.Command = new SqlCommand(
                            "INSERT INTO Mails (sender_id, receiver_id, sender_role, receiver_role, message_text) " +
                            "VALUES (@sender, @receiver, @srole, @rrole, @text)",
                            DBConfig.Connection);

                        DBConfig.Command.Parameters.AddWithValue("@sender", senderId);
                        DBConfig.Command.Parameters.AddWithValue("@receiver", adminId);
                        DBConfig.Command.Parameters.AddWithValue("@srole", "user");
                        DBConfig.Command.Parameters.AddWithValue("@rrole", "admin");
                        DBConfig.Command.Parameters.AddWithValue("@text", message);

                        DBConfig.Command.ExecuteNonQuery();
                    }

                    Console.WriteLine("Mail sent to all admins.");
                }
                else if (senderRole == "admin")
                {
                    Console.Write("Enter Receiver User ID: ");
                    int receiverId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter your message: ");
                    string message = Console.ReadLine();

                    DBConfig.Command = new SqlCommand(
                        "INSERT INTO Mails (sender_id, receiver_id, sender_role, receiver_role, message_text) " +
                        "VALUES (@sender, @receiver, @srole, @rrole, @text)",
                        DBConfig.Connection);

                    DBConfig.Command.Parameters.AddWithValue("@sender", senderId);
                    DBConfig.Command.Parameters.AddWithValue("@receiver", receiverId);
                    DBConfig.Command.Parameters.AddWithValue("@srole", "admin");
                    DBConfig.Command.Parameters.AddWithValue("@rrole", "user");
                    DBConfig.Command.Parameters.AddWithValue("@text", message);

                    DBConfig.Command.ExecuteNonQuery();

                    Console.WriteLine("Mail sent to user.");
                }

                DBConfig.CloseConnection();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error sending mail: {ex.Message}");
            }
        }
        internal static void ViewInbox(int userId, string role)
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand(
                    "SELECT * FROM Mails WHERE receiver_id = @id AND receiver_role = @role ORDER BY sent_at DESC",
                    DBConfig.Connection);

                DBConfig.Command.Parameters.AddWithValue("@id", userId);
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
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error retrieving inbox: {ex.Message}");
            }
        }

    }
}

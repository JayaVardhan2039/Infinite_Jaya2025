using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Passenger
{
    //User Session Logics
    static class User
    {
        internal static int LoggedInUserId { set; get; }
        internal static string LoggedInUsername { set; get; }
        internal static void UserMenu()
        {
            bool isBlocked = IsUserBlocked(LoggedInUserId);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"Welcome {LoggedInUsername},");
                

                if (TravelBackend.HasUnreadMails(LoggedInUserId))
                {
                    Console.WriteLine("Excuse Me::You have new mails to check!");
                }

                Console.WriteLine("What do you want to do?..");
                Console.WriteLine("1. Book Tickets");
                Console.WriteLine("2. Cancel Tickets");
                Console.WriteLine("3. Mailing");
                Console.WriteLine("4. My Travel Plans");
                Console.WriteLine("5. Surf the trains");
                Console.WriteLine("6. Logout");
                
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        if (isBlocked)
                            Console.WriteLine("You are blocked by the admin. Booking is not allowed. Write to admin via Mail");
                        else
                            Ledger.BookTickets();
                        break;
                    case 2:
                        if (isBlocked)
                            Console.WriteLine("You are blocked by the admin. Cancellation is not allowed. Write to admin via Mail");
                        else
                            Ledger.CancelTickets();
                        break;
                    case 3:
                        Mailing.MailingMenu(LoggedInUserId, "user");
                        break;
                    case 4:
                        Ledger.ShowConfirmedBookings(LoggedInUserId);
                        break;
                    case 5:
                        TravelBackend.DisplayAllTrainsForUser();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        internal static bool IsUserBlocked(int userId)
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("sp_IsUserBlocked", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;

                DBConfig.Command.Parameters.AddWithValue("@userid", userId);

                SqlParameter outputParam = new SqlParameter("@isBlocked", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                DBConfig.Command.Parameters.Add(outputParam);

                DBConfig.Command.ExecuteNonQuery();

                DBConfig.CloseConnection();

                return (bool)outputParam.Value;
            }
            catch (SqlException)
            {
                DBConfig.CloseConnection();
                return false;
            }
        }
        }
}

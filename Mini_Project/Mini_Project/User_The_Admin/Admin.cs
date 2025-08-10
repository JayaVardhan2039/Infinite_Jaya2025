using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Admin
{
    //Admin Main Menu
    static class Admin
    {
       
        internal static int LoggedInAdminId;
        internal static string LoggedInAdminUsername;
        internal static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Hi,Admin! " + LoggedInAdminUsername);
                Console.WriteLine("Menu..");
                Console.WriteLine("1. View Bookings");
                Console.WriteLine("2. View Cancellations");
                Console.WriteLine("3. Add Train");
                Console.WriteLine("4. Update Train");
                Console.WriteLine("5. Block/Unblock User");
                Console.WriteLine("6. Delete Train");
                Console.WriteLine("7. Delete Booking");
                Console.WriteLine("8. Delete Cancellation");
                Console.WriteLine("9. Mailing");
                Console.WriteLine("10. Display all the trains");
                Console.WriteLine("11. Logout");

                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 11.");
                    continue;
                }

                switch (choice)
                {
                    case 1: Manifest.ViewBookings(); break;
                    case 2: Manifest.ViewCancellations(); break;
                    case 3: Railworks. AddTrain(); break;
                    case 4: Railworks.UpdateTrain(); break;
                    case 5: Security.BlockUser(); break;
                    case 6: Railworks.DeleteTrain(); break;
                    case 7: Manifest.DeleteBooking(); break;
                    case 8: Manifest.DeleteCancellation(); break;
                    case 9: Mailing.MailingMenu(LoggedInAdminId, "admin"); break;
                    case 10: Railworks.DisplayAllTrainsForAdmin(); break;
                    case 11: return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }
    }
}

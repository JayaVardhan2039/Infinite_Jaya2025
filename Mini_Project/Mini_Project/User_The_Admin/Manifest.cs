using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Admin
{
    //Admin's second important adminstrative operations
    class Manifest
    {

        internal static void ViewBookings()
        {
            try
            {
                DBConfig.OpenConnection();

                // Active bookings
                DBConfig.Command = new SqlCommand("SELECT * FROM Bookings WHERE deleted = 0", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n--- Active Bookings ---");
                if (!DBConfig.Reader.HasRows)
                {
                    Console.WriteLine("Nothing to show.");
                }
                else
                {
                    while (DBConfig.Reader.Read())
                    {
                        Console.WriteLine(
                            $"Booking ID: {DBConfig.Reader["booking_id"]}, " +
                            $"Train No: {DBConfig.Reader["tno"]}, " +
                            $"User ID: {DBConfig.Reader["userid"]}, " +
                            $"Seats: {DBConfig.Reader["seats_booked"]}, " +
                            $"Booking Date: {DBConfig.Reader["booking_date"]}, " +
                            $"Total Amount: {DBConfig.Reader["total_amount"]}, " +
                            $"Travel Date: {DBConfig.Reader["travel_date"]}, " +
                            $"Berth: {DBConfig.Reader["berth_allotment"]}, " +
                            $"Class: {DBConfig.Reader["class_name"]}"
                        );
                    }
                }

                DBConfig.CloseReader();

                // Deleted bookings
                DBConfig.Command = new SqlCommand("SELECT * FROM Bookings WHERE deleted = 1", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n--- Deleted Bookings ---");
                if (!DBConfig.Reader.HasRows)
                {
                    Console.WriteLine("Nothing to show.");
                }
                else
                {
                    while (DBConfig.Reader.Read())
                    {
                        Console.WriteLine(
                            $"Booking ID: {DBConfig.Reader["booking_id"]}, " +
                            $"Train No: {DBConfig.Reader["tno"]}, " +
                            $"User ID: {DBConfig.Reader["userid"]}, " +
                            $"Seats: {DBConfig.Reader["seats_booked"]}, " +
                            $"Booking Date: {DBConfig.Reader["booking_date"]}, " +
                            $"Total Amount: {DBConfig.Reader["total_amount"]}, " +
                            $"Travel Date: {DBConfig.Reader["travel_date"]}, " +
                            $"Berth: {DBConfig.Reader["berth_allotment"]}, " +
                            $"Class: {DBConfig.Reader["class_name"]}"
                        );
                    }
                }
                
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine();
                ViewAllBookingsWithPassengers();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal static void ViewCancellations()
        {
            try
            {
                DBConfig.OpenConnection();

                // Active cancellations
                DBConfig.Command = new SqlCommand("SELECT * FROM Cancellations WHERE deleted = 0", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n--- Active Cancellations ---");
                if (!DBConfig.Reader.HasRows)
                {
                    Console.WriteLine("Nothing to show.");
                }
                else
                {
                    while (DBConfig.Reader.Read())
                    {
                        Console.WriteLine(
                            $"Cancellation ID: {DBConfig.Reader["cancellation_id"]}, " +
                            $"Booking ID: {DBConfig.Reader["booking_id"]}, " +
                            $"Seats Cancelled: {DBConfig.Reader["seats_cancelled"]}, " +
                            $"Date: {DBConfig.Reader["cancellation_date"]}, " +
                            $"Reason: {DBConfig.Reader["refund_reason"]}, " +
                            $"Amount Refunded: {DBConfig.Reader["refund_amount"]}"
                        );
                    }
                }

                DBConfig.CloseReader();

                // Deleted cancellations
                DBConfig.Command = new SqlCommand("SELECT * FROM Cancellations WHERE deleted = 1", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n--- Deleted Cancellations ---");
                if (!DBConfig.Reader.HasRows)
                {
                    Console.WriteLine("Nothing to show.");
                }
                else
                {
                    while (DBConfig.Reader.Read())
                    {
                        Console.WriteLine(
                            $"Cancellation ID: {DBConfig.Reader["cancellation_id"]}, " +
                            $"Booking ID: {DBConfig.Reader["booking_id"]}, " +
                            $"Seats Cancelled: {DBConfig.Reader["seats_cancelled"]}, " +
                            $"Date: {DBConfig.Reader["cancellation_date"]}, " +
                            $"Reason: {DBConfig.Reader["refund_reason"]}, " +
                            $"Amount Refunded: {DBConfig.Reader["refund_amount"]}"
                        );
                    }
                }
                
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine();
                ViewAllCancellationsWithPassengers();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal static void DeleteBooking()
        {
            ViewBookings();
            Console.Write("Enter Booking ID to delete: ");
            int bookingId = Convert.ToInt32(Console.ReadLine());

            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("UPDATE Bookings SET deleted = 1 WHERE booking_id = @booking_id", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@booking_id", bookingId);

                int rowsAffected = DBConfig.Command.ExecuteNonQuery();
                DBConfig.CloseConnection();

                Console.WriteLine(rowsAffected > 0 ? "Booking deleted successfully." : "Booking not found or already deleted.");
            }
            catch (SqlException ex)
            {
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal static void DeleteCancellation()
        {
            ViewCancellations();
            Console.Write("Enter Cancellation ID to delete: ");
            int cancellationId = Convert.ToInt32(Console.ReadLine());

            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("UPDATE Cancellations SET deleted = 1 WHERE cancellation_id = @cancellation_id", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@cancellation_id", cancellationId);

                int rowsAffected = DBConfig.Command.ExecuteNonQuery();
                DBConfig.CloseConnection();

                Console.WriteLine(rowsAffected > 0 ? "Cancellation deleted successfully." : "Cancellation not found or already deleted.");
            }
            catch (SqlException ex)
            {
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal static void ViewAllBookingsWithPassengers()
        {
            try
            {
                DBConfig.OpenConnection();
                DBConfig.Command = new SqlCommand(@"
            SELECT b.booking_id, b.userid, b.tno, b.class_name, b.travel_date, b.seats_booked,
                   p.passenger_name, p.passenger_age
            FROM Bookings b
            LEFT JOIN PassengerDetails p ON b.booking_id = p.booking_id
            WHERE b.deleted = 0
            ORDER BY b.booking_date DESC", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("=== All Bookings with Passengers ===");
                while (DBConfig.Reader.Read())
                {
                    Console.WriteLine($"Booking ID: {DBConfig.Reader["booking_id"]}, Train No: {DBConfig.Reader["tno"]}, User ID: {DBConfig.Reader["userid"]}, Class: {DBConfig.Reader["class_name"]}, Travel Date: {DBConfig.Reader["travel_date"]}");
                    Console.WriteLine($"Passenger: {DBConfig.Reader["passenger_name"]}, Age: {DBConfig.Reader["passenger_age"]}");
                    Console.WriteLine("--------------------------------");
                }
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal static void ViewAllCancellationsWithPassengers()
        {
            try
            {
                DBConfig.OpenConnection();
                DBConfig.Command = new SqlCommand(@"
    SELECT 
        c.booking_id,
        b.tno,
        b.class_name,
        b.travel_date,
        c.cancellation_date,
        c.refund_amount,
        c.refund_reason,
        p.passenger_name,
        p.passenger_age,
        p.status
    FROM Cancellations c
    JOIN Bookings b ON c.booking_id = b.booking_id
    LEFT JOIN PassengerDetails p ON c.booking_id = p.booking_id
    WHERE p.status = 'cancelled'
    ORDER BY c.cancellation_date DESC", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("=== All Cancellations with Passengers ===");
                while (DBConfig.Reader.Read())
                {
                    Console.WriteLine($"Booking ID: {DBConfig.Reader["booking_id"]}, Train No: {DBConfig.Reader["tno"]}, Class: {DBConfig.Reader["class_name"]}, Travel Date: {DBConfig.Reader["travel_date"]}");
                    Console.WriteLine($"Cancelled On: {DBConfig.Reader["cancellation_date"]}, Refund: {DBConfig.Reader["refund_amount"]}, Reason: {DBConfig.Reader["refund_reason"]}");
                    Console.WriteLine($"Passenger: {DBConfig.Reader["passenger_name"]}, Age: {DBConfig.Reader["passenger_age"]}, Status: {DBConfig.Reader["status"]}");
                    Console.WriteLine("----------------------------------------");
                }
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal static void SearchandViewUsers()
        {
            while (true)
            {
                Console.WriteLine("\n--- User Search Menu ---");
                Console.WriteLine("1. View All Users");
                Console.WriteLine("2. Search by User ID");
                Console.WriteLine("3. Search by Username");
                Console.WriteLine("4. Search by Phone Number");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 5)
                {
                    Console.WriteLine("Exiting User Search...");
                    break;
                }

                try
                {
                    DBConfig.OpenConnection();

                    switch (choice)
                    {
                        case 1:
                            DBConfig.Command = new SqlCommand("SELECT * FROM Users", DBConfig.Connection);
                            break;
                        case 2:
                            Console.Write("Enter User ID: ");
                            string userId = Console.ReadLine();
                            DBConfig.Command = new SqlCommand("SELECT * FROM Users WHERE userid = @userid", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@userid", userId);
                            break;
                        case 3:
                            Console.Write("Enter Username: ");
                            string username = Console.ReadLine();
                            DBConfig.Command = new SqlCommand("SELECT * FROM Users WHERE username LIKE @username", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@username", "%" + username + "%");
                            break;
                        case 4:
                            Console.Write("Enter Phone Number: ");
                            string phone = Console.ReadLine();
                            DBConfig.Command = new SqlCommand("SELECT * FROM Users WHERE phone = @phone", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@phone", phone);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            DBConfig.CloseConnection();
                            continue;
                    }

                    DBConfig.Reader = DBConfig.Command.ExecuteReader();
                    Console.WriteLine("\n--- User Information ---");

                    if (!DBConfig.Reader.HasRows)
                    {
                        Console.WriteLine("No users found.");
                    }
                    else
                    {
                        while (DBConfig.Reader.Read())
                        {
                            Console.WriteLine(
                                $"User ID: {DBConfig.Reader["userid"]}, " +
                                $"Username: {DBConfig.Reader["username"]}, " +
                                $"First Name: {DBConfig.Reader["firstname"]}, " +
                                $"Last Name: {DBConfig.Reader["lastname"]}, " +
                                $"Phone: {DBConfig.Reader["phone"]}, " +
                                $"Email: {DBConfig.Reader["email"]}, " +
                                $"Role: {DBConfig.Reader["roles"]}, " +
                                $"Blocked: {(Convert.ToInt32(DBConfig.Reader["blocked"]) == 1 ? "Yes" : "No")}"
                            );
                        }
                    }

                    DBConfig.CloseReader();
                    DBConfig.CloseConnection();
                }
                catch (SqlException ex)
                {
                    DBConfig.CloseReader();
                    DBConfig.CloseConnection();
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }


        //for testing purpose
        internal static bool TestDeleteBooking(int bookingId)
        {
            try
            {
                DBConfig.OpenConnection();
                SqlTransaction transaction = DBConfig.Connection.BeginTransaction();

                DBConfig.Command = new SqlCommand("UPDATE Bookings SET deleted = 1 WHERE booking_id = @booking_id", DBConfig.Connection, transaction);
                DBConfig.Command.Parameters.AddWithValue("@booking_id", bookingId);

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
        internal static bool TestDeleteCancellation(int cancellationId)
        {
            try
            {
                DBConfig.OpenConnection();
                SqlTransaction transaction = DBConfig.Connection.BeginTransaction();

                DBConfig.Command = new SqlCommand("UPDATE Cancellations SET deleted = 1 WHERE cancellation_id = @cancellation_id", DBConfig.Connection, transaction);
                DBConfig.Command.Parameters.AddWithValue("@cancellation_id", cancellationId);

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

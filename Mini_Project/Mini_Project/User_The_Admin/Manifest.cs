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

                DBConfig.Command = new SqlCommand("SELECT * FROM Bookings WHERE deleted = 0", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                if (!DBConfig.Reader.HasRows)
                {
                    Console.WriteLine("Nothing to show.");
                    DBConfig.CloseReader();
                    DBConfig.CloseConnection();
                    return;
                }

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
        internal static void ViewCancellations()
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("SELECT * FROM Cancellations WHERE deleted = 0", DBConfig.Connection);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                if (!DBConfig.Reader.HasRows)
                {
                    Console.WriteLine("Nothing to show.");
                    DBConfig.CloseReader();
                    DBConfig.CloseConnection();
                    return;
                }

                while (DBConfig.Reader.Read())
                {
                    Console.WriteLine($"Cancellation ID: {DBConfig.Reader["cancellation_id"]}, Booking ID: {DBConfig.Reader["booking_id"]}, Seats Cancelled: {DBConfig.Reader["seats_cancelled"]}, Date: {DBConfig.Reader["cancellation_date"]}, Reason: {DBConfig.Reader["refund_reason"]}, Amount Refunded: {DBConfig.Reader["refund_amount"]}");
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

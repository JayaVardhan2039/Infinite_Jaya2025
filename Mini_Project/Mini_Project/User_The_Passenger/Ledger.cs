using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Passenger
{
    //All User Transactive things Like Booking,Cancellations etc..
    static class Ledger
    {
        
        internal static void BookTickets()
        {
            int userid = User.LoggedInUserId;

            List<string> sourceCities = TravelBackend.GetDistinctSourceCities();
            string source = TravelBackend.SelectCity(sourceCities, "Source");

            List<string> destinationCities = TravelBackend.GetDestinationCitiesForSource(source);
            string destination = TravelBackend.SelectCity(destinationCities, "Destination");

            TravelBackend.DisplayAvailableTrainsForUser(source, destination);

            Console.Write("Enter Train Number: ");
            string trainInput = Console.ReadLine();
            int trainNumber;
            while (!int.TryParse(trainInput, out trainNumber))
            {
                Console.Write("Invalid input. Enter a valid Train Number: ");
                trainInput = Console.ReadLine();
            }

            TravelBackend.DisplayTrainClasses(trainNumber);

            Console.Write("Enter Class Name to Book: ");
            string selectedClass = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(selectedClass))
            {
                Console.Write("Class name cannot be empty. Enter again: ");
                selectedClass = Console.ReadLine();
            }

            Console.Write("Enter number of seats to book (max 3): ");
            string seatsInput = Console.ReadLine();
            int seatsToBook;
            while (!int.TryParse(seatsInput, out seatsToBook) || seatsToBook < 1 || seatsToBook > 3)
            {
                Console.Write("Invalid input. Enter a number between 1 and 3: ");
                seatsInput = Console.ReadLine();
            }

            Console.Write("Enter Travel Date (yyyy-mm-dd): ");
            string dateInput = Console.ReadLine();
            DateTime travelDate;
            while (!DateTime.TryParse(dateInput, out travelDate) || travelDate.Date < DateTime.Today)
            {
                Console.Write("Invalid date. Enter a valid future date (yyyy-mm-dd): ");
                dateInput = Console.ReadLine();
            }

            Console.Write("Enter Berth Preference (Lower/Middle/Upper): ");
            string berth = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(berth))
            {
                Console.Write("Berth preference cannot be empty. Enter again: ");
                berth = Console.ReadLine();
            }

            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("sp_BookTickets", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;

                DBConfig.Command.Parameters.AddWithValue("@userid", userid);
                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                DBConfig.Command.Parameters.AddWithValue("@class_name", selectedClass);
                DBConfig.Command.Parameters.AddWithValue("@seats_to_book", seatsToBook);
                DBConfig.Command.Parameters.AddWithValue("@travel_date", travelDate);
                DBConfig.Command.Parameters.AddWithValue("@berth_allotment", berth);

                SqlParameter outputParam = new SqlParameter("@booking_id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                DBConfig.Command.Parameters.Add(outputParam);
                SqlParameter amountParam = new SqlParameter("@total_amount_bill", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                DBConfig.Command.Parameters.Add(amountParam);


                DBConfig.Command.ExecuteNonQuery();
                int bookingId = (int)outputParam.Value;
                decimal totalAmount = (decimal)amountParam.Value;


                Console.WriteLine();
                Console.WriteLine("Booking successful!");
                Console.WriteLine("=====================================");
                Console.WriteLine("         BOOKING CONFIRMATION        ");
                Console.WriteLine("=====================================");
                Console.WriteLine($"Booking ID     : {bookingId}");
                Console.WriteLine($"User ID        : {userid}");
                Console.WriteLine($"Train Number   : {trainNumber}");
                Console.WriteLine($"Class          : {selectedClass}");
                Console.WriteLine($"Berth Pref.    : {berth}");
                Console.WriteLine($"Seats Booked   : {seatsToBook}");
                Console.WriteLine($"Travel Date    : {travelDate:yyyy-MM-dd}");
                Console.WriteLine($"Booking Date   : {DateTime.Now:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"Amount         : {totalAmount}");
                Console.WriteLine("=====================================");

                DBConfig.CloseConnection();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseConnection();
                Console.WriteLine($"Booking failed: {ex.Message}");
            }
        }
        internal static void CancelTickets()
        {
            Console.Write("Enter Booking ID: ");
            string bookingInput = Console.ReadLine();
            int bookingId;

            while (!int.TryParse(bookingInput, out bookingId))
            {
                Console.Write("Invalid input. Please enter a valid Booking ID: ");
                bookingInput = Console.ReadLine();
            }

            Console.Write("Enter number of seats to cancel: ");
            string seatsInput = Console.ReadLine();
            int seatsToCancel;

            while (!int.TryParse(seatsInput, out seatsToCancel) || seatsToCancel <= 0)
            {
                Console.Write("Invalid input. Please enter a valid number of seats to cancel: ");
                seatsInput = Console.ReadLine();
            }

            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("sp_CancelTickets", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;

                DBConfig.Command.Parameters.AddWithValue("@booking_id", bookingId);
                DBConfig.Command.Parameters.AddWithValue("@seats_to_cancel", seatsToCancel);

                SqlParameter refundAmountParam = new SqlParameter("@refund_amount", SqlDbType.Decimal)
                {
                    Precision = 10,
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };
                DBConfig.Command.Parameters.Add(refundAmountParam);

                SqlParameter refundReasonParam = new SqlParameter("@refund_reason", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                DBConfig.Command.Parameters.Add(refundReasonParam);

                DBConfig.Command.ExecuteNonQuery();

                decimal refundAmount = (decimal)refundAmountParam.Value;
                string refundReason = refundReasonParam.Value.ToString();

                Console.WriteLine($"Refund Reason : {refundReason}");
                Console.WriteLine($"Cancellation successful. Refund Amount: Rupees {refundAmount}");

                DBConfig.CloseConnection();
            }
            catch (SqlException ex)
            {
                DBConfig.CloseConnection();
                Console.WriteLine($"Cancellation failed: {ex.Message}");
            }
        }
        internal static void ShowConfirmedBookings(int userId)
        {
            try
            {
                DBConfig.OpenConnection();
                DBConfig.Command = new SqlCommand("sp_GetConfirmedBookings", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;
                DBConfig.Command.Parameters.AddWithValue("@userid", userId);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n--- Your Confirmed Bookings ---");
                bool hasBookings = false;

                while (DBConfig.Reader.Read())
                {
                    int seatsBooked = Convert.ToInt32(DBConfig.Reader["seats_booked"]);
                    if (seatsBooked > 0)
                    {
                        hasBookings = true;
                        Console.WriteLine($"Booking ID: {DBConfig.Reader["booking_id"]}, Train No: {DBConfig.Reader["tno"]}, Class: {DBConfig.Reader["class_name"]}, Seats: {seatsBooked}, Travel Date: {DBConfig.Reader["travel_date"]}");
                    }
                }

                if (!hasBookings)
                {
                    Console.WriteLine("You have no confirmed bookings.");
                }

                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine("An error occurred while retrieving your bookings.");
            }
        }
        


        //for testing purpose
        internal static bool TestCancelTickets(int bookingId, int seatsToCancel, out decimal refundAmount, out string refundReason)
        {
            refundAmount = 0;
            refundReason = "";

            try
            {
                DBConfig.OpenConnection();
                SqlTransaction transaction = DBConfig.Connection.BeginTransaction();

                DBConfig.Command = new SqlCommand("sp_CancelTickets", DBConfig.Connection, transaction);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;

                DBConfig.Command.Parameters.AddWithValue("@booking_id", bookingId);
                DBConfig.Command.Parameters.AddWithValue("@seats_to_cancel", seatsToCancel);

                SqlParameter refundAmountParam = new SqlParameter("@refund_amount", SqlDbType.Decimal)
                {
                    Precision = 10,
                    Scale = 2,
                    Direction = ParameterDirection.Output
                };
                DBConfig.Command.Parameters.Add(refundAmountParam);

                SqlParameter refundReasonParam = new SqlParameter("@refund_reason", SqlDbType.VarChar, 100)
                {
                    Direction = ParameterDirection.Output
                };
                DBConfig.Command.Parameters.Add(refundReasonParam);

                DBConfig.Command.ExecuteNonQuery();

                refundAmount = (decimal)refundAmountParam.Value;
                refundReason = refundReasonParam.Value.ToString();

                transaction.Rollback(); // Prevent actual cancellation
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

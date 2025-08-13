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

            List<int> availableTrainNumbers = TravelBackend.GetAvailableTrainNumbers(source, destination);
            while (!availableTrainNumbers.Contains(trainNumber))
            {
                Console.Write("Train number not in the list of available trains. Enter a valid Train Number: ");
                trainInput = Console.ReadLine();
                while (!int.TryParse(trainInput, out trainNumber))
                {
                    Console.Write("Invalid input. Enter a valid Train Number: ");
                    trainInput = Console.ReadLine();
                }
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

            // Temporarily store passenger details
            List<(string name, int age)> passengers = new List<(string, int)>();
            for (int i = 1; i <= seatsToBook; i++)
            {
                Console.Write($"Enter name for Passenger {i}: ");
                string passengerName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(passengerName))
                {
                    Console.Write("Name cannot be empty. Enter again: ");
                    passengerName = Console.ReadLine();
                }

                Console.Write($"Enter age for Passenger {i}: ");
                string ageInput = Console.ReadLine();
                int passengerAge;
                while (!int.TryParse(ageInput, out passengerAge) || passengerAge <= 0)
                {
                    Console.Write("Invalid age. Enter a valid number: ");
                    ageInput = Console.ReadLine();
                }

                passengers.Add((passengerName, passengerAge));
            }

            Console.Write("Enter Travel Date (yyyy-mm-dd): ");
            string dateInput = Console.ReadLine();
            DateTime travelDate;
            DateTime today = DateTime.Today;
            DateTime maxDate = today.AddDays(180);

            while (!DateTime.TryParse(dateInput, out travelDate) || travelDate.Date < today || travelDate.Date > maxDate)
            {
                Console.WriteLine($"Invalid date. Please enter a date between {today:yyyy-MM-dd} and {maxDate:yyyy-MM-dd}: ");
                dateInput = Console.ReadLine();
            }


            Console.Write("Enter Berth Preference (Lower/Middle/Upper): ");
            string berth = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(berth))
            {
                Console.Write("Berth preference cannot be empty. Enter again: ");
                berth = Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine("Please review your booking details:");
            Console.WriteLine($"Train Number   : {trainNumber}");
            Console.WriteLine($"Class          : {selectedClass}");
            Console.WriteLine($"Berth Pref.    : {berth}");
            Console.WriteLine($"Seats Booked   : {seatsToBook}");
            Console.WriteLine($"Travel Date    : {travelDate:yyyy-MM-dd}");
            Console.WriteLine("Passenger Details:");
            foreach (var passenger in passengers)
            {
                Console.WriteLine($" - {passenger.name}, Age: {passenger.age}");
            }
            Console.WriteLine();
            Console.Write("Do you want to confirm the booking? (yes/no): ");
            string confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation != "yes")
            {
                Console.WriteLine("Booking cancelled by user.");
                return;
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

                // Insert passengers now that bookingId is available
                foreach (var passenger in passengers)
                {
                    DBConfig.Command = new SqlCommand(
                        "INSERT INTO PassengerDetails (booking_id, passenger_name, passenger_age, status) VALUES (@booking_id, @name, @age, 'booked')",
                        DBConfig.Connection);
                    DBConfig.Command.Parameters.AddWithValue("@booking_id", bookingId);
                    DBConfig.Command.Parameters.AddWithValue("@name", passenger.name);
                    DBConfig.Command.Parameters.AddWithValue("@age", passenger.age);
                    DBConfig.Command.ExecuteNonQuery();
                }

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
                Console.WriteLine($"Amount         : {totalAmount}/-");
                Console.WriteLine("=====================================");
                Console.WriteLine("A mail will be sent to you shortly");
                Mailing.SendSystemMailToUser(userid, $"Your booking (ID: {bookingId}) for Train {trainNumber} on {travelDate:yyyy-MM-dd} is confirmed. Total amount: {totalAmount} rupees.");
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

            try
            {
                DBConfig.OpenConnection();

                // Fetch passengers for the booking
                DBConfig.Command = new SqlCommand(
                    "SELECT passenger_name, passenger_age FROM PassengerDetails WHERE booking_id = @booking_id AND status = 'booked'",
                    DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@booking_id", bookingId);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                List<(int index, string name)> availablePassengers = new List<(int, string)>();
                int counter = 1;
                Console.WriteLine("\nPassengers under this booking:");
                while (DBConfig.Reader.Read())
                {
                    string name = DBConfig.Reader["passenger_name"].ToString();
                    int age = Convert.ToInt32(DBConfig.Reader["passenger_age"]);
                    availablePassengers.Add((counter, name));
                    Console.WriteLine($" {counter}. {name}, Age: {age}");
                    counter++;
                }
                DBConfig.CloseReader();

                if (availablePassengers.Count == 0)
                {
                    Console.WriteLine("No active passengers found for this booking.");
                    DBConfig.CloseConnection();
                    return;
                }

                Console.Write("Enter number of seats to cancel: ");
                string seatsInput = Console.ReadLine();
                int seatsToCancel;

                while (!int.TryParse(seatsInput, out seatsToCancel) || seatsToCancel <= 0 || seatsToCancel > availablePassengers.Count)
                {
                    Console.Write($"Invalid input. Enter a number between 1 and {availablePassengers.Count}: ");
                    seatsInput = Console.ReadLine();
                }

                Console.WriteLine("Enter the passenger numbers to cancel (separated by commas): ");
                string selectionInput = Console.ReadLine();
                var selectedIndexes = selectionInput.Split(',')
                    .Select(s => s.Trim())
                    .Where(s => int.TryParse(s, out int n) && n >= 1 && n <= availablePassengers.Count)
                    .Select(int.Parse)
                    .Distinct()
                    .Take(seatsToCancel)
                    .ToList();

                if (selectedIndexes.Count != seatsToCancel)
                {
                    Console.WriteLine("Mismatch in number of selected passengers. Cancellation aborted.");
                    DBConfig.CloseConnection();
                    return;
                }

                List<string> passengersToCancel = selectedIndexes
                    .Select(i => availablePassengers.First(p => p.index == i).name)
                    .ToList();
                Console.WriteLine();
                Console.WriteLine("Please review the cancellation details:");
                Console.WriteLine($"Booking ID       : {bookingId}");
                Console.WriteLine($"Seats to Cancel  : {seatsToCancel}");
                Console.WriteLine("Passengers to Cancel:");
                foreach (var name in passengersToCancel)
                {
                    Console.WriteLine($" - {name}");
                }
                Console.WriteLine();
                Console.Write("Do you want to confirm the cancellation? (yes/no): ");
                string confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation != "yes")
                {
                    Console.WriteLine("Cancellation aborted by user.");
                    DBConfig.CloseConnection();
                    return;
                }

                // Proceed with cancellation
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

                // Update passenger status to 'cancelled'
                foreach (var name in passengersToCancel)
                {
                    DBConfig.Command = new SqlCommand(
                        "UPDATE PassengerDetails SET status = 'cancelled' WHERE booking_id = @booking_id AND passenger_name = @name",
                        DBConfig.Connection);
                    DBConfig.Command.Parameters.AddWithValue("@booking_id", bookingId);
                    DBConfig.Command.Parameters.AddWithValue("@name", name);
                    DBConfig.Command.ExecuteNonQuery();
                }

                // Print cancellation report
                Console.WriteLine();
                Console.WriteLine("=====================================");
                Console.WriteLine("         CANCELLATION REPORT         ");
                Console.WriteLine("=====================================");
                Console.WriteLine($"Booking ID       : {bookingId}");
                Console.WriteLine($"Seats Cancelled  : {seatsToCancel}");
                Console.WriteLine($"Refund Amount    : {refundAmount}/-");
                Console.WriteLine($"Refund Reason    : {refundReason}");
                Console.WriteLine("Cancelled Passengers:");
                foreach (var name in passengersToCancel)
                {
                    Console.WriteLine($" - {name}");
                }
                Console.WriteLine("=====================================");
                Console.WriteLine("A mail will be sent to you shortly");
                Mailing.SendSystemMailToUser(User.LoggedInUserId, $"Your cancellation for Booking ID {bookingId} is successful. Refund: {refundAmount} rupees. Reason: {refundReason}.");
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
                        if (!hasBookings)
                        {
                            Console.WriteLine("You have no confirmed bookings.");
                        }
                        else
                        {
                            Console.WriteLine("\n{0,-12} {1,-10} {2,-10} {3,-6} {4,-12} {5,-15} {6,-5}",
                                "Booking ID", "Train No", "Class", "Seats", "Travel Date", "Passenger Name", "Age");
                            Console.WriteLine(new string('-', 75));
                            DBConfig.Reader.Close(); // Close previous reader

                            // Re-execute to re-read for tabular display
                            DBConfig.Command = new SqlCommand("sp_GetConfirmedBookings", DBConfig.Connection);
                            DBConfig.Command.CommandType = CommandType.StoredProcedure;
                            DBConfig.Command.Parameters.AddWithValue("@userid", userId);
                            DBConfig.Reader = DBConfig.Command.ExecuteReader();

                            while (DBConfig.Reader.Read())
                            {
                                
                                if (seatsBooked > 0)
                                {
                                    Console.WriteLine("{0,-12} {1,-10} {2,-10} {3,-6} {4,-12:yyyy-MM-dd} {5,-15} {6,-5}",
                                        DBConfig.Reader["booking_id"],
                                        DBConfig.Reader["tno"],
                                        DBConfig.Reader["class_name"],
                                        seatsBooked,
                                        Convert.ToDateTime(DBConfig.Reader["travel_date"]),
                                        DBConfig.Reader["passenger_name"],
                                        DBConfig.Reader["passenger_age"]);
                                }
                            }
                        }

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

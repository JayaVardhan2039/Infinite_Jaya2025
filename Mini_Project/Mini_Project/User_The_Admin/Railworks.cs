using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Admin
{
    //All User Main Logics like Adding and updating Train
    class Railworks
    {
        internal static void AddTrain()
        {
            Console.WriteLine("Already Available Trains");
            DisplayAllTrainsForAdmin();

            Console.Write("Enter Train Number: ");
            int trainNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Train Name: ");
            string trainName = Console.ReadLine();
            Console.Write("Enter From Station: ");
            string from = Console.ReadLine();
            Console.Write("Enter Destination Station: ");
            string dest = Console.ReadLine();
            Console.Write("Enter Price Range: ");
            string price = Console.ReadLine();
            Console.Write("Enter Train Status (active/inactive): ");
            string trainStatus = Console.ReadLine();

            Console.Write("Enter number of classes to add: ");
            int classCount = Convert.ToInt32(Console.ReadLine());

            int totalSeats = 0;
            List<string> classNames = new List<string>();
            List<(string className, int seats, decimal price)> classDetails = new List<(string, int, decimal)>();

            for (int i = 0; i < classCount; i++)
            {
                Console.WriteLine($"\nEnter details for Class {i + 1}:");
                Console.Write("Class Name (Sleeper/1AC/2AC/3AC/General): ");
                string className = Console.ReadLine();
                Console.Write("Seats Available in this class: ");
                int classSeats = Convert.ToInt32(Console.ReadLine());
                Console.Write("Price per Seat in this class: ");
                decimal classPrice = decimal.Parse(Console.ReadLine());

                classDetails.Add((className, classSeats, classPrice));
                totalSeats += classSeats;
                classNames.Add(className);
            }

            string classesOfTravel = string.Join(",", classNames);

            Console.Write("Enter Number of Seats Available (total): ");
            int seatsAvailable = Convert.ToInt32(Console.ReadLine());

            try
            {
                DBConfig.OpenConnection();

                // Insert train first
                DBConfig.Command = new SqlCommand(
                    "INSERT INTO Trains (tno, tname, [from], dest, price, classes_of_travel, train_status, seats_available, total_seats) " +
                    "VALUES (@tno, @tname, @from, @dest, @price, @classes_of_travel, @train_status, @seats_available, @total_seats)",
                    DBConfig.Connection);

                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                DBConfig.Command.Parameters.AddWithValue("@tname", trainName);
                DBConfig.Command.Parameters.AddWithValue("@from", from);
                DBConfig.Command.Parameters.AddWithValue("@dest", dest);
                DBConfig.Command.Parameters.AddWithValue("@price", price);
                DBConfig.Command.Parameters.AddWithValue("@classes_of_travel", classesOfTravel);
                DBConfig.Command.Parameters.AddWithValue("@train_status", trainStatus);
                DBConfig.Command.Parameters.AddWithValue("@seats_available", seatsAvailable);
                DBConfig.Command.Parameters.AddWithValue("@total_seats", totalSeats);

                DBConfig.Command.ExecuteNonQuery();

                // Then insert classes
                foreach (var detail in classDetails)
                {
                    DBConfig.Command = new SqlCommand(
                        "INSERT INTO TrainClasses (tno, class_name, seats_available, price) " +
                        "VALUES (@tno, @class_name, @seats, @price)",
                        DBConfig.Connection);

                    DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                    DBConfig.Command.Parameters.AddWithValue("@class_name", detail.className);
                    DBConfig.Command.Parameters.AddWithValue("@seats", detail.seats);
                    DBConfig.Command.Parameters.AddWithValue("@price", detail.price);

                    DBConfig.Command.ExecuteNonQuery();
                }

                DBConfig.CloseConnection();
                Console.WriteLine("Train and its classes added successfully.");
            }
            catch (SqlException ex)
            {
                DBConfig.CloseConnection();
                Console.WriteLine($"Error adding train: {ex.Message}");
            }
        }
        internal static void UpdateTrain()
        {
            DisplayAllTrainsForAdmin();
            Console.Write("Enter Train Number to update: ");
            int trainNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("What would you like to update?");
            Console.WriteLine("1. Train Name");
            Console.WriteLine("2. From Station");
            Console.WriteLine("3. Destination Station");
            Console.WriteLine("4. Prices");
            Console.WriteLine("5. Classes of Travel");
            Console.WriteLine("6. Train Status Activation");
            Console.WriteLine("7. Total Seats Available");
            Console.WriteLine("8. Train Classes (Add/Update/Remove)");
            Console.Write("Enter your choice (1-8): ");
            string input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            if (choice >= 1 && choice <= 7)
            {
                string column = "";
                object newValue;

                switch (choice)
                {
                    case 1:
                        column = "tname";
                        Console.Write("Enter new Train Name: ");
                        newValue = Console.ReadLine();
                        break;
                    case 2:
                        column = "[from]";
                        Console.Write("Enter new From Station: ");
                        newValue = Console.ReadLine();
                        break;
                    case 3:
                        column = "dest";
                        Console.Write("Enter new Destination Station: ");
                        newValue = Console.ReadLine();
                        break;
                    case 4:
                        {
                            DBConfig.OpenConnection();
                            DBConfig.Command = new SqlCommand("SELECT price FROM Trains WHERE tno = @tno", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            string oldPriceString = DBConfig.Command.ExecuteScalar()?.ToString();
                            DBConfig.CloseConnection();

                            if (string.IsNullOrEmpty(oldPriceString))
                            {
                                Console.WriteLine("No prices found for this train.");
                                return;
                            }

                            var oldPrices = oldPriceString.Split(',').Select(p => p.Trim()).ToList();
                            var newPrices = new List<string>();

                            for (int i = 0; i < oldPrices.Count; i++)
                            {
                                Console.Write($"Enter new price for class {i + 1} (old: {oldPrices[i]}): ");
                                string newPrice = Console.ReadLine();
                                newPrices.Add(newPrice);

                                // Update TrainClasses table
                                DBConfig.OpenConnection();
                                DBConfig.Command = new SqlCommand("UPDATE TrainClasses SET price = @newPrice WHERE tno = @tno AND price = @oldPrice", DBConfig.Connection);
                                DBConfig.Command.Parameters.AddWithValue("@newPrice", Convert.ToDecimal(newPrice));
                                DBConfig.Command.Parameters.AddWithValue("@oldPrice", Convert.ToDecimal(oldPrices[i]));
                                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                                DBConfig.Command.ExecuteNonQuery();
                                DBConfig.CloseConnection();
                            }

                            string updatedPriceString = string.Join(",", newPrices);
                            DBConfig.OpenConnection();
                            DBConfig.Command = new SqlCommand("UPDATE Trains SET price = @price WHERE tno = @tno", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@price", updatedPriceString);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.ExecuteNonQuery();
                            DBConfig.CloseConnection();

                            column = "price";
                            newValue = string.Join(",", newPrices);
                            Console.WriteLine("Prices updated successfully.");
                           
                            break;
                        }

                    case 5:
                        {
                            DBConfig.OpenConnection();
                            DBConfig.Command = new SqlCommand("SELECT classes_of_travel FROM Trains WHERE tno = @tno", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            string oldClassesString = DBConfig.Command.ExecuteScalar()?.ToString();
                            DBConfig.CloseConnection();

                            if (string.IsNullOrEmpty(oldClassesString))
                            {
                                Console.WriteLine("No classes found for this train.");
                                return;
                            }

                            var oldClasses = oldClassesString.Split(',').Select(c => c.Trim()).ToList();
                            var newClasses = new List<string>();

                            for (int i = 0; i < oldClasses.Count; i++)
                            {
                                string oldClass = oldClasses[i];

                                // Check for active bookings
                                DBConfig.OpenConnection();
                                DBConfig.Command = new SqlCommand(
                                    "SELECT COUNT(*) FROM PassengerDetails pd " +
                                    "JOIN Bookings b ON pd.booking_id = b.booking_id " +
                                    "WHERE b.tno = @tno AND pd.status = 'booked' AND b.deleted = 0 AND b.class_name = @class",
                                    DBConfig.Connection);
                                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                                DBConfig.Command.Parameters.AddWithValue("@class", oldClass);
                                int activeBookings = (int)DBConfig.Command.ExecuteScalar();
                                DBConfig.CloseConnection();

                                if (activeBookings > 0)
                                {
                                    Console.WriteLine($"Cannot update class '{oldClass}' because there are {activeBookings} active bookings.");
                                    newClasses.Add(oldClass); // retain old class name
                                    continue;
                                }

                                Console.Write($"Enter new class name for class {i + 1} (old: {oldClass}): ");
                                string newClass = Console.ReadLine();
                                newClasses.Add(newClass);

                                // Update TrainClasses table
                                DBConfig.OpenConnection();
                                DBConfig.Command = new SqlCommand("UPDATE TrainClasses SET class_name = @newClass WHERE tno = @tno AND class_name = @oldClass", DBConfig.Connection);
                                DBConfig.Command.Parameters.AddWithValue("@newClass", newClass);
                                DBConfig.Command.Parameters.AddWithValue("@oldClass", oldClass);
                                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                                DBConfig.Command.ExecuteNonQuery();
                                DBConfig.CloseConnection();
                            }

                            string updatedClassesString = string.Join(",", newClasses);
                            DBConfig.OpenConnection();
                            DBConfig.Command = new SqlCommand("UPDATE Trains SET classes_of_travel = @classes WHERE tno = @tno", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@classes", updatedClassesString);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.ExecuteNonQuery();
                            DBConfig.CloseConnection();


                            column = "classes_of_travel";
                            newValue= string.Join(",", newClasses);

                            Console.WriteLine("Classes of travel updated successfully.");
                            break;
                        }

                    case 6:
                        column = "train_status";
                        Console.Write("Enter new Train Status (only 'active' allowed): ");
                        newValue = Console.ReadLine().ToLower();

                        if (newValue.ToString() != "active")
                        {
                            Console.WriteLine("You can only change status from 'inactive' to 'active'.");
                            return;
                        }

                        try
                        {
                            DBConfig.OpenConnection();

                            DBConfig.Command = new SqlCommand("SELECT train_status FROM Trains WHERE tno = @tno", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);

                            string currentStatus = (string)DBConfig.Command.ExecuteScalar();

                            if (currentStatus.ToLower() == "active")
                            {
                                Console.WriteLine("Train is already active. No update needed.");
                                DBConfig.CloseConnection();
                                return;
                            }

                            DBConfig.Command = new SqlCommand("UPDATE Trains SET train_status = 'active' WHERE tno = @tno", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.ExecuteNonQuery();

                            Console.WriteLine("Train status updated to active.");
                            DBConfig.CloseConnection();
                        }
                        catch (SqlException ex)
                        {
                            DBConfig.CloseConnection();
                            Console.WriteLine($"Error checking train status: {ex.Message}");
                        }
                        break;

                    case 7:
                        column = "seats_available";
                        Console.Write("Enter new Number of Seats Available: ");
                        newValue = Convert.ToInt32(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        return;
                }

                try
                {
                    DBConfig.OpenConnection();

                    string query = $"UPDATE Trains SET {column} = @newValue WHERE tno = @tno";
                    DBConfig.Command = new SqlCommand(query, DBConfig.Connection);
                    DBConfig.Command.Parameters.AddWithValue("@newValue", newValue);
                    DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);

                    int rowsAffected = DBConfig.Command.ExecuteNonQuery();

                    Console.WriteLine(rowsAffected > 0 ? "Train updated successfully." : "Train update failed.");

                    DBConfig.CloseConnection();
                }
                catch (SqlException ex)
                {
                    DBConfig.CloseConnection();
                    Console.WriteLine($"Error updating train: {ex.Message}");
                }
            }
            else if (choice == 8)
            {
                Console.WriteLine("1. Update Class");
                Console.WriteLine("2. Remove Class");
                Console.WriteLine("3. Add New Class");
                Console.Write("Enter your choice: ");
                int action = Convert.ToInt32(Console.ReadLine());


                try
                {
                    DBConfig.OpenConnection();

                    if (action == 1)
                    {
                        Console.Write("Enter Class Name to Update: ");
                        string className = Console.ReadLine();
                        // Check if class has active bookings
                        DBConfig.Command = new SqlCommand(
                            "SELECT COUNT(*) FROM PassengerDetails pd " +
                            "JOIN Bookings b ON pd.booking_id = b.booking_id " +
                            "WHERE b.tno = @tno AND pd.status = 'booked' AND b.deleted = 0 AND b.class_name = @class",
                            DBConfig.Connection);
                        DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                        DBConfig.Command.Parameters.AddWithValue("@class", className);

                        int activePassengers = (int)DBConfig.Command.ExecuteScalar();
                        if (activePassengers > 0)
                        {
                            Console.WriteLine($"Cannot update/remove class '{className}' because there are {activePassengers} active bookings.");
                            DBConfig.CloseConnection();
                            return;
                        }

                        Console.Write("Enter New Seats Available: ");
                        int newSeats = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter New Price: ");
                        decimal newPrice = decimal.Parse(Console.ReadLine());

                        try
                        {
                            DBConfig.OpenConnection();

                            // Step 1: Get old seat count from TrainClasses
                            DBConfig.Command = new SqlCommand("SELECT seats_available FROM TrainClasses WHERE tno = @tno AND class_name = @class", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.Parameters.AddWithValue("@class", className);
                            object oldSeatsObj = DBConfig.Command.ExecuteScalar();

                            if (oldSeatsObj == null)
                            {
                                Console.WriteLine("Train class not found.");
                                DBConfig.CloseConnection();
                                return;
                            }

                            int oldSeats = Convert.ToInt32(oldSeatsObj);
                            int seatDifference = newSeats - oldSeats;

                            // Step 2: Update TrainClasses table
                            DBConfig.Command = new SqlCommand("UPDATE TrainClasses SET seats_available = @seats, price = @price WHERE tno = @tno AND class_name = @class", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.Parameters.AddWithValue("@class", className);
                            DBConfig.Command.Parameters.AddWithValue("@seats", newSeats);
                            DBConfig.Command.Parameters.AddWithValue("@price", newPrice);
                            int rows = DBConfig.Command.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                // Step 3: Update price in Trains table
                                DBConfig.Command = new SqlCommand("SELECT classes_of_travel, price FROM Trains WHERE tno = @tno", DBConfig.Connection);
                                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                                if (DBConfig.Reader.Read())
                                {
                                    string classes = DBConfig.Reader.GetString(0);
                                    string prices = DBConfig.Reader.GetString(1);
                                    DBConfig.CloseReader();

                                    List<string> classList = classes.Split(',').ToList();
                                    List<string> priceList = prices.Split(',').ToList();

                                    int index = classList.IndexOf(className);
                                    if (index != -1 && index < priceList.Count)
                                    {
                                        priceList[index] = newPrice.ToString();
                                        string updatedPrices = string.Join(",", priceList);

                                        // Step 4: Update Trains table
                                        DBConfig.Command = new SqlCommand(
                                            "UPDATE Trains SET price = @price, seats_available = seats_available + @diff, total_seats = total_seats + @diff WHERE tno = @tno",
                                            DBConfig.Connection);
                                        DBConfig.Command.Parameters.AddWithValue("@price", updatedPrices);
                                        DBConfig.Command.Parameters.AddWithValue("@diff", seatDifference);
                                        DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                                        DBConfig.Command.ExecuteNonQuery();

                                        Console.WriteLine("Train class updated successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Class not found in train record.");
                                    }
                                }
                                else
                                {
                                    DBConfig.CloseReader();
                                    Console.WriteLine("Train not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Train class not found.");
                            }

                            DBConfig.CloseConnection();
                        }
                        catch (SqlException ex)
                        {
                            DBConfig.CloseReader();
                            DBConfig.CloseConnection();
                            Console.WriteLine($"Error updating train class: {ex.Message}");
                        }
                    }

                    else if (action == 2)
                    {
                        Console.Write("Enter Class Name to Remove: ");
                        string className = Console.ReadLine();
                        // Check if class has active bookings
                        DBConfig.Command = new SqlCommand(
                            "SELECT COUNT(*) FROM PassengerDetails pd " +
                            "JOIN Bookings b ON pd.booking_id = b.booking_id " +
                            "WHERE b.tno = @tno AND pd.status = 'booked' AND b.deleted = 0 AND b.class_name = @class",
                            DBConfig.Connection);
                        DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                        DBConfig.Command.Parameters.AddWithValue("@class", className);

                        int activePassengers = (int)DBConfig.Command.ExecuteScalar();
                        if (activePassengers > 0)
                        {
                            Console.WriteLine($"Cannot update/remove class '{className}' because there are {activePassengers} active bookings.");
                            DBConfig.CloseConnection();
                            return;
                        }

                        try
                        {
                            DBConfig.OpenConnection();

                            // Step 1: Get the number of seats for the class
                            DBConfig.Command = new SqlCommand("SELECT seats_available FROM TrainClasses WHERE tno = @tno AND class_name = @class", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.Parameters.AddWithValue("@class", className);

                            object seatsObj = DBConfig.Command.ExecuteScalar();
                            if (seatsObj == null)
                            {
                                Console.WriteLine("Train class not found.");
                                DBConfig.CloseConnection();
                                return;
                            }

                            int seatsToRemove = Convert.ToInt32(seatsObj);

                            // Step 2: Remove the class from TrainClasses
                            DBConfig.Command = new SqlCommand("DELETE FROM TrainClasses WHERE tno = @tno AND class_name = @class", DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.Parameters.AddWithValue("@class", className);
                            int rows = DBConfig.Command.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                // Step 3: Get current classes and prices
                                DBConfig.Command = new SqlCommand("SELECT classes_of_travel, price FROM Trains WHERE tno = @tno", DBConfig.Connection);
                                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                                if (DBConfig.Reader.Read())
                                {
                                    string classes = DBConfig.Reader.GetString(0);
                                    string prices = DBConfig.Reader.GetString(1);
                                    DBConfig.CloseReader();

                                    List<string> classList = classes.Split(',').ToList();
                                    List<string> priceList = prices.Split(',').ToList();

                                    int index = classList.IndexOf(className);
                                    if (index != -1)
                                    {
                                        classList.RemoveAt(index);
                                        priceList.RemoveAt(index);

                                        string updatedClasses = string.Join(",", classList);
                                        string updatedPrices = string.Join(",", priceList);

                                        // Step 4: Update Trains table
                                        DBConfig.Command = new SqlCommand(
                                            "UPDATE Trains SET classes_of_travel = @classes, price = @price, seats_available = seats_available - @seats, total_seats = total_seats - @seats WHERE tno = @tno",
                                            DBConfig.Connection);
                                        DBConfig.Command.Parameters.AddWithValue("@classes", updatedClasses);
                                        DBConfig.Command.Parameters.AddWithValue("@price", updatedPrices);
                                        DBConfig.Command.Parameters.AddWithValue("@seats", seatsToRemove);
                                        DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                                        DBConfig.Command.ExecuteNonQuery();

                                        Console.WriteLine("Train class and price removed successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Class not found in train record.");
                                    }
                                }
                                else
                                {
                                    DBConfig.CloseReader();
                                    Console.WriteLine("Train not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Train class not found.");
                            }

                            DBConfig.CloseConnection();
                        }
                        catch (SqlException ex)
                        {
                            DBConfig.CloseReader();
                            DBConfig.CloseConnection();
                            Console.WriteLine($"Error removing train class: {ex.Message}");
                        }
                    }

                    else if (action == 3)
                    {
                        Console.Write("Enter New Class Name: ");
                        string newClassName = Console.ReadLine();

                        Console.Write("Enter Seats Available: ");
                        int newSeats = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Price per Seat: ");
                        decimal newPrice = decimal.Parse(Console.ReadLine());

                        try
                        {
                            DBConfig.OpenConnection();

                            // Step 1: Insert new class into TrainClasses
                            DBConfig.Command = new SqlCommand(
                                "INSERT INTO TrainClasses (tno, class_name, seats_available, price) VALUES (@tno, @class, @seats, @price)",
                                DBConfig.Connection);
                            DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                            DBConfig.Command.Parameters.AddWithValue("@class", newClassName);
                            DBConfig.Command.Parameters.AddWithValue("@seats", newSeats);
                            DBConfig.Command.Parameters.AddWithValue("@price", newPrice);

                            int rows = DBConfig.Command.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                // Step 2: Fetch current train details
                                DBConfig.Command = new SqlCommand(
                                    "SELECT seats_available, total_seats, classes_of_travel, price FROM Trains WHERE tno = @tno",
                                    DBConfig.Connection);
                                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);

                                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                                int currentSeats = 0, currentTotalSeats = 0;
                                string currentClasses = "", currentPrice = "";

                                if (DBConfig.Reader.Read())
                                {
                                    currentSeats = Convert.ToInt32(DBConfig.Reader["seats_available"]);
                                    currentTotalSeats = Convert.ToInt32(DBConfig.Reader["total_seats"]);
                                    currentClasses = DBConfig.Reader["classes_of_travel"].ToString();
                                    currentPrice = DBConfig.Reader["price"].ToString();
                                }
                                DBConfig.CloseReader();

                                // Step 3: Update train record
                                int updatedSeats = currentSeats + newSeats;
                                int updatedTotalSeats = currentTotalSeats + newSeats;
                                string updatedClasses = string.IsNullOrEmpty(currentClasses) ? newClassName : currentClasses + "," + newClassName;
                                string updatedPrice = string.IsNullOrEmpty(currentPrice) ? newPrice.ToString("0.##") : currentPrice + "," + newPrice.ToString("0.##");

                                DBConfig.Command = new SqlCommand(
                                    "UPDATE Trains SET seats_available = @seats, total_seats = @total, classes_of_travel = @classes, price = @price WHERE tno = @tno",
                                    DBConfig.Connection);
                                DBConfig.Command.Parameters.AddWithValue("@seats", updatedSeats);
                                DBConfig.Command.Parameters.AddWithValue("@total", updatedTotalSeats);
                                DBConfig.Command.Parameters.AddWithValue("@classes", updatedClasses);
                                DBConfig.Command.Parameters.AddWithValue("@price", updatedPrice);
                                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);

                                DBConfig.Command.ExecuteNonQuery();

                                Console.WriteLine("New train class added and train details updated successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add class.");
                            }

                            DBConfig.CloseConnection();
                        }
                        catch (SqlException ex)
                        {
                            DBConfig.CloseReader();
                            DBConfig.CloseConnection();
                            Console.WriteLine($"Error adding new class: {ex.Message}");
                        }
                    }


                    DBConfig.CloseConnection();
                }
                catch (SqlException ex)
                {
                    DBConfig.CloseReader(); // In case any reader was opened
                    DBConfig.CloseConnection();
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
        internal static void DeleteTrain()
        {
            DisplayAllTrainsForAdmin();

            Console.Write("Enter Train Number to delete (inactivate): ");
            int trainNumber = Convert.ToInt32(Console.ReadLine());

            try
            {
                DBConfig.OpenConnection();

                // Check if train has active bookings
                DBConfig.Command = new SqlCommand("SELECT COUNT(*) FROM Bookings WHERE tno = @tno AND deleted = 0", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                int activeBookings = (int)DBConfig.Command.ExecuteScalar();

                if (activeBookings > 0)
                {
                    Console.WriteLine("Warning: This train has active bookings.");
                    Console.Write("Do you really want to delete this train and cancel all bookings? (yes/no im just kidding): ");
                    string confirmation = Console.ReadLine()?.Trim().ToLower();
                    if (confirmation != "yes")
                    {
                        Console.WriteLine("Train deletion cancelled.");
                        DBConfig.CloseConnection();
                        return;
                    }
                }

                // Step 1: Mark train as inactive
                DBConfig.Command = new SqlCommand("UPDATE Trains SET train_status = 'inactive' WHERE tno = @tno", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                DBConfig.Command.ExecuteNonQuery();

                // Step 2: Cancel bookings and refund
                DBConfig.Command = new SqlCommand("SELECT booking_id, userid, total_amount FROM Bookings WHERE tno = @tno AND deleted = 0", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                List<(int bookingId, int userId, decimal amount)> bookings = new List<(int, int, decimal)>();
                while (DBConfig.Reader.Read())
                {
                    bookings.Add((
                        Convert.ToInt32(DBConfig.Reader["booking_id"]),
                        Convert.ToInt32(DBConfig.Reader["userid"]),
                        Convert.ToDecimal(DBConfig.Reader["total_amount"])
                    ));
                }
                DBConfig.CloseReader();

                foreach (var booking in bookings)
                {
                    // Mark booking as deleted
                    DBConfig.Command = new SqlCommand("UPDATE Bookings SET deleted = 1 WHERE booking_id = @bid", DBConfig.Connection);
                    DBConfig.Command.Parameters.AddWithValue("@bid", booking.bookingId);
                    DBConfig.Command.ExecuteNonQuery();

                    // Insert cancellation record
                    DBConfig.Command = new SqlCommand(
                        "INSERT INTO Cancellations (booking_id, seats_cancelled, cancellation_date, refund_amount, refund_reason) " +
                        "SELECT booking_id, seats_booked, GETDATE(), total_amount, 'Train Inactivated - Full Refund' FROM Bookings WHERE booking_id = @bid",
                        DBConfig.Connection);
                    DBConfig.Command.Parameters.AddWithValue("@bid", booking.bookingId);
                    DBConfig.Command.ExecuteNonQuery();

                    // Send mail to user
                    string message = $"Your booking (ID: {booking.bookingId}) for train {trainNumber} has been cancelled due to train inactivation. Full refund of ₹{booking.amount} has been processed.";
                    DBConfig.Command = new SqlCommand(
                        "INSERT INTO Mails (sender_id, receiver_id, sender_role, receiver_role, message_text) " +
                        "VALUES (@sender, @uid, 'admin', 'user', @msg)", DBConfig.Connection);
                    DBConfig.Command.Parameters.AddWithValue("@sender", Admin.LoggedInAdminId);
                    DBConfig.Command.Parameters.AddWithValue("@uid", booking.userId);
                    DBConfig.Command.Parameters.AddWithValue("@msg", message);
                    DBConfig.Command.ExecuteNonQuery();
                }

                // Step 3: Restore seats in trains
                DBConfig.Command = new SqlCommand("UPDATE Trains SET seats_available = total_seats WHERE tno = @tno", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                DBConfig.Command.ExecuteNonQuery();

                // Step 4: Restore seats in trainclasses
                DBConfig.Command = new SqlCommand("UPDATE trainclasses SET seats_available = total_seats WHERE tno = @tno", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);
                DBConfig.Command.ExecuteNonQuery();

                DBConfig.CloseConnection();
                Console.WriteLine("Train marked as inactive, bookings cancelled, refunds processed, and seats restored.");
            }
            catch (SqlException ex)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        internal static void DisplayAllTrainsForAdmin()
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand(
                    "SELECT t.tno, t.tname, t.[from], t.dest, tc.class_name, tc.seats_available, tc.price, t.train_status " +
                    "FROM Trains t LEFT JOIN TrainClasses tc ON t.tno = tc.tno " +
                    "ORDER BY t.train_status DESC", DBConfig.Connection);

                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\n===============================");
                Console.WriteLine("     ALL TRAINS (ADMIN VIEW)   ");
                Console.WriteLine("===============================");

                bool printedActiveHeader = false;
                bool printedInactiveHeader = false;

                while (DBConfig.Reader.Read())
                {
                    string status = DBConfig.Reader["train_status"].ToString();

                    if (status == "active" && !printedActiveHeader)
                    {
                        Console.WriteLine();
                        Console.WriteLine("---ACTIVE TRAINS---");
                        Console.WriteLine("Train No | Name           | From       | To         | Class     | Status   | Seats | Price");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        printedActiveHeader = true;
                    }
                    else if (status == "inactive" && !printedInactiveHeader)
                    {
                        Console.WriteLine();
                        Console.WriteLine("---INACTIVE TRAINS---");
                        Console.WriteLine("Train No | Name           | From       | To         | Class     | Status   | Seats | Price");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        printedInactiveHeader = true;
                    }

                    Console.WriteLine($"{DBConfig.Reader["tno"],-9} | {DBConfig.Reader["tname"],-14} | {DBConfig.Reader["from"],-10} | {DBConfig.Reader["dest"],-10} | {DBConfig.Reader["class_name"],-9} | {status,-8} | {DBConfig.Reader["seats_available"],-5} | {DBConfig.Reader["price"],-5}");
                }

                Console.WriteLine("------------------------------------------------------------------------------------------");

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



        //for testing purpose
        internal static bool TestDeleteTrain(int trainNumber)
        {
            try
            {
                DBConfig.OpenConnection();
                SqlTransaction transaction = DBConfig.Connection.BeginTransaction();

                DBConfig.Command = new SqlCommand("DELETE FROM Trains WHERE tno = @tno", DBConfig.Connection, transaction);
                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);

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

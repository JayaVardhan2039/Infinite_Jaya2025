using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Mini_Project.User_The_Passenger
{
    //All user related retrieval methods
    static class TravelBackend
    {
        internal static List<string> GetDistinctSourceCities()
        {
            List<string> cities = new List<string>();

            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("sp_GetDistinctSourceCities", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;

                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                while (DBConfig.Reader.Read())
                {
                    cities.Add(DBConfig.Reader["SourceCity"].ToString());
                }

                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }

            return cities;
        }
        internal static List<string> GetDestinationCitiesForSource(string source)
        {
            List<string> cities = new List<string>();

            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("sp_GetDestinationCitiesForSource", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;
                DBConfig.Command.Parameters.AddWithValue("@source", source);

                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                while (DBConfig.Reader.Read())
                {
                    cities.Add(DBConfig.Reader["DestinationCity"].ToString());
                }

                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }

            return cities;
        }
        internal static string SelectCity(List<string> cities, string type)
        {
            Console.WriteLine();
            Console.WriteLine($"Select {type} City:");
            for (int i = 0; i < cities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cities[i]}");
            }
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice < 1 || choice > cities.Count)
            {
                Console.WriteLine("Invalid choice. Defaulting to first city.");
                return cities[0];
            }
            return cities[choice - 1];
        }

        internal static List<int> GetAvailableTrainNumbers(string from, string dest)
        {
            List<int> trainNumbers = new List<int>();

            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand(
                    "SELECT tno FROM trains WHERE [from] = @from AND dest = @dest AND train_status = 'active'",
                    DBConfig.Connection);

                DBConfig.Command.Parameters.AddWithValue("@from", from);
                DBConfig.Command.Parameters.AddWithValue("@dest", dest);

                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                while (DBConfig.Reader.Read())
                {
                    trainNumbers.Add(Convert.ToInt32(DBConfig.Reader["tno"]));
                }

                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine("An error occurred while retrieving available train numbers.");
            }

            return trainNumbers;
        }

        internal static void DisplayAvailableTrainsForUser(string source, string destination)
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand("sp_GetAvailableTrainsWithClasses", DBConfig.Connection);
                DBConfig.Command.CommandType = CommandType.StoredProcedure;
                DBConfig.Command.Parameters.AddWithValue("@source", source);
                DBConfig.Command.Parameters.AddWithValue("@destination", destination);

                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                if (!DBConfig.Reader.HasRows)
                {
                    Console.WriteLine("No trains available for the selected route.");
                    DBConfig.CloseReader();
                    DBConfig.CloseConnection();
                    return;
                }

                while (DBConfig.Reader.Read())
                {
                    Console.WriteLine($"Train No: {DBConfig.Reader["tno"]}, Name: {DBConfig.Reader["tname"]}, From: {DBConfig.Reader["from"]}, To: {DBConfig.Reader["dest"]}, Classes: {DBConfig.Reader["Classes"]}");
                }

                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine("An error occurred while fetching train data.");
            }
        }
        internal static void DisplayTrainClasses(int trainNumber)
        {
            try
            {
                DBConfig.OpenConnection();

                DBConfig.Command = new SqlCommand(
                    "SELECT class_name, seats_available, price FROM TrainClasses WHERE tno = @tno",
                    DBConfig.Connection);

                DBConfig.Command.Parameters.AddWithValue("@tno", trainNumber);

                DBConfig.Reader = DBConfig.Command.ExecuteReader();

                Console.WriteLine("\nAvailable Classes:");
                bool hasAvailableSeats = false;

                while (DBConfig.Reader.Read())
                {
                    int seats = Convert.ToInt32(DBConfig.Reader["seats_available"]);
                    string className = DBConfig.Reader["class_name"].ToString();
                    decimal price = Convert.ToDecimal(DBConfig.Reader["price"]);

                    if (seats > 0)
                    {
                        hasAvailableSeats = true;
                        Console.WriteLine($"Class: {className}, Seats: {seats}, Price: {price}");
                    }
                }

                if (!hasAvailableSeats)
                {
                    Console.WriteLine("No seats available at the moment.");
                }

                DBConfig.CloseReader();
                DBConfig.CloseConnection();
            }
            catch (SqlException)
            {
                DBConfig.CloseReader();
                DBConfig.CloseConnection();
                Console.WriteLine("An error occurred while retrieving train class information.");
            }
        }
        internal static bool HasUnreadMails(int userId)
        {
            try
            {
                DBConfig.OpenConnection();
                DBConfig.Command = new SqlCommand("SELECT COUNT(*) FROM mails WHERE receiver_id = @userId AND receiver_role = 'user' AND read_status = 0", DBConfig.Connection);
                DBConfig.Command.Parameters.AddWithValue("@userId", userId);
                int count = (int)DBConfig.Command.ExecuteScalar();
                DBConfig.CloseConnection();
                return count > 0;
            }
            catch
            {
                DBConfig.CloseConnection();
                return false;
            }
        }
        internal static void DisplayAllTrainsForUser()
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
                Console.WriteLine("     ALL TRAINS (USER VIEW)   ");
                Console.WriteLine("===============================");

                Console.WriteLine("---ACTIVE TRAINS---");

                while (DBConfig.Reader.Read())
                {
                    string status = DBConfig.Reader["train_status"].ToString();

                    
                        Console.WriteLine();
      
                        Console.WriteLine("Train No | Name           | From       | To         | Class     | Status   | Seats | Price");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                        
                    
                    //else if (status == "inactive" && !printedInactiveHeader)
                    //{
                    //    Console.WriteLine();
                    //    Console.WriteLine("---INACTIVE TRAINS---");
                    //    Console.WriteLine("Train No | Name           | From       | To         | Class     | Status   | Seats | Price");
                    //    Console.WriteLine("------------------------------------------------------------------------------------------");
                    //    printedInactiveHeader = true;
                    //}

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


    }
}

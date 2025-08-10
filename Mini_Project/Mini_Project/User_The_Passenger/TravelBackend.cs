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
                while (DBConfig.Reader.Read())
                {
                    Console.WriteLine($"Class: {DBConfig.Reader["class_name"]}, Seats: {DBConfig.Reader["seats_available"]}, Price: {DBConfig.Reader["price"]}");
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
    }
}

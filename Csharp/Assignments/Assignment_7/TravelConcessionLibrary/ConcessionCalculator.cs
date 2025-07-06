using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelConcessionLibrary
{
    public class ConcessionCalculator
    {
        public static string CalculateConcession(int Age, int TotalFare)
        {
            if (Age <= 5)
            {
                return "Little Champs - Free Ticket";
            }
            else if (Age > 60)
            {
                double ConcessionFare = TotalFare - (TotalFare * 0.3);
                return $"Senior Citizen - Fare after concession: {ConcessionFare}";
            }
            else
            {
                return $"Ticket Booked - Fare: {TotalFare}";
            }
        }
    }
}


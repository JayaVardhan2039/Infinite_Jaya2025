using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 1. Write a program to find the Sum and the Average points scored by the teams in the IPL. 
 * Create a Class called CricketTeam that has a function called Pointscalculation(int no_of_matches)
 * that takes no.of matches as input and accepts that many scores from the user. 
 * The function should then return the Count of Matches, Average and Sum of the scores.
 */

namespace CodeChallenge_3
{
    class CricketTeam
    {
        public (int count, int sum, double average) Pointscalculation(int no_of_matchs)
        {
            List<int> scores = new List<int>();

            for (int i = 0; i < no_of_matchs; i++)
            {
                bool value = false;
                while (!value)
                {
                    Console.Write($"Enter score for match {i + 1}: ");
                    try
                    {
                        int score = Convert.ToInt32(Console.ReadLine());
                        scores.Add(score);
                        value = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Number too large. Please enter a smaller integer.");

                    }
                }
            }

            int sum = 0;
            foreach (int s in scores)
            {
                sum += s;
            }

            double average = no_of_matchs > 0 ? (double)sum / no_of_matchs : 0;

            return (no_of_matchs, sum, average);
        }
    }

    class IPL_Score
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter number of matches played by your IPL_Team: ");
                try
                {

                    int no_of_matches = Convert.ToInt32(Console.ReadLine());

                    if (no_of_matches <= 0)
                    {
                        Console.WriteLine("Number of matches must be greater than zero.");
                        continue;
                    }

                    CricketTeam cricketteam = new CricketTeam();
                    var result = cricketteam.Pointscalculation(no_of_matches);

                    Console.WriteLine($"The Match Count: {result.count}");
                    Console.WriteLine($"The Total Score: {result.sum}");
                    Console.WriteLine($"The Average Score: {result.average:F2}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input from you. Please enter a valid integer.");
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Number is too large. Please enter a smaller integer.");
                    continue;
                }

                Console.Write("Do you want to calculate for another team? select one in (y/n): ");
                string select = Console.ReadLine().ToLower();

                if (select != "y")
                {

                    break;
                }
            }

            Console.WriteLine("Press any key to exit......");
            Console.Read();
        }
    }
}


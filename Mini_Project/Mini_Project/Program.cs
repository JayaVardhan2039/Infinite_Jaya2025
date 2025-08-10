using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using Mini_Project.Authentication;


namespace MiniProj
{
    public class Program
    {
        //Straing point of the project
        public static void Main()
        {
            ShowTrainAnimation();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to the Infinite Railway Reservation System");
                Console.WriteLine("1. Register as User");
                Console.WriteLine("2. Login as Admin");
                Console.WriteLine("3. Login as User");
                Console.WriteLine("4. Exit, Thank you!");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                int choice;

                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            UserGenesis.RegisterUser();
                            break;
                        case 2:
                            LoginFlow.AdminLogin();
                            break;
                        case 3:
                            LoginFlow.UserLogin();
                            break;
                        case 4:
                            Console.WriteLine("Exit,Thank you!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }

        //onload animation for the system


        internal static void ShowTrainAnimation()
        {
            

            string[] train = new string[]
            {
        "                      W E L C O M E     T O     I N F I N I T E     R A I L W A Y S                      ",
        "       ___      ____      ____      ____      ____      ____      ____      ____      ____      ____      ____      ____      ____      ____      ____      ",
        "      /   |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|    |----|",
        "   _ |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |____|    |",
        " (/ /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\  /|____|\\",
        "(/ O-O-O-O--O-O-O-O--O-O-O-O-O-O-   -O-O-O-O--O-O-O-O--O-O-O-O     -O-O--O-O-O-O--O-O-O-O--O-O-O-O--O-O-O-O-O-  -O-O--O-O-O-O--O-O-O-O--O-O-O-O--O-O-O-O--O   O-O-O-O-O-O-O-O-O-O"
            };

        int width = Console.WindowWidth;
        int trainLength = train[5].Length;
        int trainHeight = train.Length;

            for (int i = width; i >= -trainLength; i--)
            {
                Console.Clear();
                for (int j = 0; j<trainHeight; j++)
                {
                    string line = train[j];
                    int startPos = Math.Max(i, 0);
                    int skip = i < 0 ? -i : 0;

                    if (skip<line.Length)
                    {
                        int visibleLength = Math.Min(line.Length - skip, width - startPos);
                        if (visibleLength > 0)
                        {
                            string visiblePart = line.Substring(skip, visibleLength);
                            Console.SetCursorPosition(startPos, j + 2);
                            Console.Write(visiblePart);
                        }
}
                }
                Thread.Sleep(30);
            }

            Thread.Sleep(1500); // Pause after animation
        }
        
    }
}

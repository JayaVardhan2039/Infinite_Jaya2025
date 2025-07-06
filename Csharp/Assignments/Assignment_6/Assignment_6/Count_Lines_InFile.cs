using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*
 * 3. Write a program in C# Sharp to count the number of lines in a file.
 */
namespace Assignment_6
{
    class Count_Lines_InFile
    {
        static void Main()
        {
            string defaultPath = @"MyStringArray.txt";
            //i gave user choice to select the file
            Console.WriteLine("Do you want to enter a custom file path? (yes/no): ");
            string choice = Console.ReadLine().ToLower();

            string filePath;

            if (choice == "yes")
            {
                Console.Write("Enter the full file path: ");
                filePath = Console.ReadLine();
                //Console.WriteLine(filePath);
            }
            else
            {
                filePath = defaultPath;
                Console.WriteLine($"Using default path: {filePath}");
            }

            FileStream fs = null;
            StreamReader reader = null;
            int lineCount = 0;

            try
            {
                if (File.Exists(filePath))
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(fs);

                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }

                    Console.WriteLine($"The file contains {lineCount} lines.");
                }
                else
                {
                    Console.WriteLine("File not found at the specified path.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs != null)
                    fs.Close();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

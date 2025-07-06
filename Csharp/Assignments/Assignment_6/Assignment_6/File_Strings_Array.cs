using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*
 *2. Write a program in C# Sharp to create a file and write an array of strings to the file.
 */
namespace Assignment_6
{
    class File_Strings_Array
    {
        static void Main()
        {
            List<string> bookList = new List<string>();
            string input;

            Console.WriteLine("Enter desired strings (type 'done' to finish):");
            int i = 1;
            while (true)
            {
                Console.Write("Enter string {0}: ", i);
                i++;
                input = Console.ReadLine();

                if (input.ToLower() == "done")
                    break;

                bookList.Add(input);
            }

            string filePath = @"MyStringArray.txt";
            FileStream fs = null;
            StreamWriter writer = null;

            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(fs);

                foreach (string line in bookList)
                {
                    writer.WriteLine(line);
                }

                Console.WriteLine($"File written successfully to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing file: " + ex.Message);
            }
            finally
            {
                // Ensure resources are released
                if (writer != null)
                    writer.Close();
                if (fs != null)
                    fs.Close();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

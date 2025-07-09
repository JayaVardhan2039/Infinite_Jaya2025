using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/*
 * 3. Write a program in C# Sharp to append some text to an existing file. 
 * If file is not available, then create one in the same workspace.
 * Hint: (Use the appropriate mode of operation. Use stream writer class)
 */
namespace Assignment_6
{
    class File_Append
    {
        static void Main()
        {
            string filePath = "blah_blah.txt";

            while (true)
            {
                try
                {
                    Console.WriteLine("You can enter multiple lines of text to append to the file (type 'END' on a new line to finish):");
                    StringBuilder InputBuilder = new StringBuilder();
                    string line;
                    while ((line = Console.ReadLine()) != null)
                    {
                        if (line.ToUpper() == "END")
                            break;
                        InputBuilder.Append(line);
                    }

                    string inputText = InputBuilder.ToString();

                    if (string.IsNullOrWhiteSpace(inputText))
                    {
                        Console.WriteLine("Empty input. Ther is nothing to append.");
                        continue;
                    }


                    FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(fs);

                    writer.WriteLine(inputText);

                    writer.Close();
                    fs.Close();

                    Console.WriteLine($"Text successfully appended to the file {filePath}.");
                }
                catch (IOException except)
                {
                    Console.WriteLine($"File operation failed: {except.Message}");
                }

                Console.Write("Do you want to append more text? type (y/n): ");
                string select = Console.ReadLine().ToLower();
                if (select != "y")
                    break;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

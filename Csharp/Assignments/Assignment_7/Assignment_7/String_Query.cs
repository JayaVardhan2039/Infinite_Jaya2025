using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 
2.)

Write a query that returns words starting with letter 'a' and ending with letter 'm'.


Expected input and output
"mum", "amsterdam", "bloom" → "amsterdam"
 */
namespace Assignment_7
{
    class String_Query
    {

        static void Main()
        {
            List<string> words = new List<string>();
            string input;
            int i = 1;

            Console.WriteLine("Enter words one by one (type 'done' to finish):");

            while (true)
            {
                Console.Write($"Enter word {i}: ");
                input = Console.ReadLine();
                i++;

                if (input.ToLower() == "done")
                    break;

                words.Add(input);
            }

            var result = words
            .Where(w => w.ToLower().StartsWith("a") && w.ToLower().EndsWith("m"));

            Console.WriteLine("\nWords starting with 'a' and ending with 'm':");
            foreach (var word in result)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    
}
}

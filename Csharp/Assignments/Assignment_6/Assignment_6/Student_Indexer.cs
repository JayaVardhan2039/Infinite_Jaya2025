using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 1. Create a class called Books with BookName and AuthorName as members. Instantiate the class through constructor and also write a method Display() to display the details. 

Create an Indexer of Books Object to store 5 books in a class called BookShelf. Using the indexer method assign values to the books and display the same.

Hint(use Aggregation/composition)

 */

namespace Assignment_6
{
    class Books
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }

        public Books(string bookname, string authorname)
        {
            this.BookName = bookname;
            this.AuthorName = authorname;
        }

        public void Display()
        {
            Console.WriteLine($"Book: {BookName}, Author: {AuthorName}");
        }

    }

    class BooksShelf
        {
        public Books[] books = new Books[5];

        public Books this[int i]
        {
            //indexer
            get { return books[i]; }
            set { books[i] = value; }
        }
        }
    class Student_Indexer
    {
        static void Main(string[] args)
        {

            BooksShelf shelf = new BooksShelf();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Enter details for Book {i + 1}:");

                Console.Write("Book Name: ");
                string bookName = Console.ReadLine();

                Console.Write("Author Name: ");
                string authorName = Console.ReadLine();

                shelf[i] = new Books(bookName, authorName);
                Console.WriteLine(); 
            }


            Console.WriteLine("Books in the shelf:");
            for (int i = 0; i < 5; i++)
            {
                shelf[i].Display();
            }
            Console.WriteLine("Press any key to exit.....");

            Console.Read();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 3. Create a class called Books with BookName and AuthorName as members. Instantiate the class through constructor and also write a method Display() to display the details. 

Create an Indexer of Books Object to store 5 books in a class called BookShelf. Using the indexer method assign values to the books and display the same.

Hint(use Aggregation/composition)
 */
namespace Assignment_5
{
    // Book class 
    public class Book
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }

        public Book(string bookName, string authorName)
        {
            BookName = bookName;
            AuthorName = authorName;
        }

        public void Display()
        {
            Console.WriteLine($"Book: {BookName}, Author: {AuthorName}");
        }
    }

    // BookShelf class 
    public class BookShelf
    {
        private Book[] books = new Book[5];

        // Indexer
        public Book this[int index]
        {
            get
            {
                if (index >= 0 && index < books.Length)
                    return books[index];
                else
                    throw new IndexOutOfRangeException("Invalid index for bookshelf.");
            }
            set
            {
                if (index >= 0 && index < books.Length)
                    books[index] = value;
                else
                    throw new IndexOutOfRangeException("Invalid index for bookshelf.");
            }
        }

        public void DisplayAllBooks()
        {
            Console.WriteLine("\nBooks on the Shelf:");
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] != null)
                    books[i].Display();
                else
                    Console.WriteLine($"Slot {i + 1}: Empty");
            }
        }
    }

    class Book_Shelf
    {
        static void Main(string[] args)
        {
            BookShelf shelf = new BookShelf();

            Console.WriteLine("Enter details for 5 books:");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"\nBook {i + 1}:");

                Console.Write("Enter Book Name: ");
                string bookName = Console.ReadLine();

                Console.Write("Enter Author Name: ");
                string authorName = Console.ReadLine();

                shelf[i] = new Book(bookName, authorName);
            }

            
            shelf.DisplayAllBooks();

            Console.Read();
        }
    }
}

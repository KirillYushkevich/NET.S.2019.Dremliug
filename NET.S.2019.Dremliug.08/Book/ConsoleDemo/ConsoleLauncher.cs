using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTask
{
    internal class ConsoleLauncher
    {
        private static Book[] newBooks = new[]
        {
            new Book(0143105426, "Jane Austen", "Pride and Prejudice", "Penguin Classics", 2009),
            new Book(null, "Howard I. Chapelle", "The History of The American Sailing Ships", "W. W. Norton", 1935),
            new Book(0670022691, "Amor Towles", "Rules of Civility", "Viking", 2011),
            };

        public static void Main()
        {
            Console.WriteLine("Start of demo\n");
            BookListService demoService = new BookListService();
            BookBinaryFileStorage demoStorage = new BookBinaryFileStorage();

            demoService.LoadFromStorage(demoStorage);
            Display(demoService);

            Console.WriteLine("Sorting\n");
            demoService.SortBooksByTag(new BookComparerByTag("Author"));
            Display(demoService);

            Console.WriteLine("Find by tag: ");
            foreach (Book book in demoService.FindBookByTag("Rules"))
            {
                Console.WriteLine(book.ToString());
            }

            Console.WriteLine();

            foreach (Book book in newBooks)
            {
                try
                {
                    demoService.AddBook(book);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"Already in service: {book.Title}");
                }
            }

            demoService.SaveToStorage(demoStorage);

            Console.WriteLine("\nEnd of demo");
            Console.ReadLine();
        }

        private static void Display(BookListService service)
        {
            foreach (Book book in service)
            {
                Console.WriteLine(book.ToString());
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTask
{
    public class BookListService : IBookService
    {
        private HashSet<Book> set;
        private List<Book> list;

        public BookListService()
        {
            set = new HashSet<Book>();
            list = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if(book is null)
            {
                throw new ArgumentNullException($"Book must not be null");
            }

            if (set.Contains(book))
            {
                throw new ArgumentException($"Book is already in the list");
            }

            set.Add(book);
            list.Add(book);
        }

        public void RemoveBook(Book book)
        {
            if (book is null)
            {
                throw new ArgumentNullException($"Book must not be null");
            }

            if (! set.Contains(book))
            {
                throw new ArgumentException($"Book not found");
            }

            set.Remove(book);
            list.Remove(book);
        }

        public IEnumerable<Book> FindBookByTag(string tag)
        {
            List<Book> result = new List<Book>();

            foreach (Book book in list)
            {
                if ((book.Author?.Contains(tag) ?? false) ||
                    (book.Title?.Contains(tag) ?? false) ||
                    (book.Publisher?.Contains(tag) ?? false))
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public IEnumerable<Book> FindBookByTag(int tag)
        {
            List<Book> result = new List<Book>();

            foreach (Book book in list)
            {
                if ((book.Isbn.HasValue && book.Isbn.Value == (ulong)tag) ||
                    (book.Year.HasValue && book.Year.Value == tag))
                {
                    result.Add(book);
                }
            }

            return result;
        }

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            list.Sort(comparer);
        }

        public void LoadFromStorage(IBookStorage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException($"Storage must not be null");
            }

            foreach (Book book in storage.Load())
            {
                try
                {
                    AddBook(book);
                }
                catch (ArgumentException)
                {
                    // Do nothing. Suppress duplicate book errors on load.
                }
            }
        }

        public void SaveToStorage(IBookStorage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException($"Storage must not be null");
            }

            storage.Save(set);
        }

        public IEnumerator<Book> GetEnumerator() => list.GetEnumerator();
    }
}
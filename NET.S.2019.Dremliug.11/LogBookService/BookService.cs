using System;
using System.Collections.Generic;
using BookTask;

namespace NET.S._2019.Dremliug._11
{
    public class BookService : IBookService
    {
        private ILogger _logger;
        private HashSet<Book> _set;
        private List<Book> _list;

        public BookService()
        {
            _set = new HashSet<Book>();
            _list = new List<Book>();
        }

        public void StartLogging(ILogger logger)
        {
            _logger?.Info($"Starting logging: {DateTime.Now}");
            _logger = logger ?? throw new ArgumentNullException($"Logger is null");
        }

        public void StopLogging()
        {
            _logger?.Info($"Stopping logging: {DateTime.Now}");
            _logger = null;
        }

        public void AddBook(Book book)
        {
            if (book is null)
            {
                _logger?.Warn($"Attempt to add null Book: {DateTime.Now}");
                throw new ArgumentNullException($"Book must not be null");
            }

            if (_set.Contains(book))
            {
                _logger?.Warn($"Attempt to add existing Book: {DateTime.Now}");
                throw new ArgumentException($"Book is already in the list");
            }

            _set.Add(book);
            _list.Add(book);
            _logger?.Debug($"Book successfully added.");
        }

        public void RemoveBook(Book book)
        {
            if (book is null)
            {
                _logger?.Warn($"Attempt to remove null Book: {DateTime.Now}");
                throw new ArgumentNullException($"Book must not be null");
            }

            if (!_set.Contains(book))
            {
                _logger?.Warn($"Attempt to remove non-existing existing Book: {DateTime.Now}");
                throw new ArgumentException($"Book not found");
            }

            _set.Remove(book);
            _list.Remove(book);
            _logger?.Debug($"Book successfully removed.");
        }

        public IEnumerable<Book> FindBookByTag(string tag)
        {
            List<Book> result = new List<Book>();

            _logger?.Debug($"Started search book by string tag");

            foreach (Book book in _list)
            {
                if ((book.Author?.Contains(tag) ?? false) ||
                    (book.Title?.Contains(tag) ?? false) ||
                    (book.Publisher?.Contains(tag) ?? false))
                {
                    result.Add(book);
                }
            }

            _logger?.Debug($"Finished search book by string tag, found: {result.Count}");

            return result;
        }

        public IEnumerable<Book> FindBookByTag(int tag)
        {
            List<Book> result = new List<Book>();

            _logger?.Debug($"Started search book by int tag");

            foreach (Book book in _list)
            {
                if ((book.Isbn.HasValue && book.Isbn.Value == (ulong)tag) ||
                    (book.Year.HasValue && book.Year.Value == tag))
                {
                    result.Add(book);
                }
            }

            _logger?.Debug($"Finished search book by int tag, found: {result.Count}");

            return result;
        }

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            _logger?.Debug($"Started sorting.");
            _list.Sort(comparer);
            _logger?.Debug($"Finished sorting.");
        }

        public void LoadFromStorage(IBookStorage storage)
        {
            if (storage == null)
            {
                _logger?.Warn($"Attempt to load from null storage: {DateTime.Now}");
                throw new ArgumentNullException($"Storage must not be null");
            }

            foreach (Book book in storage.Load())
            {
                _logger?.Debug($"Started load from storage.");
                try
                {
                    AddBook(book);
                }
                catch (ArgumentException)
                {
                    // Do nothing. Suppress duplicate book errors on load.
                    _logger?.Info($"Duplicate book found while loading from storage: {book.ToString()}");
                }

                _logger?.Debug($"Finished load from storage.");
            }
        }

        public void SaveToStorage(IBookStorage storage)
        {
            if (storage == null)
            {
                _logger?.Warn($"Attempt to save to null storage: {DateTime.Now}");
                throw new ArgumentNullException($"Storage must not be null");
            }

            _logger?.Debug($"Started save to storage.");
            storage.Save(_set);
            _logger?.Debug($"Finished save to storage.");
        }

        public IEnumerator<Book> GetEnumerator() => _list.GetEnumerator();
    }
}
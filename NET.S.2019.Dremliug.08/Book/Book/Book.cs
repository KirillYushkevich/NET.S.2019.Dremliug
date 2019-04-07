using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTask
{
    /// <summary>
    /// Book
    /// </summary>
    public class Book : IEquatable<Book>, IComparable<Book>, IComparable
    {
        #region Fields
        private int? _cachedHashcode = null;
        private string _cachedString = null;
        #endregion

        #region Properties
        public ulong? Isbn { get; private set; }
        public string Author { get; private set; }
        public string Title { get; private set; }
        public string Publisher { get; private set; }
        public int? Year { get; private set; }
        public int? Pages { get; private set; }
        public decimal? Price { get; private set; }
        #endregion

        #region Constructor
        public Book(ulong? isbn, string author = null, string title = null, string publisher = null, int? year = null, int? pages = null, decimal? price = null)
        {
            CheckValues(isbn, author, title, publisher, year, pages, price);

            Isbn = isbn;
            Author = author;
            Title = title;
            Publisher = publisher;
            Year = year;
            Pages = pages;
            Price = price;
        }
        #endregion

        /// <summary>
        /// Updates <see cref="Book"/> properties with given values.
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="author"></param>
        /// <param name="title"></param>
        /// <param name="publisher"></param>
        /// <param name="year"></param>
        /// <param name="pages"></param>
        /// <param name="price"></param>
        public void Update(ulong? isbn = null, string author = null, string title = null, string publisher = null, int? year = null, int? pages = null, decimal? price = null)
        {
            CheckValues(isbn, author, title, publisher, year, pages, price);

            Isbn = isbn ?? Isbn;
            Author = author ?? Author;
            Title = title ?? Title;
            Publisher = publisher ?? Publisher;
            Year = year ?? Year;
            Pages = pages ?? Pages;
            Price = price ?? Price;

            _cachedHashcode = null;
            _cachedString = null;
        }

        #region IEquatable<T>
        public override bool Equals(object obj) => Equals(obj as Book);

        public bool Equals(Book other)
        {
            return ReferenceEquals(this, other) ||
                (
                other != null &&
                this.GetType() == other.GetType() &&

                // Pages and Price are not checked and can be safely modified from the outside.
                (this.Isbn, this.Author, this.Title, this.Publisher, this.Year) ==
                (other.Isbn, other.Author, other.Title, other.Publisher, other.Year)
                );
        }

        public override int GetHashCode()
        {
            if (!_cachedHashcode.HasValue)
            {
                _cachedHashcode = (Isbn, Author, Title, Publisher, Year).GetHashCode();
            }

            return _cachedHashcode.Value;
        }

        public static bool operator ==(Book book1, Book book2)
        {
            return EqualityComparer<Book>.Default.Equals(book1, book2);
        }

        public static bool operator !=(Book book1, Book book2)
        {
            return !(book1 == book2);
        }
        #endregion

        #region IComparable<T>
        public virtual int CompareTo(object obj) => CompareTo(obj as Book);

        public virtual int CompareTo(Book other)
        {
            return
                ReferenceEquals(this, other) ? 0 :
                other is null ? 1 :
                this.Isbn != other.Isbn ? Nullable.Compare(this.Isbn, other.Isbn) :
                this.Author != other.Author ? string.Compare(this.Author, other.Author) :
                this.Title != other.Title ? string.Compare(this.Title, other.Title) :
                this.Publisher != other.Publisher ? string.Compare(this.Publisher, other.Publisher) :
                Nullable.Compare(this.Year, other.Year);
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            if (_cachedString is null)
            {
                _cachedString = $"[ISBN: {Isbn}, Author: {Author}, Title: {Title}, Publisher: {Publisher}, Year: {Year}, Pages: {Pages}, Price: {Price:C}]";
            }

            return _cachedString;
        }
        #endregion

        #region private helper methods
        /// <summary>
        /// Helper method to check input parametes.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if any parameter is incorrect.</exception>
        private void CheckValues(ulong? isbn, string author, string title, string publisher, int? year, int? pages, decimal? price)
        {
            if (isbn.HasValue && isbn > 9_999_999_999_999)
            {
                throw new ArgumentException($"Incorrect ISBN: {isbn}");
            }

            if (year.HasValue && year < 1)
            {
                throw new ArgumentException($"Incorrect Year: {year}");
            }

            if (pages.HasValue && pages < 1)
            {
                throw new ArgumentException($"Incorrect Pages: {pages}");
            }

            if (price.HasValue && price < 0)
            {
                throw new ArgumentException($"Incorrect Price: {price}");
            }
        }
        #endregion
    }
}
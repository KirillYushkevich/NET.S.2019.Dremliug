using System;

namespace BookFormat
{
    /// <summary>
    /// Simple class for training <see cref="IFormattable"/> implementation.
    /// </summary>
    public class Book : IFormattable
    {
        #region Constructor
        public Book(ulong? isbn, string author = null, string title = null, string publisher = null, int? year = null, int? pages = null, decimal? price = null)
        {
            Isbn = isbn;
            Author = author;
            Title = title;
            Publisher = publisher;
            Year = year;
            Pages = pages;
            Price = price;
        }
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

        #region ToString, IFormattable
        public override string ToString()
        {
            return $"[ISBN: {Isbn}, Author: {Author}, Title: {Title}, Publisher: {Publisher}, Year: {Year}, Pages: {Pages}, Price: {Price:C}]";
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            switch (format?.ToUpperInvariant())
            {
                case "%SHORT":
                    return $"[Author: {Author}, Title: {Title}]";
                case "%MEDIUM":
                    return $"[Author: {Author}, Title: {Title}, Publisher: {Publisher}, Year: {Year}]";
                case "%LONG":
                    return $"[ISBN: {Isbn}, Author: {Author}, Title: {Title}, Publisher: {Publisher}, Year: {Year}, Pages: {Pages}]";
                case "%ALL":
                case "G":
                case "":
                case null:
                    return ToString();
                default:
                    throw new FormatException($"Format \"{format}\" is not supported.");
            }
        }
        #endregion
    }
}
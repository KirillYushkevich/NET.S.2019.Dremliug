using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTask
{
    internal class BookBinaryFileStorage : IBookStorage
    {
        private readonly string _filePath = "SavedBooks.bin";

        public BookBinaryFileStorage(string filePath = null)
        {
            if (filePath != null)
            {
                _filePath = filePath;
            }
        }

        public void Save(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_filePath, FileMode.Create)))
            {
                foreach (Book book in books)
                {
                    writer.Write(book.Isbn ?? 0);
                    writer.Write(book.Isbn.HasValue);

                    writer.Write(book.Author ?? string.Empty);
                    writer.Write(book.Author != null);

                    writer.Write(book.Title ?? string.Empty);
                    writer.Write(book.Title != null);

                    writer.Write(book.Publisher ?? string.Empty);
                    writer.Write(book.Publisher != null);

                    writer.Write(book.Year ?? 0);
                    writer.Write(book.Year.HasValue);

                    writer.Write(book.Pages ?? 0);
                    writer.Write(book.Pages.HasValue);

                    writer.Write(book.Price ?? 0);
                    writer.Write(book.Price.HasValue);
                }
            }
        }

        public IEnumerable<Book> Load()
        {
            List<Book> result = new List<Book>();

            using (var reader = new BinaryReader(File.Open(_filePath, FileMode.OpenOrCreate)))
            {
                while (reader.PeekChar() != -1)
                {
                    ulong? isbn = reader.ReadUInt64();
                    isbn = reader.ReadBoolean() ? isbn : null;

                    string author = reader.ReadString();
                    author = reader.ReadBoolean() ? author : null;

                    string title = reader.ReadString();
                    title = reader.ReadBoolean() ? title : null;

                    string publisher = reader.ReadString();
                    publisher = reader.ReadBoolean() ? publisher : null;

                    int? year = reader.ReadInt32();
                    year = reader.ReadBoolean() ? year : null;

                    int? pages = reader.ReadInt32();
                    pages = reader.ReadBoolean() ? pages : null;

                    decimal? price = reader.ReadDecimal();
                    price = reader.ReadBoolean() ? price : null;

                    result.Add(new Book(isbn, author, title, publisher, year, pages, price));
                }
            }

            return result;
        }
    }
}

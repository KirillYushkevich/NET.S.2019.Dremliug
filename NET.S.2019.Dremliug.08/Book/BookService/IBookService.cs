using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2019.Dremliug._08
{
    public interface IBookService
    {
        void AddBook(Book book);

        void RemoveBook(Book book);

        IEnumerable<Book> FindBookByTag(string tag);

        void SortBooksByTag(IComparer<Book> comparer);

        void LoadFromStorage(IBookStorage storage);

        void SaveToStorage(IBookStorage storage);
    }
}

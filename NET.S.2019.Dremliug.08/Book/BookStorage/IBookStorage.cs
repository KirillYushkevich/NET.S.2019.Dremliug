using System.Collections.Generic;

namespace BookTask
{
    public interface IBookStorage
    {
        void Save(IEnumerable<Book> books);

        IEnumerable<Book> Load();
    }
}

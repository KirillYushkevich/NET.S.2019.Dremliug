using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.S._2019.Dremliug._08
{
    public interface IBookStorage
    {
        void Save(IEnumerable<Book> books);

        IEnumerable<Book> Load();
    }
}

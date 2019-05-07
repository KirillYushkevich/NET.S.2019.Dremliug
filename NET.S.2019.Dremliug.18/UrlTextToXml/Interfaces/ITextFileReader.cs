using System.Collections.Generic;

namespace UrlTextToXml.Interfaces
{
    /// <summary>
    /// Provides reading URL strings.
    /// </summary>
    public interface ITextFileReader
    {
        IEnumerable<string> ReadStrings();
    }
}

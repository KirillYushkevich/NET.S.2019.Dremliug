using System.Collections.Generic;

namespace UrlTextToXml.Interfaces
{
    /// <summary>
    /// Provides conversion from raw strings to an xml file.
    /// </summary>
    public interface IXmlFileCreator
    {
        void CreateXml(IEnumerable<string> urls, string path);
    }
}

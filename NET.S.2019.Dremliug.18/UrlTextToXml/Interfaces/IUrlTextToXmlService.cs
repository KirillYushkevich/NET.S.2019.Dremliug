namespace UrlTextToXml.Interfaces
{
    /// <summary>
    /// Provides a conversion service from a text file with URLs to an XML file.
    /// </summary>
    public interface IUrlTextToXmlService
    {
        string UrlTextToXml(string inputFile);
    }
}

namespace UrlTextToXml.Interfaces
{
    /// <summary>
    /// Provides url string validation.
    /// </summary>
    public interface IUrlValidator
    {
        bool IsValid(string url);
    }
}

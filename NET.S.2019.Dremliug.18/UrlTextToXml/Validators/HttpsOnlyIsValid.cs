using System;

namespace UrlTextToXml
{
    public class HttpsOnlyIsValid : Interfaces.IUrlValidator
    {
        bool Interfaces.IUrlValidator.IsValid(string url)
        {
            bool success = Uri.TryCreate(url, UriKind.Absolute, out Uri uri);

            return uri?.Scheme == "https" && success;
        }
    }
}

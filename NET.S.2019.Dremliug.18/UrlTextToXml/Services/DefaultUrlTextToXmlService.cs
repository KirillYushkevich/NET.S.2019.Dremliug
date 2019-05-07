using System;
using System.IO;
using System.Linq;
using UrlTextToXml.Interfaces;

namespace UrlTextToXml
{
    /// <summary>
    /// Service implementation for console application demo.
    /// </summary>
    public class DefaultUrlTextToXmlService : Interfaces.IUrlTextToXmlService
    {
        public DefaultUrlTextToXmlService(ITextFileReader reader, IUrlValidator validator, IXmlFileCreator creator, ILogger logger = null)
        {
            this.TextFileReader = reader;
            this.UrlValidator = validator;
            this.XmlFileCreator = creator;
            this.Logger = logger;
        }

        internal ITextFileReader TextFileReader { get; set; }

        internal IUrlValidator UrlValidator { get; set; }

        internal IXmlFileCreator XmlFileCreator { get; set; }

        internal ILogger Logger { get; set; }

        string Interfaces.IUrlTextToXmlService.UrlTextToXml(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                throw new ArgumentException($"Source file does not exist.");
            }

            string outputFile = Path.Combine(Path.GetDirectoryName(inputFile), $"{Path.GetFileNameWithoutExtension(inputFile)}.xml");

            this.XmlFileCreator.CreateXml(
               this.TextFileReader.ReadStrings()
               .Where((url, line) =>
               {
                   bool result;
                   if ((result = UrlValidator.IsValid(url)) == false)
                   {
                       Logger?.Info($"Unhandled {url} at line {line}");
                   }

                   return result;
               }),
               outputFile);

            return outputFile;
        }
    }
}

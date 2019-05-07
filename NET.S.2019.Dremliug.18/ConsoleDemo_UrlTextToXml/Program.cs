using System;
using System.IO;
using System.Xml.Linq;
using Ninject;
using UrlTextToXml;
using UrlTextToXml.Interfaces;

namespace ConsoleDemo_UrlTextToXml
{
    internal class Program
    {
        private static string[] DemoUrls
        {
            get;
        }
        =
        {
            @"https://github.com/AnzhelikaKravchuk?tab=repositories=4",
            @"https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU",
            @"http://github.com/AnzhelikaKravchuk/Algorithms-and-Data-Structures", // must fail because of Http
            @"https://habrahabr.ru/company/it-grad/blog/341486/",
            @"https://habrahabr.ru/company/it-grad/blog/341486/",
            @"https:/habr.com/ru/company/it-grad/blog/449476/", // must fail because of invalid url
            @"https://example01.com/", // no path and no params with slash
            @"https://example02.com", // no path and no params without slash
            @"https://example03.com/path03a/path03b", // no params
            @"https://example04.com/path04a/path04b?key04a=value04a&key04b=value04b&key04c=value04c", // many params

        };

        private static void Main(string[] args)
        {
            var path = args.Length > 0 ? args[0] : null;

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                // path = @"Links.txt"; // Create files near .exe
                path = @"Z:\Links.txt"; // Create files at the specified path.

                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (string url in DemoUrls)
                    {
                        writer.WriteLine(url);
                    }
                }
            }

            IKernel kernel = new StandardKernel();
            kernel.Bind<ILogger>().To<ConsoleLogger>();
            kernel.Bind<ITextFileReader>().ToConstructor(_ => new NewLineSeparatedFileReader(path));
            kernel.Bind<IUrlValidator>().To<HttpsOnlyIsValid>();
            kernel.Bind<IXmlFileCreator>().To<UriToXmlConverter>();
            kernel.Bind<IUrlTextToXmlService>().To<DefaultUrlTextToXmlService>();

            var service = kernel.Get<IUrlTextToXmlService>();

            string newPath = service.UrlTextToXml(path);
            Console.WriteLine();
            Console.WriteLine(XDocument.Load(newPath));

            Console.ReadLine();
        }
    }
}

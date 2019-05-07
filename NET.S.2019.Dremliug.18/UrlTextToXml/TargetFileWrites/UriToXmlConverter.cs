using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UrlTextToXml
{
    /// <summary>
    /// Represents a converter from a collection of strings to an XML file. Relies on LINQ to XML and Uri classes.
    /// </summary>
    public class UriToXmlConverter : Interfaces.IXmlFileCreator
    {
        void Interfaces.IXmlFileCreator.CreateXml(IEnumerable<string> urls, string path)
        {
            XElement formedAddresses = new XElement(
                "urlAddresses",
                    urls.Select(url =>
                    {
                        Uri.TryCreate(url, UriKind.Absolute, out Uri uri);
                        return UriToXelement(uri);
                    }));

            new XDocument(formedAddresses).Save(path);
        }

        private XElement UriToXelement(Uri uri)
        {
            return (uri is null) ? null
                : new XElement(
                    "urlAddress",
                        GetHost(uri.Host),
                        GetSegments(uri.Segments),
                        GetParameters(uri.Query));

            // local function.
            XElement GetHost(string host)
            {
                return new XElement("host", new XAttribute("name", host));
            }

            // local function.
            XElement GetSegments(string[] segments)
            {
                // The first segment always exists and is empty or "/". Ignore if only one.
                return (segments.Length > 1) ?
                    new XElement("uri", segments.Skip(1).Select(segment => new XElement("segment", segment.Replace("/", string.Empty))))
                    : null;
            }

            // local function.
            XElement GetParameters(string query)
            {
                return (query.Length > 0) ?
                    new XElement(
                        "parameters",
                        query.Substring(1).Split('&')
                        .Select(p =>
                        {
                            int index = p.IndexOf('=');
                            string key = p.Substring(0, index);
                            string value = p.Substring(index + 1);
                            return new XElement("parameter", new XAttribute("value", value), new XAttribute("key", key));
                        }))
                    : null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace UrlTextToXml
{
    /// <summary>
    /// Implements an <see cref="Interfaces.ITextFileReader"/> by reading strings from a file.
    /// </summary>
    public class NewLineSeparatedFileReader : Interfaces.ITextFileReader
    {
        private string _filePath;

        public NewLineSeparatedFileReader(string path)
        {
            this._filePath = File.Exists(path) ? path : throw new ArgumentException($"Cannot find the file: {path}");
        }

        /// <summary>
        /// Reads all strings from the current file.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> Interfaces.ITextFileReader.ReadStrings()
        {
            using (StreamReader streamReader = new StreamReader(this._filePath))
            {
                string currentLine = null;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    yield return currentLine;
                }
            }
        }
    }
}

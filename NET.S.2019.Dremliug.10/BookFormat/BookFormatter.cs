using System;

namespace BookFormat
{
    /// <summary>
    /// Extends <see cref="Book"/> type formatting.
    /// </summary>
    public class BookFormatter : IFormatProvider, ICustomFormatter
    {
        /// <summary><see cref="IFormatProvider"/> implementation.</summary>
        /// <param name="formatType"></param>
        /// <returns>Returns returns an instance of itself if <see cref="ICustomFormatter"/> was requested, null otherwise. </returns>
        public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;

        /// <summary>
        /// Formats the <see cref="Book"/> instance and allows other types to format themselves.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            switch (arg)
            {
                case Book book:
                    
                    switch (format?.ToUpperInvariant())
                    {
                        case "%TA":
                            return $"[Title: {book.Title}, Author: {book.Author}]";

                        case "%I":
                            return $"[ISBN: {book.Isbn}]";

                        case "%T":
                            return $"[Title: {book.Title}]";

                        default:
                            try
                            {
                                return book.ToString(format);
                            }
                            catch (FormatException)
                            {
                                throw new FormatException($"Format \"{format}\" is not supported.");
                            }
                    }

                case IFormattable obj:
                    try
                    {
                        return obj.ToString(format, null);
                    }
                    catch (FormatException)
                    {
                        throw new FormatException($"Format \"{format}\" is not supported.");
                    }

                case null:
                    return string.Empty;

                default:
                    return arg.ToString();
            }
        }
    }
}
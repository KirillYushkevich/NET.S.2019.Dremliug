using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookFormat;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BookFormatTests
    {
        private static Book book = new Book(0670022691, "Amor Towles", "Rules of Civility", "Viking", 2011, 128, 5.61M);

        private static IEnumerable<TestCaseData> IFormattable_TestsData
        {
            get
            {
                yield return new TestCaseData("%SHORT", $"[Author: {book.Author}, Title: {book.Title}]");
                yield return new TestCaseData("%Short", $"[Author: {book.Author}, Title: {book.Title}]");
                yield return new TestCaseData("%MEDIUM", $"[Author: {book.Author}, Title: {book.Title}, Publisher: {book.Publisher}, Year: {book.Year}]");
                yield return new TestCaseData("%long", $"[ISBN: {book.Isbn}, Author: {book.Author}, Title: {book.Title}, Publisher: {book.Publisher}, Year: {book.Year}, Pages: {book.Pages}]");
                yield return new TestCaseData("%ALL", $"[ISBN: {book.Isbn}, Author: {book.Author}, Title: {book.Title}, Publisher: {book.Publisher}, Year: {book.Year}, Pages: {book.Pages}, Price: {book.Price:C}]");
                yield return new TestCaseData("g", $"[ISBN: {book.Isbn}, Author: {book.Author}, Title: {book.Title}, Publisher: {book.Publisher}, Year: {book.Year}, Pages: {book.Pages}, Price: {book.Price:C}]");
                yield return new TestCaseData(null, $"[ISBN: {book.Isbn}, Author: {book.Author}, Title: {book.Title}, Publisher: {book.Publisher}, Year: {book.Year}, Pages: {book.Pages}, Price: {book.Price:C}]");
            }
        }

        private static IEnumerable<TestCaseData> Exceptions_TestsData
        {
            get
            {
                yield return new TestCaseData("%M");
            }
        }

        private static IEnumerable<TestCaseData> BookFormatter_TestsData
        {
            get
            {
                yield return new TestCaseData("%TA", $"[Title: {book.Title}, Author: {book.Author}]");
                yield return new TestCaseData("%I", $"[ISBN: {book.Isbn}]");
                yield return new TestCaseData("%t", $"[Title: {book.Title}]");
                yield return new TestCaseData("%Short", $"[Author: {book.Author}, Title: {book.Title}]");
            }
        }

        // $"{book:%ALL}";
        [TestCaseSource(nameof(IFormattable_TestsData))]
        public void IFormattable_Tests(string format, string expected)
            => Assert.AreEqual(expected, book.ToString(format));

        [TestCaseSource(nameof(Exceptions_TestsData))]
        public void IFormattable_Exceptions(string format)
            => Assert.Throws<FormatException>(() => book.ToString(format));

        // ((FormattableString)$"{book:%TA}").ToString(new BookFormatter());
        [TestCaseSource(nameof(BookFormatter_TestsData))]
        public void BookFormatter_Tests(string format, string expected)
            => Assert.AreEqual(expected, string.Format(new BookFormatter(), "{0:" + format + "}", book));

        [TestCase("D4", 28, ExpectedResult = "0028")]
        [TestCase("", 0.3, ExpectedResult = "0.3")]
        public string BookFormatter_NonBooks_Tests(string format, dynamic value)
            => string.Format(new BookFormatter(), "{0:" + format + "}", value);

        [TestCaseSource(nameof(Exceptions_TestsData))]
        public void BookFormatter_Exceptions(string format)
            => Assert.Throws<FormatException>(() => string.Format(new BookFormatter(), "{0:" + format + "}", book));
    }
}

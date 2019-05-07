using System;

namespace UrlTextToXml
{
    /// <summary>
    /// Implements an <see cref="Interfaces.ILogger"/> by logging to the console.
    /// </summary>
    public class ConsoleLogger : Interfaces.ILogger
    {
        public void Debug(string message, Exception exception = null)
        {
            Console.WriteLine($"Debug:" + this.CreateLine(message, exception));
        }

        public void Error(string message, Exception exception = null)
        {
            Console.WriteLine($"Error:" + this.CreateLine(message, exception));
        }

        public void Fatal(string message, Exception exception = null)
        {
            Console.WriteLine($"Fatal:" + this.CreateLine(message, exception));
        }

        public void Info(string message, Exception exception = null)
        {
            Console.WriteLine($"Info:" + this.CreateLine(message, exception));
        }

        public void Trace(string message, Exception exception = null)
        {
            Console.WriteLine($"Trace:" + this.CreateLine(message, exception));
        }

        public void Warn(string message, Exception exception = null)
        {
            Console.WriteLine($"Warn:" + this.CreateLine(message, exception));
        }

        private string CreateLine(string message, Exception exception = null)
        {
            return $" {message}{((exception != null) ? "\n" + exception?.Message : string.Empty)}";
        }
    }
}

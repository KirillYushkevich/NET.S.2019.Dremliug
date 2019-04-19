using System;
using NLog;

namespace NET.S._2019.Dremliug._11
{
    public class NLogger : ILogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Trace(string message, Exception exception = null) => Logger.Trace(exception, message);

        public void Debug(string message, Exception exception = null) => Logger.Debug(exception, message);

        public void Info(string message, Exception exception = null) => Logger.Info(exception, message);

        public void Warn(string message, Exception exception = null) => Logger.Warn(exception, message);

        public void Error(string message, Exception exception = null) => Logger.Error(exception, message);

        public void Fatal(string message, Exception exception = null) => Logger.Fatal(exception, message);
    }
}

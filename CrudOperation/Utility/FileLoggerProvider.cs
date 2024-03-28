using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace CrudOperation.Utility
{


    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;

        public FileLoggerProvider(string filePath)
        {
            _filePath = filePath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath);
        }

        public void Dispose()
        {
            // Dispose any resources if needed
        }
    }

    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private readonly object _lock = new object();

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null; // Not implemented
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Implement your custom logic for log level filtering if needed
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock (_lock)
            {
                // Write log message to file
                using (var writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] [{logLevel}] {formatter(state, exception)}");
                }
            }
        }
    }

}

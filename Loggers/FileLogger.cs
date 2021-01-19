using System;
using System.IO;

namespace Lab_MP_AP.Loggers
{
    public class FileLogger : ILogger, IDisposable
    {
        private StreamWriter _writer;

        public FileLogger(string filePath)
        {
            _writer = new StreamWriter(filePath);
        }

        public void Log(string text)
        {
            _writer.WriteLine(text);
        }

        public void Dispose()
        {
            _writer.Close();
        }
    }
}

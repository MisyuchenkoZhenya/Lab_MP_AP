using System;
using System.IO;

namespace Lab_MP_AP.Loggers
{
    public class FileLogger : ILogger, IDisposable
    {
        private StreamWriter writer;

        public FileLogger(string filePath)
        {
            this.writer = new StreamWriter(filePath);
        }

        public void Log(string text)
        {
            writer.WriteLine(text);
        }

        public void Dispose()
        {
            writer.Close();
        }
    }
}

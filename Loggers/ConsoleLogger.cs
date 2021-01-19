using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_MP_AP.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}

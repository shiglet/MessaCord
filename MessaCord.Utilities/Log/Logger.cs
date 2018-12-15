using System;
using System.Collections.Generic;
using System.Text;

namespace MessaCord.Utilities.Log
{
    public  class Logger : ILog
    {
        public Logger(bool debug)
        {
            this._debug = debug;
        }

        private bool _debug = false;
        public void Log(LogLevel logLevel, string log)
        {
            switch (logLevel)
            {
                case LogLevel.Default:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[Default] "+ log);
                    Console.ResetColor();
                    break;
                case LogLevel.Debug:
                    if (!_debug) break;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("[Common] " + log);
                    Console.ResetColor();
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Warning] " + log);
                    Console.ResetColor();
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("[Error] " + log);
                    Console.ResetColor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public void Log(string log)
        {
            Log(LogLevel.Default, log);
        }

        public void LogDebug(string log)
        {
            Log(LogLevel.Debug, log);
        }

        public void LogError(string log)
        {
            Log(LogLevel.Error, log);
        }
    }
}

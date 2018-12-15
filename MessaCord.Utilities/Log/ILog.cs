namespace MessaCord.Utilities.Log
{
    public interface ILog
    {
        void Log(LogLevel logLevel, string log);
        void Log(string log);
    }

    public enum LogLevel : int
    {
        Default = 0,
        Debug = 1,
        Warning = 2,
        Error = 3
    }

}
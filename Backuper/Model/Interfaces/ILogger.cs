namespace Backuper.Model.Interfaces;

public interface ILogger
{
    public LoggerLevel Level { get; set; }
    public void Log(string message, LoggerLevel loggerLevel);
}

public enum LoggerLevel
{
    Error,
    Info,
    Debug
}
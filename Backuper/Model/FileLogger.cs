using Backuper.Model.Interfaces;
using System;
using System.IO;
using System.Windows;

namespace Backuper.Model;

public class FileLogger : ILogger
{
    public LoggerLevel Level { get; set; }

    private string logPath;

    public FileLogger(LoggerLevel level)
    {
        Level = level;
        CreateLogFile();
    }

    public void Log(string message, LoggerLevel loggerLevel)
    {
        if (Level >= loggerLevel)
        {
            message = $"({loggerLevel})\t{DateTime.Now.ToString("HH.mm.ss")}: {message}\n";
            File.AppendAllText(logPath, message);
        }
    }

    private void CreateLogFile()
    {
        string directoryPath = Path.Combine(
            System.AppDomain.CurrentDomain.BaseDirectory, "Log");

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        logPath = Path.Combine(directoryPath, DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")) + ".txt";
    }
}
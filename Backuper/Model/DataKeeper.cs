using Backuper.Model.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Backuper.Model;

public class DataKeeper
{
    public string SourceDirectory { get; set; }
    public string TargetDirectory { get; set; }
    public LoggerLevel LoggerLevel { get; set; }
}

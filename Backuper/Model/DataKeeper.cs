using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Backuper.Model;

public class DataKeeper : INotifyPropertyChanged
{
    public string SourceDirectory { get; set; }
    public string TargetDirectory { get; set; }
}

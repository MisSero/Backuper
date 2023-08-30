using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Backuper.Model;

public class DataKeeper : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string sourceDirectory;
    private string targetDirectory;

    public string SourceDirectory
    {
        get => sourceDirectory;
        set
        {
            sourceDirectory = value;
            OnPropertyChanged();
        }
    }
    public string TargetDirectory
    {
        get => targetDirectory;
        set
        {
            targetDirectory = value;
            OnPropertyChanged();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}

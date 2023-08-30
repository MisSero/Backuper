using System.ComponentModel;
using System.Runtime.CompilerServices;
using Backuper.Model;

namespace Backuper.ViewModel;

public class DataKeeperViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly DataKeeper _dataKeeper;

    public string SourceDirectory
    {
        get => _dataKeeper.SourceDirectory;
        set
        {
            _dataKeeper.SourceDirectory = value;
            OnPropertyChanged();
        }
    }
    public string TargetDirectory
    {
        get => _dataKeeper.TargetDirectory;
        set
        {
            _dataKeeper.TargetDirectory = value;
            OnPropertyChanged();
        }
    }
    public DataKeeperViewModel(DataKeeper dataKeeper)
    {
        _dataKeeper = dataKeeper;
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

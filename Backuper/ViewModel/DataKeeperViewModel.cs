using System.ComponentModel;
using System.Runtime.CompilerServices;
using Backuper.Model;
using Backuper.Model.Interfaces;

namespace Backuper.ViewModel;

public class DataKeeperViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly ILogger _logger;
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
    public int LoggerLevel
    {
        get => (int)_dataKeeper.LoggerLevel;
        set
        {
            _dataKeeper.LoggerLevel = (LoggerLevel)value;
            _logger.Level = (LoggerLevel)value;
            OnPropertyChanged();
        }
    }

    public DataKeeperViewModel(DataKeeper dataKeeper, ILogger logger)
    {
        _dataKeeper = dataKeeper;
        _logger = logger;
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}

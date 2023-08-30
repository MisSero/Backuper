using System;
using System.IO;
using System.Text.Json;

namespace Backuper.Model;

public class SaveModel
{
    public DataKeeper DataKeeper { get; private set; }

    private readonly string _savePath;
    private readonly string _fileName = "save.json";

    public SaveModel()
    {
        _savePath = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments) + @"\Backuper";

        Load();
    }

    public void Save()
    {
        string jsonData = JsonSerializer.Serialize(DataKeeper);

        string filePath = Path.Combine(_savePath, _fileName);
        File.WriteAllText(filePath, jsonData);
    }

    private void Load()
    {
        if (!Directory.Exists(_savePath))
            Directory.CreateDirectory(_savePath);

        string filePath = Path.Combine(_savePath, _fileName);
        if (File.Exists(filePath))
            DataKeeper = JsonSerializer.Deserialize<DataKeeper>(filePath);
        else
            DataKeeper = new DataKeeper();
    }
}

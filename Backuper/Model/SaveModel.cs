using Backuper.Model.Interfaces;
using System;
using System.IO;
using System.Text.Json;

namespace Backuper.Model;

public class SaveModel
{
    public DataKeeper DataKeeper { get; private set; }
    public ILogger Logger { get; private set; }

    private readonly string _savePath;
    private readonly string _fileName = "save.json";

    public SaveModel()
    {
        _savePath = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments) + @"\Backuper";

        Logger = new FileLogger(LoggerLevel.Error);
        Load();
    }

    public void Save()
    {
        string jsonData = JsonSerializer.Serialize(DataKeeper);

        string filePath = Path.Combine(_savePath, _fileName);
        File.WriteAllText(filePath, jsonData);

        Logger.Log("Сохранение прошло успешно", LoggerLevel.Debug);
    }

    private void Load()
    {
        if (!Directory.Exists(_savePath))
            Directory.CreateDirectory(_savePath);

        string filePath = Path.Combine(_savePath, _fileName);
        if (File.Exists(filePath))
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                DataKeeper = JsonSerializer.Deserialize<DataKeeper>(jsonData);
                Logger.Level = DataKeeper.LoggerLevel;

                Logger.Log("Загрузка прошла успешно", LoggerLevel.Debug);
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка при десериализации save.json: {ex}", LoggerLevel.Error);
            }
        }
        else
            DataKeeper = new DataKeeper();

    }
}

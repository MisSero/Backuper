using System;
using System.IO;
using System.IO.Compression;


namespace Backuper.Model;

public class BackupModel
{
    public string SourceDirectory { get; set; }
    public string TargetDirectory { get; set; }

    public BackupModel(string sourceDirectory, string targetDirectory)
    {
        SourceDirectory = sourceDirectory;
        TargetDirectory = targetDirectory;
        Backup();
    }

    /// <summary>
    /// Создание zip архива в целевой папке на основе файлов из исходной
    /// </summary>
    public void Backup()
    {
        if (string.IsNullOrEmpty(SourceDirectory) || string.IsNullOrEmpty(TargetDirectory))
            return;

        string directoryName = new DirectoryInfo(SourceDirectory).Name;
        string targetDirectory = Path.Combine(TargetDirectory, $"{directoryName} " +
            DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")) + ".zip";

        if (!Directory.Exists(SourceDirectory))
        {
            Console.WriteLine("Исходная папка не существует.");
            return;
        }

        if (Directory.Exists(targetDirectory))
            return;

        // Создаём zip архив с добавлением в него всех файлов из исходной папки
        using (var zip = ZipFile.Open(targetDirectory, ZipArchiveMode.Create))
        {
            foreach (string filePath in Directory.GetFiles(SourceDirectory, "*", SearchOption.AllDirectories))
            {
                string relativePath = filePath.Substring(SourceDirectory.Length + 1);
                zip.CreateEntryFromFile(filePath, relativePath);
                Console.WriteLine($"Файл {relativePath} добавлен в архив.");
            }
        }
    }
}

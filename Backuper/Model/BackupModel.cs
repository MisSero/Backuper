using System;
using System.IO;
using System.IO.Compression;


namespace Backuper.Model;

public static class BackupModel
{
    /// <summary>
    /// Создание zip архива в целевой папке на основе файлов из исходной
    /// </summary>
    public static void Backup(string sourceDirectory, string targetDirectory)
    {
        if (string.IsNullOrEmpty(sourceDirectory) || string.IsNullOrEmpty(targetDirectory))
            return;

        string directoryName = new DirectoryInfo(sourceDirectory).Name;
        targetDirectory = Path.Combine(targetDirectory, $"{directoryName} " +
            DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")) + ".zip";

        if (!Directory.Exists(sourceDirectory))
        {
            Console.WriteLine("Исходная папка не существует.");
            return;
        }

        if (Directory.Exists(targetDirectory))
            return;

        // Создаём zip архив с добавлением в него всех файлов из исходной папки
        using (var zip = ZipFile.Open(targetDirectory, ZipArchiveMode.Create))
        {
            foreach (string filePath in Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                string relativePath = filePath.Substring(sourceDirectory.Length + 1);
                zip.CreateEntryFromFile(filePath, relativePath);
                Console.WriteLine($"Файл {relativePath} добавлен в архив.");
            }
        }
    }
}

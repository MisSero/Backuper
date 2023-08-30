using Backuper.Model.Interfaces;
using System;
using System.IO;
using System.IO.Compression;


namespace Backuper.Model;

public static class BackupModel
{

    /// <summary>
    /// Создание zip архива в целевой папке на основе файлов из исходной
    /// </summary>
    public static void Backup(string sourceDirectory, string targetDirectory, ILogger logger)
    {
        if (string.IsNullOrEmpty(sourceDirectory) || string.IsNullOrEmpty(targetDirectory))
        {
            logger.Log("Ошибка резервного копирования, папка не установлена", LoggerLevel.Info);
            return;
        }

        string directoryName = new DirectoryInfo(sourceDirectory).Name;
        targetDirectory = Path.Combine(targetDirectory, $"{directoryName} " +
            DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss")) + ".zip";

        if (!Directory.Exists(sourceDirectory))
        {
            logger.Log("Исходная папка не существует", LoggerLevel.Info);
            return;
        }

        if (Directory.Exists(targetDirectory))
        {
            logger.Log("Ошибка резервного копирования, архив назначения уже существует", LoggerLevel.Info);
            return;
        }    

        logger.Log("Резервное копирование исходной папки началось", LoggerLevel.Info);

        // Создаём zip архив с добавлением в него всех файлов из исходной папки
        try
        {
            using (var zip = ZipFile.Open(targetDirectory, ZipArchiveMode.Create))
            {
                foreach (string filePath in Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories))
                {
                    string relativePath = filePath.Substring(sourceDirectory.Length + 1);
                    zip.CreateEntryFromFile(filePath, relativePath);

                    logger.Log($"Файл {relativePath} добавлен в архив", LoggerLevel.Debug);
                }
            }

            logger.Log("Резервное копирование исходной папки завершено", LoggerLevel.Info);
        }
        catch (Exception ex)
        {
            logger.Log($"Ошибка при архивировании: {ex}", LoggerLevel.Error);
        }
    }
}

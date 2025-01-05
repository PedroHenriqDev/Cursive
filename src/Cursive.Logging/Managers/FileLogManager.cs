using System.Text.RegularExpressions;
using Cursive.Logging.Configurations;
using Microsoft.Extensions.Configuration;
using Cursive.Logging.Managers.Interfaces;

namespace Cursive.Logging.Managers;

public class FileLogManager : IFileLogManager
{
    public FileLogConfiguration FileLogConfiguration { get; set; }


    public FileLogManager(IConfiguration configuration)
    {
        string filelogName = configuration["fileLogging:fileLogName"] ?? string.Empty;
        string fileLogPath = configuration["fileLogging:fileLogPath"] ?? string.Empty;
        string fileLogDirectory = configuration["fileLogging:fileLogDirectory"] ?? string.Empty;
        bool oneFileToExecution = Convert.ToBoolean(configuration["fileLogging:oneFileToExecution"]);

        FileLogConfiguration = new FileLogConfiguration(oneFileToExecution, filelogName, fileLogPath, fileLogDirectory);
        CreateLogFile();
    }

    private string GetPathDirectory() => Path.Combine(FileLogConfiguration.FileLogRelativePath, FileLogConfiguration.FileLogDirectory);

    private FileInfo? GetLastFile()
    {
        string[] logFiles = Directory.GetFiles(GetPathDirectory());

        if (logFiles.Length == 0)
            return null;

        return logFiles.Select(f => new FileInfo(f)).OrderByDescending(f => f.CreationTime).FirstOrDefault();
    }

    public void CreateLogFile()
    {
        string pathDirectory = GetPathDirectory();
        if (!Directory.Exists(pathDirectory))
        {
            Directory.CreateDirectory(pathDirectory);
        }

        if (FileLogConfiguration.OneFileToExecution)
        {
            CreateIncrementalFile();
        }
        else
        {
            File.Create($"{FileLogConfiguration.FileLogFullPath}.{FileLogConfiguration.FILE_LOG_EXTENSION}");
        }
    }

    public void CreateIncrementalFile()
    {
        FileInfo? lastLogFile = GetLastFile();

        if (lastLogFile == null || !Regex.IsMatch(lastLogFile!.Name, @$"_\d+\.{FileLogConfiguration.FILE_LOG_EXTENSION}$"))
        {
            FileLogConfiguration.UpdateLogFileName($"{FileLogConfiguration.FileLogName}_1.{FileLogConfiguration.FILE_LOG_EXTENSION}");
            using (FileStream fileStream = File.Create(FileLogConfiguration.FileLogFullPath))
            {
               fileStream.DisposeAsync();
            }
        }
        else
        {
            string[] itemsLogFile = lastLogFile.Name.Replace($".{FileLogConfiguration.FILE_LOG_EXTENSION}", "").Split("_");
            int fileLogNumber = int.Parse(itemsLogFile[itemsLogFile.Length - 1]) + 1;
            FileLogConfiguration.UpdateLogFileName($"{FileLogConfiguration.FileLogName}_{fileLogNumber}.{FileLogConfiguration.FILE_LOG_EXTENSION}");
            using (FileStream fileStream = File.Create(FileLogConfiguration.FileLogFullPath))
            {
               fileStream.DisposeAsync();
            }
        }
    }

    public async Task WriteInLogFileAsync(string message)
    {
        using(var sw = new StreamWriter(FileLogConfiguration.FileLogFullPath))
        {
            await sw.WriteLineAsync(message);
        }
    }
}

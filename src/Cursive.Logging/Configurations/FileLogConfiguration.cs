namespace Cursive.Logging.Configurations;

public sealed class FileLogConfiguration
{
    public FileLogConfiguration(bool oneFileToExecution, string fileLogName,  string fileLogRelativePath, string fileLogDirectory)
    {
        OneFileToExecution = oneFileToExecution;
        FileLogName = fileLogName.Replace($".{FILE_LOG_EXTENSION}", "", StringComparison.InvariantCultureIgnoreCase);
        FileLogRelativePath = fileLogRelativePath;
        FileLogDirectory = fileLogDirectory;

        MountFullPath();
    }

    private void MountFullPath() => FileLogFullPath = Path.Combine(FileLogRelativePath, FileLogDirectory, FileLogName);

    public void UpdateLogFileName(string newLogFileName)
    {
        FileLogName = newLogFileName;
        FileLogFullPath = Path.Combine(FileLogRelativePath, FileLogDirectory, FileLogName);
    }

    public string FileLogName { get; private set; } = string.Empty;
    public string FileLogRelativePath { get; private set; } = string.Empty;
    public string FileLogFullPath { get; private set; } = string.Empty;
    public string FileLogDirectory { get; private set; } = string.Empty;
    public bool OneFileToExecution { get; private set; }
    public const string FILE_LOG_EXTENSION = "txt";
}

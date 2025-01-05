namespace Cursive.Logging.Managers.Interfaces;

public interface IFileLogManager
{
    void CreateLogFile();
    void CreateIncrementalFile();
    Task WriteInLogFileAsync(string message);
}

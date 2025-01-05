using System.Runtime.CompilerServices;
using System.Text;
using Cursive.Logging.Loggers.Interfaces;
using Cursive.Logging.Managers.Interfaces;

namespace Cursive.Logging.Loggers;

public class FileLogger<T> : IFileLogger<T> where T : class
{
    private readonly IFileLogManager _fileLogManager;
    public LoggerContext LoggerContext { get; private set; }

    public FileLogger(IFileLogManager fileLogManager, string method = "")
    {
        _fileLogManager = fileLogManager;
        LoggerContext = new LoggerContext(typeof(T).Name, method);
    }

    public async Task LogInfoAsync(string message, [CallerMemberName] string method = "", string @class = "")
    {
        await _fileLogManager.WriteInLogFileAsync(MountMessage("Info", method, @class, message));
    }

    public async Task LogInfosAsync(IEnumerable<string> messages, [CallerMemberName] string method = "", string @class = "" )
    {
         foreach (string message in messages)
           await _fileLogManager.WriteInLogFileAsync(MountMessage("Info", method, @class, message));
    }

    public async Task LogWarningAsync(string message, [CallerMemberName] string method = "", string @class = "")
    {
        await _fileLogManager.WriteInLogFileAsync(MountMessage("Warning", method, @class, message));
    }

    public async Task LogWarningsAsync(IEnumerable<string> messages, [CallerMemberName] string method = "", string @class = "")
    {
        foreach (string message in messages)
            await _fileLogManager.WriteInLogFileAsync(MountMessage("Warning", method, @class, message));
    }

    public async Task LogErrorAsync(string message, [CallerMemberName] string method = "", string @class = "")
    {
        await _fileLogManager.WriteInLogFileAsync(MountMessage("Error", method, @class, message));
    }

    public async Task LogErrorsAsync(IEnumerable<string> messages, [CallerMemberName] string method = "", string @class = "")
    {
        foreach(string message in messages)
         await _fileLogManager.WriteInLogFileAsync(MountMessage("Error", method, @class, message));
    }

    private string MountMessage(string type,string method, string @class, string message)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"{type} - {DateTime.Now} -> ");

        if (string.IsNullOrEmpty(method) && !string.IsNullOrEmpty(LoggerContext.Method))
            method = LoggerContext.Method;

        if (string.IsNullOrEmpty(@class) && !string.IsNullOrEmpty(LoggerContext.Class))
            @class = LoggerContext.Class;

        if (!string.IsNullOrEmpty(@class) && LoggerContext.UseContext)
            sb.Append($"in class: {@class} -> ");

        if (!string.IsNullOrEmpty(method) && LoggerContext.UseContext)
            sb.Append($"in method: {method} -> ");

        if (!string.IsNullOrEmpty(message))
            sb.Append(message);

        return sb.ToString();
    }
}

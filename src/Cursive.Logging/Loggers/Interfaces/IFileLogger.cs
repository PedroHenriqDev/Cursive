namespace Cursive.Logging.Loggers.Interfaces
{
    public interface IFileLogger<T> where T : class
    {
        LoggerContext LoggerContext { get; }
        Task LogInfoAsync(string message, string method = "", string @class = "");
        Task LogInfosAsync(IEnumerable<string> messages, string method = "", string @class = "");
        Task LogWarningAsync(string message, string method = "", string @class = "");
        Task LogWarningsAsync(IEnumerable<string> messages, string method = "", string @class = "");
        Task LogErrorAsync(string message, string method = "", string @class = "");
        Task LogErrorsAsync(IEnumerable<string> messages, string method = "", string @class = "");
    }
}

namespace Cursive.Logging.Exceptions;

public class LogFileNotExistsException : Exception
{
    public LogFileNotExistsException(string message) : base(message)
    {
    }
}

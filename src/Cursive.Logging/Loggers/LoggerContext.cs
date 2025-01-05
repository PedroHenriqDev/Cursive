namespace Cursive.Logging.Loggers
{
    public class LoggerContext
    {
        public LoggerContext(string @class, string method = "", bool useContext = true)
        {
            Method = method;
            @Class = @class;
            UseContext = useContext;
        }

        public string Method { get; set; }
        public string @Class { get; set; }
        public bool UseContext { get; set; }
    }
}

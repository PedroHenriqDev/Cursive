namespace Cursive.Domain.Validations
{
    public class ValidationMessage
    {
        public string Property { get; private set; }
        public string Message { get; private set; }

        public ValidationMessage(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public ValidationMessage()
        {
            Property = string.Empty;
            Message = string.Empty;
        }

        public override string ToString()
        {
            return $"{Property} - {Message}";
        }
    }
}

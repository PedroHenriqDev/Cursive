namespace Cursive.Domain.Validations;

public class Validation
{
    public bool IsValid { get; private set; }
    public IList<ValidationMessage> Messages { get; private set; }

    public Validation(bool isValid, IList<ValidationMessage> messages)
    {
        IsValid = isValid;
        Messages = messages;
    }
}

using Cursive.Domain.Entities.Abstractions;
using Cursive.Domain.Enums;
using Cursive.Domain.Validations;

namespace Cursive.Domain.Entities;

public class Document : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public EDocumentType Type { get; set; } = EDocumentType.Text;
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public override Validation Validate()
    {
        IList<ValidationMessage> validationMessages = new List<ValidationMessage>();

        if (string.IsNullOrEmpty(Title))
            validationMessages.Add(new ValidationMessage(nameof(Title), string.Format(Messages.NULL_OR_EMPTY_STRING, nameof(Title))));

        if (string.IsNullOrEmpty(Text))
            validationMessages.Add(new ValidationMessage(nameof(Text), string.Format(Messages.NULL_OR_EMPTY_STRING, nameof(Text))));

        if (UserId == Guid.Empty)
            validationMessages.Add(new ValidationMessage(nameof(UserId), string.Format(Messages.NULL_OR_EMPTY_STRING, nameof(UserId))));

        return new Validation(!validationMessages.Any(), validationMessages);
    }
}

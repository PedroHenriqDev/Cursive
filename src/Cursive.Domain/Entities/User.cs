using Cursive.Domain.Entities.Abstractions;
using Cursive.Domain.Utils;
using Cursive.Domain.Validations;
using Cursive.Domain.ValueObjects;

namespace Cursive.Domain.Entities;

public class User : Entity
{
    public User()
    {
    }

    public User(Name name, string email, string password, DateTime birthDate) : base(GuidUtils.GenerateId(), DateTime.Now)
    {
        Name = name;
        Email = email;
        Password = password;
        BirthDate = birthDate;
        Salt = GuidUtils.GenerateId().ToString();
    }

    public Name Name { get; set; } = new Name();
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set;} = string.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<Document> Documents { get; set; } = new List<Document>();

    public override Validation Validate()
    {
        IList<ValidationMessage> validationMessages = new List<ValidationMessage>();

        if (string.IsNullOrEmpty(Name.FirstName) || string.IsNullOrEmpty(Name.LastName))
            validationMessages.Add(new ValidationMessage(nameof(Name), string.Format(Messages.NULL_OR_EMPTY_STRING, nameof(Name))));

        if (string.IsNullOrEmpty(Email))
            validationMessages.Add(new ValidationMessage(nameof(Email), string.Format(Messages.NULL_OR_EMPTY_STRING, nameof(Email))));

        if(BirthDate == DateTime.MinValue || BirthDate > DateTime.Now)
            validationMessages.Add(new ValidationMessage(nameof(Email), Messages.GREATER_DATE));

        return new Validation(!validationMessages.Any(), validationMessages);
    }
}

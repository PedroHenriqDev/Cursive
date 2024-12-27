using Cursive.Domain.ValueObjects;

namespace Cursive.Application.Dtos.User.Requests;

public class UserRequest
{
    public UserRequest(Name name, string email, DateTime createdAt)
    {
        FirstName = name.FirstName;
        LastName = name.LastName;
        Email = email;
        BirthDate = createdAt;
    }

    public UserRequest(string firstName, string lastName, string email, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        BirthDate = birthDate;
    }

    public UserRequest()
    {
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}

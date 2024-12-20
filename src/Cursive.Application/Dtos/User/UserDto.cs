using Cursive.Domain.ValueObjects;

namespace Cursive.API.Dtos.User;

public class UserDto
{
    public UserDto(Guid id, Name name, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = name.FirstName;
        LastName = name.LastName;
        Email = email;
        CreatedAt = createdAt;
    }

    public UserDto(Guid id, string firstName, string lastName, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CreatedAt = createdAt;
    }

    public UserDto()
    {
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

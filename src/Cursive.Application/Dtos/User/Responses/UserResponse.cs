namespace Cursive.Application.Dtos.User.Responses;

public class UserResponse
{
    public UserResponse()
    {
        FirstName = string.Empty; 
        LastName = string.Empty;
        Email = string.Empty;
    }

    public UserResponse(Guid id, string firstName, string lastName, string email, DateTime createdAt, DateTime birthDate)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CreatedAt = createdAt;
        BirthDate = birthDate;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime BirthDate { get; set; }
}

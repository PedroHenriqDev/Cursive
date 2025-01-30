namespace Cursive.Communication.Dtos.User.Requests;

public class LoginRequest
{
    public LoginRequest()
    {
    }

    public LoginRequest(string email, string password, string recaptchaToken)
    {
        Email = email;
        Password = password;
        ReCaptchaToken = recaptchaToken;
    }

    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ReCaptchaToken { get; set; } = string.Empty;
}

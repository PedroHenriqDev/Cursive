namespace Cursive.Communication.Dtos.Responses;

public class ReCaptchaResponse
{
    public bool Success { get; set; }
    public double Score { get; set; }
    public string Action { get; set; } = string.Empty;
}

namespace Cursive.Communication.Dtos.Responses;

public class TokenResponse
{
    public TokenResponse()
    {
    }

    public TokenResponse(string token, DateTime expireTime)
    {
        Token = token;
        ExpireTime = expireTime;
    }

    public string Token { get; set; } = string.Empty;
    public DateTime ExpireTime { get; set; }
}

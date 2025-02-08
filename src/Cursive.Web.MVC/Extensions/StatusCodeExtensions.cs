namespace Cursive.Web.MVC.Extensions;

public static class StatusCodeExtensions
{
    public static bool IsSuccessStatusCode(this int statusCode)
    {
        return statusCode > 200 && statusCode < 400;
    }
}

using Microsoft.AspNetCore.Http;

namespace Cursive.Web.HttpCore.Helpers;

public static class HttpHelper
{
    public static string GetCookie<T>(this HttpContext httpContext, string key)
    {
        string cookie = string.Empty;

        if (httpContext.Request.Cookies.ContainsKey(key))
        {
            cookie = httpContext.Request.Cookies[key] ?? string.Empty;
        }
        return cookie;
    }

    public static bool TryGetCookie(this HttpContext httpContext, string key, out string value)
    {
        bool result = false;
        value = string.Empty;

        if (httpContext.Request.Cookies.ContainsKey(key))
        {
            value = httpContext.Request.Cookies["key"];

            result = true;
        }

        return result;
    }

    public static void SetCookie(this HttpContext httpContext, string key, string value, CookieOptions cookieOptions)
    {
        httpContext.Response.Cookies.Append(key, value, cookieOptions);
    }
}

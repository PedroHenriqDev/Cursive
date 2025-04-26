using System.Globalization;
using System.Net.Mail;
using Cursive.Application.Services;
using Cursive.Application.Services.Interfaces;
using Cursive.Web.MVC.Services;
using Cursive.Web.MVC.Services.Interfaces;
using FluentEmail.Core;
using FluentEmail.Smtp;

namespace Cursive.Web.MVC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection app, IConfiguration configuration)
    {
        app.AddScoped<IAuthenticationService, AuthenticationService>();
        app.AddScoped<ITokenService, TokenService>();
        app.AddScoped<IEmailService, EmailService>();
        app.AddSingleton(configuration);
        return app;
    }

    public static IServiceCollection AddCookieAuth(this IServiceCollection app, IConfiguration configuration)
    {
        string? cookieName = configuration["Keys:TokenCookieKey"];

        app.AddAuthentication("CookieAuth").AddCookie("CookieAuth", opt =>
        {
            opt.Cookie.Name = cookieName;
            opt.LoginPath = "/User/Login";
            opt.AccessDeniedPath = "/User/Login";
            opt.Cookie.HttpOnly = true;
            opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            opt.Cookie.SameSite = SameSiteMode.Lax;
        });

        return app;
    }

    public static IServiceCollection AddSmtpServer(this IServiceCollection app, IConfiguration configuration)
    {
        string host = configuration["FluentEmail:Host"]?.ToString() ?? string.Empty;
        string dirPath = configuration["FluentEmail:Path"]?.ToString() ?? string.Empty;

        Email.DefaultSender = new SmtpSender(() => new SmtpClient(host) 
        {
            EnableSsl = false,
            DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
            PickupDirectoryLocation = dirPath
        });

        return app;
    }

    public static IServiceCollection AddEmailService(this IServiceCollection app, IConfiguration configuration)
    {
        //string from = configuration["FluentEmail:From"]?.ToString() ?? string.Empty;
        //string server = configuration["FluentEmail:SmtpServer"]?.ToString() ?? string.Empty;
        //int port = int.Parse(configuration["FluentEmail:Port"]!.ToString());

        //app.AddFluentEmail(from)
        //    .AddSmtpSender(server, port);

        return app;
    }
}

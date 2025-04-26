using Cursive.Shared.Cors;
using Cursive.Shared.IoC;
using Cursive.Web.MVC.Extensions;
using FluentEmail.Core;
using FluentEmail.Smtp;

namespace Cursive.Web.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddWebClients(builder.Configuration);
        builder.Services.AddWebServices(builder.Configuration);
        builder.Services.AddDefaultCorsAllowAll();
        builder.Services.AddSmtpServer(builder.Configuration);
        builder.Services.AddEmailService(builder.Configuration);
        builder.Services.AddCookieAuth(builder.Configuration);

        WebApplication app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
     
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Document}/{action=Index}/{id?}");

        app.Run();
    }
}

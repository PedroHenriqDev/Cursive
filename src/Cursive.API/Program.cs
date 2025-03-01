using Cursive.API.Extensions;
using Cursive.API.Middlewares;
using Cursive.Shared.IoC;
using Cursive.Shared.Cors;

namespace Cursive.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddJwtAuthentication(builder.Configuration);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSqlServer(builder.Configuration);
        builder.Services.AddUnitOfWork();
        builder.Services.AddServices();
        builder.Services.AddActionFilters();
        builder.Services.AddFileLogger(builder.Configuration);
        builder.Services.AddSingleton<GlobalExceptionMiddleware>();
        builder.Services.AddDefaultCorsAllowAll();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseGlobalExceptionMiddleware();
        app.MapControllers();

        app.Run();
    }
}
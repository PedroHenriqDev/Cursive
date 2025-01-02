using Cursive.API.Middlewares;
using Cursive.Shared.IoC; 

namespace Cursive.API;

public class Program
{
    public static void Main(string[] args)
  {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSqlServer(builder.Configuration);
        builder.Services.AddUnitOfWork();
        builder.Services.AddServices();
        builder.Services.AddSingleton<GlobalExceptionMiddleware>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.Run();
    }
}
using Cursive.Application.Services;
using Cursive.Application.Services.Interfaces;
using Cursive.Infra.Data;
using Cursive.Infra.UnitOfWork;
using Cursive.Infra.UnitOfWork.Interfaces;
using Cursive.Logging.Loggers;
using Cursive.Logging.Loggers.Interfaces;
using Cursive.Logging.Managers;
using Cursive.Logging.Managers.Interfaces;
using Cursive.Web.HttpCore.Interfaces;
using Cursive.Web.HttpCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cursive.Shared.IoC;

public static class DependencyResolver
{
    public static IServiceCollection AddSqlServer(this IServiceCollection serviceCollection, IConfiguration configuration) 
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        serviceCollection.AddDbContext<CursiveDbContext>(opt => opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("Cursive.Infra")));
        return serviceCollection;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection) 
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ICryptoService, CryptoService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IClaimService, ClaimService>();

        return serviceCollection;
    }

    public static IServiceCollection AddWebClients(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IUserClient>(opt => new UserClient(configuration));
        serviceCollection.AddScoped<IReCaptchaClient, ReCaptchaClient>();

        return serviceCollection;
    }

    public static IServiceCollection AddFileLogger(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IFileLogManager>(new FileLogManager(configuration));

        serviceCollection.AddScoped(typeof(IFileLogger<>), typeof(FileLogger<>));
     
        return serviceCollection;
    }
}

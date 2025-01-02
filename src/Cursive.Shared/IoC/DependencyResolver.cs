using Cursive.Application.Services;
using Cursive.Application.Services.Interfaces;
using Cursive.Infra.Data;
using Cursive.Infra.UnitOfWork;
using Cursive.Infra.UnitOfWork.Interfaces;
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
        return serviceCollection;
    }
}

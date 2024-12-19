using Cursive.Infra.Data;
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
}

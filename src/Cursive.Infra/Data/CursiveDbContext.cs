using Cursive.Domain.Entities;
using Cursive.Infra.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Cursive.Infra.Data;

public sealed class CursiveDbContext : DbContext
{
    public DbSet<User> Users { get; private set; }
    public DbSet<Document> Document { get; private set; }

    public CursiveDbContext(DbContextOptions<CursiveDbContext> dbContext) : base(dbContext) 
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}

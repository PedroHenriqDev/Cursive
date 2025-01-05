using Cursive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cursive.Infra.EntitiesConfiguration
{
    public sealed class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
                name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            });

            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        }
    }
}

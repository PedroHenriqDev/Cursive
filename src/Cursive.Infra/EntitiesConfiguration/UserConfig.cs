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
                name.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(100);
                name.Property(n => n.LastName).IsRequired().HasMaxLength(100);
            });

            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        }
    }
}

using Cursive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cursive.Infra.EntitiesConfiguration;

public class DocumentConfig : IEntityTypeConfiguration<Document>
{

    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(d => d.Text)
            .HasMaxLength(6000);

        builder.Property(d => d.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(d => d.Type)
            .HasConversion<int>()
            .IsRequired();
    }
}

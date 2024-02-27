using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class HostTypeConfiguration : IEntityTypeConfiguration<HostType>
{
    public void Configure(EntityTypeBuilder<HostType> builder)
    {
        builder.HasKey(ht => ht.Id);

        builder.HasIndex(ht => ht.Name)
            .IsUnique();

        builder.HasMany(ht => ht.Hosts)
            .WithOne(h => h.HostType)
            .HasForeignKey(h => h.HostTypeId);
    }
}
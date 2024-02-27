using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .HasMaxLength(DataSchemeConstants.DefaultTitleLength);

        builder.HasMany(h => h.HostedEvents)
            .WithOne(he => he.Host)
            .HasForeignKey(he => he.HostId);

        builder.HasOne(h => h.Details)
            .WithOne()
            .HasForeignKey<HostDetails>(hd => hd.Id);
    }
}
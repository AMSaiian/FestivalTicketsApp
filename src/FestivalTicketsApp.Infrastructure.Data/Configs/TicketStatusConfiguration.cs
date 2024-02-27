using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
{
    public void Configure(EntityTypeBuilder<TicketStatus> builder)
    {
        builder.HasKey(ts => ts.Id);

        builder.HasIndex(ts => ts.Status)
            .IsUnique();

        builder.HasMany(ts => ts.TicketsWithStatus)
            .WithOne(t => t.TicketStatus)
            .HasForeignKey(t => t.TicketStatusId);
    }
}
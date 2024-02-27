using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class EventStatusConfiguration : IEntityTypeConfiguration<EventStatus>
{
    public void Configure(EntityTypeBuilder<EventStatus> builder)
    {
        builder.HasKey(es => es.Id);

        builder.HasIndex(es => es.Status)
            .IsUnique();

        builder.HasMany(es => es.EventsWithStatus)
            .WithOne(e => e.EventStatus)
            .HasForeignKey(e => e.EventStatusId);
    }
}
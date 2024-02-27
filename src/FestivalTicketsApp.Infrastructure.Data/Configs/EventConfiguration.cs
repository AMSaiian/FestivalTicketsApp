using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(DataSchemeConstants.DefaultTitleLength);

        builder.HasMany(e => e.TicketTypes)
            .WithOne(tt => tt.Event)
            .HasForeignKey(tt => tt.EventId);

        builder.HasOne(e => e.EventDetails)
            .WithOne()
            .HasForeignKey<EventDetails>(ed => ed.Id);
    }
}
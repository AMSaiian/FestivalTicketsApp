using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class EventTypeConfiguration : IEntityTypeConfiguration<EventType>
{
    public void Configure(EntityTypeBuilder<EventType> builder)
    {
        builder.HasKey(et => et.Id);
        
        builder.HasIndex(et => et.Name)
            .IsUnique();

        builder.HasMany(et => et.EventTypeGenres)
            .WithOne(e => e.EventType)
            .HasForeignKey(e => e.EventTypeId);
    }
}
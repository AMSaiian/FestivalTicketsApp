using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class EventGenreConfiguration : IEntityTypeConfiguration<EventGenre>
{
    public void Configure(EntityTypeBuilder<EventGenre> builder)
    {
        builder.HasKey(eg => eg.Id);

        builder.HasIndex(eg =>
                new { eg.Genre, eg.EventTypeId })
            .IsUnique();

        builder.HasMany(eg => eg.EventsWithGenre)
            .WithOne(e => e.EventGenre)
            .HasForeignKey(e => e.EventGenreId);
    }
}
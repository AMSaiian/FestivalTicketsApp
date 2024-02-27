using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class TypeGenreConfiguration : IEntityTypeConfiguration<TypeGenre>
{
    public void Configure(EntityTypeBuilder<TypeGenre> builder)
    {
        builder.HasKey(tg => tg.Id);

        builder.HasIndex(tg =>
                new { tg.Genre, tg.EventTypeId })
            .IsUnique();
    }
}
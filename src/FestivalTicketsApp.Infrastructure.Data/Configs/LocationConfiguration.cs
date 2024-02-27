using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.CityName)
            .HasMaxLength(DataSchemeConstants.DefaultAddressLength);
        
        builder.Property(l => l.StreetName)
            .HasMaxLength(DataSchemeConstants.DefaultAddressLength);
        
        builder.Property(l => l.BuildingNumber)
            .HasMaxLength(DataSchemeConstants.DefaultAddressLength);

        builder.HasIndex(l =>
                new { l.CityName, l.StreetName, l.BuildingNumber })
            .IsUnique();

        builder.HasMany(l => l.Hosts)
            .WithOne(h => h.Location)
            .HasForeignKey(h => h.LocationId);
    }
}
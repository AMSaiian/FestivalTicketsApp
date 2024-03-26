using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Name)
            .HasMaxLength(DataSchemeConstants.DefaultNameLength);

        builder.Property(u => u.Surname)
            .HasMaxLength(DataSchemeConstants.DefaultNameLength);
        
        builder.HasIndex(u => u.Email)
            .IsUnique();
        builder.Property(u => u.Email)
            .IsRequired();

        builder.HasMany(u => u.FavouriteEvents)
            .WithMany(e => e.AddedToFavouriteBy);

        builder.HasMany(u => u.PurchasedTickets)
            .WithOne(t => t.Client)
            .HasForeignKey(t => t.ClientId)
            .IsRequired(false);
    }
}
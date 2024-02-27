using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasKey(tt => tt.Id);

        builder.ToTable(tt => 
            tt.HasCheckConstraint("CK_price", "[Price] > 0"));

        builder.HasIndex(tt =>
                new { tt.Name, tt.EventId })
            .IsUnique();

        builder.HasMany(tt => tt.TicketsWithType)
            .WithOne(t => t.TicketType)
            .HasForeignKey(t => t.TicketTypeId)
            .IsRequired();
    }
}
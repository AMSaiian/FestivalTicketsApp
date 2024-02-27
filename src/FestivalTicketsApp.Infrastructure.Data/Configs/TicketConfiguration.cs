using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_rowNum", "[RowNum] > 0");
            t.HasCheckConstraint("CK_seatNum", "[SeatNum] > 0");
        });
    }
}
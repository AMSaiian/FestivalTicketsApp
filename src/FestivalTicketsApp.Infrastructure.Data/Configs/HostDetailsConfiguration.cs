using FestivalTicketsApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FestivalTicketsApp.Infrastructure.Data.Configs;

public class HostDetailsConfiguration : IEntityTypeConfiguration<HostDetails>
{
    public void Configure(EntityTypeBuilder<HostDetails> builder)
    {
        builder.HasKey(hd => hd.Id);
        
        builder.ToTable(hd =>
        {
            hd.HasCheckConstraint("CK_rowAmount", "[RowAmount] > 0");
            hd.HasCheckConstraint("CK_seatsInRow", "[SeatsInRow] > 0");
        });
    }
}
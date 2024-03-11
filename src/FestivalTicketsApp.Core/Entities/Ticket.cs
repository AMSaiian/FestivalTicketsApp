namespace FestivalTicketsApp.Core.Entities;

public class Ticket : BaseEntity
{
    public int? RowNum { get; set; }
    
    public int? SeatNum { get; set; }
    
    public int TicketTypeId { get; set; }
    
    public int TicketStatusId { get; set; }
    
    public int? ClientId { get; set; }

    public TicketType TicketType { get; set; } = default!;

    public TicketStatus TicketStatus { get; set; } = default!;
    
    public Client? Client { get; set; }
}
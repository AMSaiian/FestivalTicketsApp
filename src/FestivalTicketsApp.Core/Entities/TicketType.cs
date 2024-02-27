namespace FestivalTicketsApp.Core.Entities;

public class TicketType : BaseEntity
{
    public string Name { get; set; } = default!;

    public decimal Price { get; set; }
    
    public int EventId { get; set; }

    public Event Event { get; set; } = default!;
    
    public List<Ticket> TicketsWithType { get; set; } = [];
}
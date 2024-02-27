namespace FestivalTicketsApp.Core.Entities;

public class Event : BaseEntity
{
    public string Title { get; set; } = default!;
    
    public int EventTypeId { get; set; }
    
    public int HostId { get; set; }

    public EventType EventType { get; set; } = default!;

    public Host Host { get; set; } = default!;
}
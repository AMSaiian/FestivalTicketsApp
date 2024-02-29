namespace FestivalTicketsApp.Core.Entities;

public class EventGenre : BaseEntity
{
    public string Genre { get; set; } = default!;
    
    public int EventTypeId { get; set; }

    public EventType EventType { get; set; } = default!;

    public List<Event> EventsWithGenre { get; set; } = [];
}
namespace FestivalTicketsApp.Core.Entities;

public class EventStatus : BaseEntity
{
    public string Status { get; set; } = default!;

    public List<Event> EventsWithStatus { get; set; } = [];
}
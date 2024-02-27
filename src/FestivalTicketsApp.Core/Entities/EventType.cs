namespace FestivalTicketsApp.Core.Entities;

public class EventType : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<Event> EventsWithType { get; set; } = [];

    public List<TypeGenre> EventTypeGenres { get; set; } = [];
}
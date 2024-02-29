namespace FestivalTicketsApp.Core.Entities;

public class EventType : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<EventGenre> EventTypeGenres { get; set; } = [];
}
namespace FestivalTicketsApp.Core.Entities;

public class TypeGenre : BaseEntity
{
    public string Genre { get; set; } = default!;
    
    public int EventTypeId { get; set; }

    public EventType EventType { get; set; } = default!;
}
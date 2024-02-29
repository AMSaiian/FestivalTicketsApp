namespace FestivalTicketsApp.Core.Entities;

public class EventDetails : BaseEntity
{
    public string Description { get; set; } = default!;

    public DateTime StartDate { get; set; }

    public int Duration { get; set; }
}
namespace FestivalTicketsApp.Core.Entities;

public class Event : BaseEntity
{
    public string Title { get; set; } = default!;
    
    public int EventGenreId { get; set; }
    
    public int HostId { get; set; }
    
    public int EventStatusId { get; set; }
    public EventDetails EventDetails { get; set; } = default!;

    public EventGenre EventGenre { get; set; } = default!;

    public EventStatus EventStatus { get; set; } = default!;

    public Host Host { get; set; } = default!;

    public List<TicketType> TicketTypes { get; set; } = [];

    public List<Client> AddedToFavouriteBy { get; set; } = [];
}
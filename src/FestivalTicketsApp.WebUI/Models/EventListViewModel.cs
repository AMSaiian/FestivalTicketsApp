using FestivalTicketsApp.Application.EventsService.DTO;
using FestivalTicketsApp.Application.HostsService.DTO;

namespace FestivalTicketsApp.WebUI.Models;

public class EventListViewModel
{
    public List<GenreDto>? Genres { get; set; }
    
    public List<string>? CityNames { get; set; }
    
    public List<EventDto>? Events { get; set; }

    public EventListFilterModel? Filter { get; set; } = new();
}
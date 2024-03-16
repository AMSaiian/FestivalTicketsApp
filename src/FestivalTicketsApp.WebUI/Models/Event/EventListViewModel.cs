using FestivalTicketsApp.Application.EventService.DTO;

namespace FestivalTicketsApp.WebUI.Models.Event;

public class EventListViewModel
{
    public List<GenreDto> Genres { get; set; }
    
    public List<string> CityNames { get; set; }
    
    public List<EventDto>? Events { get; set; }
    
    public EventListQuery QueryState { get; set; }
    
    public int CurrentPageNum { get; set; }
    
    public int NextPagesAmount { get; set; }
}
using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.HostService.DTO;

namespace FestivalTicketsApp.WebUI.Models.Event;

public class EventDetailsViewModel
{
    public EventWithDetailsDto Event { get; set; }
    
    public bool? IsInFavourite { get; set; }
    
    public List<HostedEventDto>? HostedEvents { get; set; }
}
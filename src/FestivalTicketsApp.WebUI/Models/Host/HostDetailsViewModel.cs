using FestivalTicketsApp.Application.HostService.DTO;

namespace FestivalTicketsApp.WebUI.Models.Host;

public class HostDetailsViewModel
{
    public HostWithDetailsDto Host { get; set; }
    
    public List<HostedEventDto>? HostedEvents { get; set; }
}
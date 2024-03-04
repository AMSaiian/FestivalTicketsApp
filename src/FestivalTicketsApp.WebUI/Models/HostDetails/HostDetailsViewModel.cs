using FestivalTicketsApp.Application.HostService.DTO;

namespace FestivalTicketsApp.WebUI.Models.HostDetails;

public class HostDetailsViewModel
{
    public HostWithDetailsDto? Host { get; set; }
    
    public List<HostedEventDto>? HostedEvents { get; set; }
}
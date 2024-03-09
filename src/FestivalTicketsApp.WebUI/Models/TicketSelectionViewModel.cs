using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.TicketService;

namespace FestivalTicketsApp.WebUI.Models;

public class TicketSelectionViewModel
{
    public List<TicketDto> Tickets { get; set; }
    
    public List<TicketTypeDto> TicketTypes { get; set; }
    
    public HostHallDetailsDto HallDetails { get; set; }
}
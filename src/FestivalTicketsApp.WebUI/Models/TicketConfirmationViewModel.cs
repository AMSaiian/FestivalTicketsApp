using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.TicketService;

namespace FestivalTicketsApp.WebUI.Models;

public class TicketConfirmationViewModel
{
    public EventDto Event { get; set; }
    
    public List<TicketWithPriceDto> SelectedTickets { get; set; }
}
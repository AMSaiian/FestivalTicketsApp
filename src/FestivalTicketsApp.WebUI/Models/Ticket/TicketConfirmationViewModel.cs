using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.TicketService.DTO;

namespace FestivalTicketsApp.WebUI.Models.Ticket;

public class TicketConfirmationViewModel
{
    public EventDto Event { get; set; }
    
    public List<TicketWithPriceDto> SelectedTickets { get; set; }
}
using FestivalTicketsApp.Application.TicketService.DTO;

namespace FestivalTicketsApp.Application.TicketService;

public interface ITicketService
{
    public Task<List<TicketDto>> GetEventTickets(int eventId);

    public Task<List<TicketTypeDto>> GetEventTicketTypes(int eventId);

    public Task<List<TicketWithPriceDto>> GetTicketsWithPriceById(List<int> ticketsId);
}
using FestivalTicketsApp.Application.TicketService.DTO;
using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.TicketService;

public interface ITicketService
{
    public Task<Result<List<TicketDto>>> GetEventTickets(int eventId);

    public Task<Result<List<TicketTypeDto>>> GetEventTicketTypes(int eventId);

    public Task<Result<List<TicketWithPriceDto>>> GetTicketsWithPriceById(List<int> ticketsId);

    public Task<Result<object>> ChangeEventTicketsStatus(string statusName, List<int> ticketsId, int? clientId);
}
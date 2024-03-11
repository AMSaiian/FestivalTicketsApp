using FestivalTicketsApp.Application.TicketService.DTO;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.TicketService;

public class TicketService(AppDbContext context) : ITicketService
{
    private readonly AppDbContext _context = context;

    public async Task<List<TicketDto>> GetEventTickets(int eventId)
    {
        bool isEventExist = await _context.Events.AnyAsync(e => e.Id == eventId);

        if (isEventExist)
        {
            IQueryable<Ticket> ticketsQuery = _context.Tickets
                .AsNoTracking()
                .Include(t => t.TicketType)
                .Include(t => t.TicketStatus);

            List<TicketDto> result = await ticketsQuery
                .Where(t => t.TicketType.EventId == eventId)
                .OrderBy(t => t.RowNum)
                .ThenBy(t => t.SeatNum)
                .Select(t => new TicketDto(
                    t.Id,
                    t.RowNum,
                    t.SeatNum,
                    t.TicketStatus.Status,
                    t.TicketType.Id))
                .ToListAsync();
            
            return result;
        }
        
        //future error handling
        return new List<TicketDto>();
    }

    public async Task<List<TicketWithPriceDto>> GetTicketsWithPriceById(List<int> ticketsId)
    {
        IQueryable<Ticket> ticketsQuery = _context.Tickets
            .AsNoTracking()
            .Include(t => t.TicketType);
        
        List<TicketWithPriceDto> result = await ticketsQuery
            .Where(t => ticketsId.Contains(t.Id))
            .OrderBy(t => t.RowNum)
            .ThenBy(t => t.SeatNum)
            .Select(t => new TicketWithPriceDto(
                t.Id,
                t.RowNum,
                t.SeatNum,
                t.TicketType.Name,
                t.TicketType.Price))
            .ToListAsync();
        
        // future error handling
        if (result.Count != ticketsId.Count)
            return new List<TicketWithPriceDto>();
        
        return result;
    }

    public async Task<List<TicketTypeDto>> GetEventTicketTypes(int eventId)
    {
        bool isEventExist = await _context.Events.AnyAsync(e => e.Id == eventId);

        if (isEventExist)
        {
            IQueryable<TicketType> ticketTypesQuery = _context.TicketTypes
                .AsNoTracking();

            List<TicketTypeDto> result = await ticketTypesQuery
                .Where(tt => tt.EventId == eventId)
                .OrderByDescending(tt => tt.Price)
                .Select(tt => new TicketTypeDto(
                    tt.Id,
                    tt.Name,
                    tt.Price))
                .ToListAsync();
            
            return result;
        }
        
        //future error handling
        return new List<TicketTypeDto>();
    }
}
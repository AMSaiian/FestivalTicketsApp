using FestivalTicketsApp.Application.TicketService.DTO;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using FestivalTicketsApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.TicketService;

public class TicketService(AppDbContext context) : ITicketService
{
    private readonly AppDbContext _context = context;

    public async Task<Result<List<TicketDto>>> GetEventTickets(int eventId)
    {
        bool isEventExist = await _context.Events.AnyAsync(e => e.Id == eventId);

        if (!isEventExist)
        {
            return Result<List<TicketDto>>.Failure(DomainErrors.RelatedEntityNotFound);
        }
        
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
        
        if (result.Count == 0)
            return Result<List<TicketDto>>.Failure(DomainErrors.QueryEmptyResult);

        return Result<List<TicketDto>>.Success(result);
    }

    public async Task<Result<List<TicketWithPriceDto>>> GetTicketsWithPriceById(List<int> ticketsId)
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
        
        if (result.Count != ticketsId.Count)
            return Result<List<TicketWithPriceDto>>.Failure(DomainErrors.EntityNotFound);
        
        return Result<List<TicketWithPriceDto>>.Success(result);
    }

    public async Task<Result<List<TicketTypeDto>>> GetEventTicketTypes(int eventId)
    {
        bool isEventExist = await _context.Events.AnyAsync(e => e.Id == eventId);

        if (!isEventExist)
        {
            return Result<List<TicketTypeDto>>.Failure(DomainErrors.RelatedEntityNotFound);
        }
        
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

        if (result.Count == 0)
            return Result<List<TicketTypeDto>>.Failure(DomainErrors.QueryEmptyResult);

        return Result<List<TicketTypeDto>>.Success(result);
    }

    public async Task<Result<object>> ChangeEventTicketsStatus(string statusName, List<int> ticketsId, int? clientId)
    {
        IQueryable<Ticket> ticketsQuery = _context.Tickets
            .Include(t => t.TicketStatus);

        IQueryable<TicketStatus> ticketStatusesQuery = _context.TicketStatuses
            .AsNoTracking();

        foreach (int ticketId in ticketsId)
        {
            Ticket? ticketEntity = await ticketsQuery.FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticketEntity is null)
                return Result<object>.Failure(DomainErrors.EntityNotFound);

            if (ticketEntity.TicketStatus.Status == statusName)
                return Result<object>.Failure(DomainErrors.SameTicketStatusSet);

            TicketStatus? needStatus = await ticketStatusesQuery.FirstOrDefaultAsync(ts => ts.Status == statusName);

            if (needStatus is null)
                return Result<object>.Failure(DomainErrors.RelatedEntityNotFound);

            ticketEntity.TicketStatusId = needStatus.Id;

            ticketEntity.ClientId = clientId;
        }

        await _context.SaveChangesAsync();
        
        return Result<object>.Success(null);
    }
}
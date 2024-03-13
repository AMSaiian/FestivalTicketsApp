using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.EventService.Filters;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using FestivalTicketsApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.EventService;

public class EventService(AppDbContext context) : IEventService
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Paginated<EventDto>>> GetEvents(EventFilter filter)
    {
        IQueryable<Event> eventsQuery = _context.Events
                .AsNoTracking()
                .Include(e => e.EventDetails)
                .Include(e => e.EventGenre)
                .ThenInclude(eg => eg.EventType)
                .Include(e => e.EventStatus);

        int nextPagesAmount = 0;

        eventsQuery = await ProcessEventFilter(eventsQuery, filter, ref nextPagesAmount);

        List<EventDto> values = await eventsQuery
            .AsNoTracking()
            .Select(e =>
                new EventDto(e.Id, e.Title, e.EventDetails.StartDate, e.Host.Name))
            .ToListAsync();

        if (values.Count == 0)
            return Result<Paginated<EventDto>>.Failure(DomainErrors.QueryEmptyResult);

        Paginated<EventDto> result = new(
            values, 
            filter.Pagination?.PageNum ?? 1, 
            nextPagesAmount);
        
        return Result<Paginated<EventDto>>.Success(result);
    }

    public async Task<Result<EventDto>> GetEventById(int id)
    {
        IQueryable<Event> eventQuery = _context.Events
            .AsNoTracking()
            .Include(e => e.EventDetails)
            .Include(e => e.Host);

        Event? eventEntity = await eventQuery.FirstOrDefaultAsync(e => e.Id == id);
        
        if (eventEntity is null)
            return Result<EventDto>.Failure(DomainErrors.EntityNotFound);

        EventDto result = new(
            eventEntity.Id,
            eventEntity.Title,
            eventEntity.EventDetails.StartDate,
            eventEntity.Host.Name);

        return Result<EventDto>.Success(result);
    }

    public async Task<Result<EventWithDetailsDto>> GetEventWithDetails(int id)
    {
        IQueryable<Event> eventsQuery = _context.Events
            .AsNoTracking()
            .Include(e => e.EventDetails)
            .Include(e => e.Host);
        
        Event? eventEntity = await eventsQuery.FirstOrDefaultAsync(e => e.Id == id);

        if (eventEntity is null)
            return Result<EventWithDetailsDto>.Failure(DomainErrors.EntityNotFound);

        EventWithDetailsDto result = new(
            eventEntity.Id,
            eventEntity.Title,
            eventEntity.EventDetails.StartDate,
            eventEntity.HostId,
            eventEntity.Host.Name,
            eventEntity.EventDetails.Description,
            eventEntity.EventDetails.Duration);

        return Result<EventWithDetailsDto>.Success(result);
    }

    public async Task<Result<List<GenreDto>>> GetGenres(int eventTypeId)
    {
        IQueryable<EventGenre> genresQuery = _context.EventGenres
                .AsNoTracking();

        bool isEventTypeExist = await _context.EventTypes.AnyAsync(et => et.Id == eventTypeId);
        if (!isEventTypeExist)
            return Result<List<GenreDto>>.Failure(DomainErrors.RelatedEntityNotFound);
        
        genresQuery = genresQuery
            .Where(g => g.EventTypeId == eventTypeId)
            .OrderBy(g => g.Id);

        List<GenreDto> result = await genresQuery
            .Select(g =>
                new GenreDto(g.Id, g.Genre))
            .ToListAsync();

        if (result.Count == 0)
            return Result<List<GenreDto>>.Failure(DomainErrors.QueryEmptyResult);

        return Result<List<GenreDto>>.Success(result);
    }

    public async Task<Result<List<EventTypeDto>>> GetEventTypes()
    {
        IQueryable<EventType> eventTypeQuery = _context.EventTypes
                .AsNoTracking();

        List<EventTypeDto> result = await eventTypeQuery
            .Select(et =>
                new EventTypeDto(et.Id, et.Name))
            .ToListAsync();

        if (result.Count == 0)
            return Result<List<EventTypeDto>>.Failure(DomainErrors.QueryEmptyResult);

        return Result<List<EventTypeDto>>.Success(result);
    }

    private Task<IQueryable<Event>> ProcessEventFilter(
        IQueryable<Event> eventsQuery,
        EventFilter filter,
        ref int nextPagesAmount)
    {
        if (filter.CityName is not null)
            eventsQuery = eventsQuery.Where(e => e.Host.Location.CityName == filter.CityName);

        if (filter.StartDate is not null)
            eventsQuery = eventsQuery.Where(e => e.EventDetails.StartDate >= filter.StartDate);

        if (filter.EndDate is not null)
            eventsQuery = eventsQuery.Where(e => e.EventDetails.StartDate.Date <= filter.EndDate);

        if (filter.HostId is not null)
            eventsQuery = eventsQuery.Where(e => e.HostId == filter.HostId);

        if (filter.EventTypeId is not null)
            eventsQuery = eventsQuery.Where(e => e.EventGenre.EventTypeId == filter.EventTypeId);

        if (filter.GenreId is not null)
            eventsQuery = eventsQuery.Where(e => e.EventGenreId == filter.GenreId);

        eventsQuery = eventsQuery.OrderBy(e => e.EventDetails.StartDate);

        if (filter.Pagination is not null)
        {
            int skipValues = (filter.Pagination.PageNum - 1) * filter.Pagination.PageSize;

            nextPagesAmount = (int)Math.Ceiling(
                (double)eventsQuery.Skip(skipValues).Count() / filter.Pagination.PageSize);
            
            eventsQuery = eventsQuery.Skip(skipValues).Take(filter.Pagination.PageSize);
        }
        
        return Task.FromResult(eventsQuery);
    }
}
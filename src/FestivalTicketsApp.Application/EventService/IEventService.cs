using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.EventService.Filters;
using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.EventService;

public interface IEventService
{
    public Task<Result<Paginated<EventDto>>> GetEvents(EventFilter filter);
    
    public Task<Result<EventDto>> GetEventById(int id);

    public Task<Result<EventWithDetailsDto>> GetEventWithDetails(int id);

    public Task<Result<List<GenreDto>>> GetGenres(int eventTypeId);

    public Task<Result<List<EventTypeDto>>> GetEventTypes();
}
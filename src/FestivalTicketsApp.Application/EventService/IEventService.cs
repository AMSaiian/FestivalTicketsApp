using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.EventService.Filters;

namespace FestivalTicketsApp.Application.EventService;

public interface IEventService
{
    public Task<List<EventDto>> GetEvents(EventFilter filter);
    
    public Task<EventDto> GetEventById(int id);

    public Task<EventWithDetailsDto> GetEventWithDetails(int id);

    public Task<List<GenreDto>> GetGenres(GenreFilter filter);

    public Task<List<EventTypeDto>> GetEventTypes();
}
using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.EventService.Filters;

namespace FestivalTicketsApp.Application.EventService;

public interface IEventService
{
    public Task<List<EventDto>> GetEvents(EventsFilter filter);

    public Task<List<GenreDto>> GetGenres(GenresFilter filter);

    public Task<List<EventTypeDto>> GetEventTypes();
}
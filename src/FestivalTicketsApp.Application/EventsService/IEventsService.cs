using FestivalTicketsApp.Application.EventsService.DTO;
using FestivalTicketsApp.Application.EventsService.Filters;

namespace FestivalTicketsApp.Application.EventsService;

public interface IEventsService
{
    public Task<List<EventDto>> GetEvents(EventsFilter filter);

    public Task<List<GenreDto>> GetGenres(GenresFilter filter);

    public Task<List<EventTypeDto>> GetEventTypes();
}
using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.EventsService.Filters;

public record EventsFilter(
    PagingFilter? Pagination, 
    DateTime? StartDate, DateTime? EndDate,
    int? HostId,
    int? EventTypeId,
    int? GenreId,
    string? CityName);
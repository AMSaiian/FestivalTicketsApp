using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.EventService.Filters;

public record EventFilter(
    PagingFilter? Pagination, 
    DateTime? StartDate, DateTime? EndDate,
    int? HostId,
    int? EventTypeId,
    int? GenreId,
    string? CityName);
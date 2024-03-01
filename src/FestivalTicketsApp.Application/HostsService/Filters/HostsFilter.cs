using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.HostsService.Filters;

public record HostsFilter(
    PagingFilter? Pagination, 
    int? HostTypeId,
    string? CityName);
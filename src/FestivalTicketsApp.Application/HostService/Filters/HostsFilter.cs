using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.HostService.Filters;

public record HostsFilter(
    PagingFilter? Pagination, 
    int? HostTypeId,
    string? CityName);
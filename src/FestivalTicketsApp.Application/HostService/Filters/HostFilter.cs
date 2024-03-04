using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.HostService.Filters;

public record HostFilter(
    PagingFilter? Pagination, 
    int? HostTypeId,
    string? CityName);
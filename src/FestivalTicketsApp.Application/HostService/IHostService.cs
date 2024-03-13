using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.HostService.Filters;
using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.HostService;

public interface IHostService
{
    public Task<Result<Paginated<HostDto>>> GetHosts(HostFilter filter);

    public Task<Result<HostWithDetailsDto>> GetHostWithDetails(int id);

    public Task<Result<List<HostedEventDto>>> GetHostedEvents(int id);
    
    public Task<Result<List<HostTypeDto>>> GetHostTypes();
    
    public Task<Result<List<string>>> GetCities();

    public Task<Result<HostHallDetailsDto>> GetHostHallDetails(int id);
}
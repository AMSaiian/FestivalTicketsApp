using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.HostService.Filters;

namespace FestivalTicketsApp.Application.HostService;

public interface IHostService
{
    public Task<List<HostDto>> GetHosts(HostFilter filter);

    public Task<HostWithDetailsDto> GetHostWithDetails(int id);

    public Task<List<HostedEventDto>> GetHostedEvents(int id);
    
    public Task<List<HostTypeDto>> GetHostTypes();
    
    public Task<List<string>> GetCities();

    public Task<HostHallDetailsDto> GetHostHallDetails(int id);
}
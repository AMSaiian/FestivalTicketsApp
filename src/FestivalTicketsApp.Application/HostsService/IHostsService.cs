using FestivalTicketsApp.Application.HostsService.DTO;
using FestivalTicketsApp.Application.HostsService.Filters;

namespace FestivalTicketsApp.Application.HostsService;

public interface IHostsService
{
    public Task<List<HostDto>> GetHosts(HostsFilter filter);
    
    public Task<List<HostTypeDto>> GetHostTypes();
    
    public Task<List<string>> GetCities();
}
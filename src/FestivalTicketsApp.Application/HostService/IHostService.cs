using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.HostService.Filters;

namespace FestivalTicketsApp.Application.HostService;

public interface IHostService
{
    public Task<List<HostDto>> GetHosts(HostsFilter filter);
    
    public Task<List<HostTypeDto>> GetHostTypes();
    
    public Task<List<string>> GetCities();
}
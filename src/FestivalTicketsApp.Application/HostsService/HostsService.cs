using FestivalTicketsApp.Application.EventsService.DTO;
using FestivalTicketsApp.Application.HostsService.DTO;
using FestivalTicketsApp.Application.HostsService.Filters;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.HostsService;

public class HostsService(AppDbContext context) : IHostsService
{
    private readonly AppDbContext _context = context;

    public async Task<List<HostDto>> GetHosts(HostsFilter filter)
    {
        IQueryable<Host> hostsQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.Location)
            .Include(h => h.HostType)
            .AsQueryable();

        hostsQuery = await ProcessHostFilter(hostsQuery, filter);

        List<HostDto> result = await hostsQuery
            .Select(h => new HostDto(h.Id, h.Name))
            .ToListAsync();

        return result;
    }

    public async Task<List<HostTypeDto>> GetHostTypes()
    {
        IQueryable<HostType> hostTypesQuery = _context.HostTypes.AsQueryable();

        List<HostTypeDto> result = await hostTypesQuery
            .Select(ht => new HostTypeDto(ht.Id, ht.Name))
            .ToListAsync();

        return result;
    }

    public async Task<List<string>> GetCities()
    {
        IQueryable<Location> locationsQuery = _context.Locations.AsQueryable();
        
        List<string> result = await locationsQuery
            .GroupBy(l => l.CityName)
            .Select(cg => cg.Key)
            .ToListAsync();

        return result;
    }

    private Task<IQueryable<Host>> ProcessHostFilter(IQueryable<Host> hostsQuery, HostsFilter filter)
    {
        if (filter.CityName is not null)
            hostsQuery = hostsQuery.Where(h => h.Location.CityName == filter.CityName);
        
        if (filter.HostTypeId is not null)
            hostsQuery = hostsQuery.Where(h => h.HostTypeId == filter.HostTypeId);

        hostsQuery = hostsQuery.OrderBy(h => h.Id);
        
        if (filter.Pagination is not null)
        {
            int skipValues = (filter.Pagination.PageNum - 1) * filter.Pagination.PageSize;
            hostsQuery = hostsQuery.Skip(skipValues).Take(filter.Pagination.PageSize);
        }
        
        return Task.FromResult(hostsQuery);
    }
}
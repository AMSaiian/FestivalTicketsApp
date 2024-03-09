using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.HostService.Filters;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.HostService;

public class HostService(AppDbContext context) : IHostService
{
    private readonly AppDbContext _context = context;

    public async Task<List<HostDto>> GetHosts(HostFilter filter)
    {
        IQueryable<Host> hostsQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.Location)
            .Include(h => h.HostType);

        hostsQuery = await ProcessHostFilter(hostsQuery, filter);

        List<HostDto> result = await hostsQuery
            .Select(h => new HostDto(h.Id, h.Name))
            .ToListAsync();

        return result;
    }

    public async Task<HostWithDetailsDto> GetHostWithDetails(int id)
    {
        IQueryable<Host> hostsQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.Location)
            .Include(h => h.Details);

        Host? hostEntity = await hostsQuery.FirstOrDefaultAsync(h => h.Id == id);

        HostWithDetailsDto result = new HostWithDetailsDto(
            hostEntity.Id,
            hostEntity.Name,
            hostEntity.Details.Description,
            new LocationDto(
                hostEntity.Location.Id,
                hostEntity.Location.CityName,
                hostEntity.Location.StreetName,
                hostEntity.Location.BuildingNumber,
                hostEntity.Location.Latitude,
                hostEntity.Location.Longitude));
        
        return result;
    }

    public async Task<HostHallDetailsDto> GetHostHallDetails(int id)
    {
        IQueryable<Host> _hostQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.Details);

        Host? hostEntity = await _hostQuery.FirstOrDefaultAsync(h => h.Id == id);

        HostHallDetailsDto result = new HostHallDetailsDto(
            hostEntity.Id,
            hostEntity.Details.RowAmount,
            hostEntity.Details.SeatsInRow,
            hostEntity.Details.IsDividedBySeats);

        return result;
    }

    public async Task<List<HostedEventDto>> GetHostedEvents(int id)
    {
        IQueryable<Host> hostsQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.HostedEvents).ThenInclude(e => e.EventDetails);

        Host? hostEntity = await hostsQuery.FirstOrDefaultAsync(h => h.Id == id);

        List<HostedEventDto> result = hostEntity.HostedEvents
            .Select(he =>
                new HostedEventDto(
                    he.Id,
                    he.Title,
                    he.EventDetails.StartDate))
            .ToList();
        
        return result;
    }

    public async Task<List<HostTypeDto>> GetHostTypes()
    {
        IQueryable<HostType> hostTypesQuery = _context.HostTypes
            .AsNoTracking();

        List<HostTypeDto> result = await hostTypesQuery
            .Select(ht => new HostTypeDto(ht.Id, ht.Name))
            .ToListAsync();

        return result;
    }

    public async Task<List<string>> GetCities()
    {
        IQueryable<Location> locationsQuery = _context.Locations
            .AsNoTracking();
        
        List<string> result = await locationsQuery
            .GroupBy(l => l.CityName)
            .Select(cg => cg.Key)
            .ToListAsync();

        return result;
    }

    private Task<IQueryable<Host>> ProcessHostFilter(IQueryable<Host> hostsQuery, HostFilter filter)
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
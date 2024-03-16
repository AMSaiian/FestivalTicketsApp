using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.HostService.Filters;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using FestivalTicketsApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.HostService;

public class HostService(AppDbContext context) : IHostService
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Paginated<HostDto>>> GetHosts(HostFilter filter)
    {
        IQueryable<Host> hostsQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.Location)
            .Include(h => h.HostType);

        int nextPagesAmount = 0;

        hostsQuery = await ProcessHostFilter(hostsQuery, filter, ref nextPagesAmount);

        List<HostDto> values = await hostsQuery
            .Select(h => new HostDto(h.Id, h.Name))
            .ToListAsync();

        if (values.Count == 0)
            return Result<Paginated<HostDto>>.Failure(DomainErrors.QueryEmptyResult);

        Paginated<HostDto> result = new(
            values,
            filter.Pagination?.PageNum ?? 1,
            nextPagesAmount);

        return Result<Paginated<HostDto>>.Success(result);
    }

    public async Task<Result<HostWithDetailsDto>> GetHostWithDetails(int id)
    {
        IQueryable<Host> hostsQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.Location)
            .Include(h => h.Details);

        Host? hostEntity = await hostsQuery.FirstOrDefaultAsync(h => h.Id == id);

        if (hostEntity is null)
            return Result<HostWithDetailsDto>.Failure(DomainErrors.EntityNotFound);

        HostWithDetailsDto result = new(
            hostEntity!.Id,
            hostEntity.Name,
            hostEntity.Details.Description,
            new LocationDto(
                hostEntity.Location.Id,
                hostEntity.Location.CityName,
                hostEntity.Location.StreetName,
                hostEntity.Location.BuildingNumber,
                hostEntity.Location.Latitude,
                hostEntity.Location.Longitude));
        
        return Result<HostWithDetailsDto>.Success(result);
    }

    public async Task<Result<HostHallDetailsDto>> GetHostHallDetails(int id)
    {
        IQueryable<Host> hostQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.Details);

        Host? hostEntity = await hostQuery.FirstOrDefaultAsync(h => h.Id == id);
        
        if (hostEntity is null)
            return Result<HostHallDetailsDto>.Failure(DomainErrors.EntityNotFound);

        HostHallDetailsDto result = new(
            hostEntity!.Id,
            hostEntity.Details.RowAmount,
            hostEntity.Details.SeatsInRow,
            hostEntity.Details.IsDividedBySeats);

        return Result<HostHallDetailsDto>.Success(result);
    }

    public async Task<Result<List<HostedEventDto>>> GetHostedEvents(int id)
    {
        IQueryable<Host> hostsQuery = _context.Hosts
            .AsNoTracking()
            .Include(h => h.HostedEvents).ThenInclude(e => e.EventDetails);

        Host? hostEntity = await hostsQuery.FirstOrDefaultAsync(h => h.Id == id);

        if (hostEntity is null)
            return Result<List<HostedEventDto>>.Failure(DomainErrors.EntityNotFound);

        List<HostedEventDto> result = hostEntity.HostedEvents
            .Select(he =>
                new HostedEventDto(
                    he.Id,
                    he.Title,
                    he.EventDetails.StartDate))
            .ToList();

        if (result.Count == 0)
            return Result<List<HostedEventDto>>.Failure(DomainErrors.QueryEmptyResult);

        return Result<List<HostedEventDto>>.Success(result);
    }

    public async Task<Result<List<HostTypeDto>>> GetHostTypes()
    {
        IQueryable<HostType> hostTypesQuery = _context.HostTypes
            .AsNoTracking();

        List<HostTypeDto> result = await hostTypesQuery
            .Select(ht => new HostTypeDto(ht.Id, ht.Name))
            .ToListAsync();

        if (result.Count == 0)
            return Result<List<HostTypeDto>>.Failure(DomainErrors.QueryEmptyResult);

        return Result<List<HostTypeDto>>.Success(result);
    }

    public async Task<Result<List<string>>> GetCities()
    {
        IQueryable<Location> locationsQuery = _context.Locations
            .AsNoTracking();
        
        List<string> result = await locationsQuery
            .GroupBy(l => l.CityName)
            .Select(cg => cg.Key)
            .ToListAsync();

        if (result.Count == 0)
            return Result<List<string>>.Failure(DomainErrors.QueryEmptyResult);

        return Result<List<string>>.Success(result);
    }

    private Task<IQueryable<Host>> ProcessHostFilter(
        IQueryable<Host> hostsQuery, 
        HostFilter filter,
        ref int nextPagesAmount)
    {
        if (filter.CityName is not null)
            hostsQuery = hostsQuery.Where(h => h.Location.CityName == filter.CityName);
        
        if (filter.HostTypeId is not null)
            hostsQuery = hostsQuery.Where(h => h.HostTypeId == filter.HostTypeId);

        hostsQuery = hostsQuery.OrderBy(h => h.Id);
        
        if (filter.Pagination is not null)
        {
            int skipValues = (filter.Pagination.PageNum - 1) * filter.Pagination.PageSize;

            nextPagesAmount = (int)Math.Ceiling(
                (double)hostsQuery.Skip(skipValues + filter.Pagination.PageSize).Count() / filter.Pagination.PageSize);
            
            hostsQuery = hostsQuery.Skip(skipValues).Take(filter.Pagination.PageSize);
        }
        
        return Task.FromResult(hostsQuery);
    }
}
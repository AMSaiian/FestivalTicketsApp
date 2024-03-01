﻿using FestivalTicketsApp.Application.EventsService.DTO;
using FestivalTicketsApp.Application.EventsService.Filters;
using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.EventsService;

public class EventsService(in AppDbContext context) : IEventsService
{
    private readonly AppDbContext _context = context;

    public async Task<List<EventDto>> GetEvents(EventsFilter filter)
    {
        IQueryable<Event> eventsQuery = _context.Events
            .AsNoTracking()
            .Include(e => e.EventDetails)
            .Include(e => e.EventGenre)
            .Include(e => e.EventStatus)
            .Include(e => e.Host).ThenInclude(e => e.Location)
            .AsQueryable();

        eventsQuery = await ProcessEventFilter(eventsQuery, filter);

        List<EventDto> result = await eventsQuery
            .AsNoTracking()
            .Select(e => 
                new EventDto(e.Id, e.Title, e.EventDetails.StartDate, e.Host.Name))
            .ToListAsync();

        return result;
    }

    public async Task<List<GenreDto>> GetGenres(GenresFilter filter)
    {
        IQueryable<EventGenre> genresQuery = _context.EventGenres
            .AsNoTracking()
            .AsQueryable();
        
        genresQuery = await ProcessGenreFilter(genresQuery, filter);

        List<GenreDto> result = await genresQuery
            .Select(g => 
                new GenreDto(g.Id, g.Genre))
            .ToListAsync();

        return result;
    }

    private Task<IQueryable<Event>> ProcessEventFilter(
        IQueryable<Event> eventsQuery, 
        EventsFilter filter)
    {
        if (filter.CityName is not null)
            eventsQuery = eventsQuery.Where(e => e.Host.Location.CityName == filter.CityName);

        if (filter.StartDate is not null)
            eventsQuery = eventsQuery.Where(e => e.EventDetails.StartDate >= filter.StartDate);
        
        if (filter.EndDate is not null)
            eventsQuery = eventsQuery.Where(e => e.EventDetails.StartDate <= filter.EndDate);

        if (filter.HostTypeId is not null)
            eventsQuery = eventsQuery.Where(e => e.Host.HostTypeId == filter.HostTypeId);

        if (filter.HostId is not null)
            eventsQuery = eventsQuery.Where(e => e.HostId == filter.HostId);

        if (filter.GenreId is not null)
            eventsQuery = eventsQuery.Where(e => e.EventGenreId == filter.GenreId);

        eventsQuery = eventsQuery.OrderBy(e => e.EventDetails.StartDate);

        if (filter.Pagination is not null)
        {
            int skipValues = filter.Pagination.PageNum - 1 * filter.Pagination.PageSize;
            eventsQuery = eventsQuery.Skip(skipValues).Take(filter.Pagination.PageSize);
        }


        return Task.FromResult(eventsQuery);
    }
    
    private Task<IQueryable<EventGenre>> ProcessGenreFilter(IQueryable<EventGenre> genresQuery, GenresFilter filter)
    {
        genresQuery = genresQuery
            .Where(g => g.EventTypeId == filter.EventTypeId)
            .OrderBy(g => g.Id);
        
        return Task.FromResult(genresQuery);
    }
}
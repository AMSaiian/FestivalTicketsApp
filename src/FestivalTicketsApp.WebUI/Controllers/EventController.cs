using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.EventService.Filters;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Shared;
using FestivalTicketsApp.WebUI.Models;
using FestivalTicketsApp.WebUI.Models.Event;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.Controllers;

public class EventController(IEventService eventService, IHostService hostService) : Controller
{
    private readonly IEventService _eventService = eventService;

    private readonly IHostService _hostService = hostService;
    
    public async Task<IActionResult> List(int id, [FromQuery, Bind(Prefix = "QueryState")] EventListQuery query)
     {
         int eventTypeId = id;

         EventListViewModel viewModel = new();

         viewModel.QueryState = query;
         
         viewModel.CityNames = await _hostService.GetCities();
         
         GenreFilter genreFilter = new(eventTypeId);
         
         viewModel.Genres = await _eventService.GetGenres(genreFilter);
         
         EventFilter eventFilter = new(
             new PagingFilter(RequestDefaults.PageNum, RequestDefaults.PageSize), 
             query.StartDate, 
             query.EndDate, 
             null, 
             eventTypeId, 
             query.GenreId, 
             query.CityName);
         
         viewModel.Events = await _eventService.GetEvents(eventFilter);
         
         return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        int eventId = id;

        EventDetailsViewModel viewModel = new();

        viewModel.Event = await _eventService.GetEventWithDetails(eventId);

        int hostId = viewModel.Event.HostId;

        viewModel.HostedEvents = await _hostService.GetHostedEvents(hostId);

        return View(viewModel);
    }
}
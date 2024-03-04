using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.EventService.Filters;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Shared;
using FestivalTicketsApp.WebUI.Models;
using FestivalTicketsApp.WebUI.Models.EventList;
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
         
         GenresFilter genresFilter = new(eventTypeId);
         
         viewModel.Genres = await _eventService.GetGenres(genresFilter);
         
         EventsFilter eventsFilter = new(
             new PagingFilter(), 
             query.StartDate, 
             query.EndDate, 
             null, 
             eventTypeId, 
             query.GenreId, 
             query.CityName);
         
         viewModel.Events = await _eventService.GetEvents(eventsFilter);
         
         return View(viewModel);
    }
}
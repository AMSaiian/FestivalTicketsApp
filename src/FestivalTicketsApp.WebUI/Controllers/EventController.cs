using FestivalTicketsApp.Application.EventsService;
using FestivalTicketsApp.Application.EventsService.Filters;
using FestivalTicketsApp.Application.HostsService;
using FestivalTicketsApp.Shared;
using FestivalTicketsApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.Controllers;

public class EventController(IEventsService eventsService, IHostsService hostsService) : Controller
{
    private static string LayoutName = "MainLayout";

    private readonly IEventsService _eventsService = eventsService;

    private readonly IHostsService _hostsService = hostsService;
    
    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> List(int id, EventListViewModel? viewModel)
    {
        int eventTypeId = id;
        
        viewModel.CityNames = await _hostsService.GetCities();

        GenresFilter genresFilter = new(eventTypeId);

        viewModel.Genres = await _eventsService.GetGenres(genresFilter);

        EventsFilter eventsFilter = new(
            new PagingFilter(),
            viewModel.Filter.StartDate,
            viewModel.Filter.EndDate,
            null,
            eventTypeId, 
            viewModel.Filter.GenreId,
            viewModel.Filter.CityName);

        viewModel.Events = await _eventsService.GetEvents(eventsFilter);
        
        return View(viewModel);
    }
}
using System.Security.Claims;
using FestivalTicketsApp.Application;
using FestivalTicketsApp.Application.ClientService;
using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.EventService.Filters;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Shared;
using FestivalTicketsApp.WebUI.Models;
using FestivalTicketsApp.WebUI.Models.Event;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.Controllers;

public class EventController(IEventService eventService, 
                             IHostService hostService, 
                             IClientService clientService) 
    : Controller
{
    private readonly IEventService _eventService = eventService;

    private readonly IHostService _hostService = hostService;

    private readonly IClientService _clientService = clientService;
    
    public async Task<IActionResult> List(int id, [FromQuery, Bind(Prefix = "QueryState")] EventListQuery query)
    {
         int eventTypeId = id;
         
         Result<List<string>> getCityNamesResult = await _hostService.GetCities();

         Result<List<GenreDto>> getGenresResult = await _eventService.GetGenres(eventTypeId);

         if (!getCityNamesResult.IsSuccess || !getGenresResult.IsSuccess)
             throw new RequiredDataNotFoundException();
         
         EventFilter eventFilter = new
         (
             new PagingFilter(query.PageNum, query.PageSize), 
             query.StartDate, 
             query.EndDate, 
             null, 
             eventTypeId, 
             query.GenreId, 
             query.CityName
         );
    
         Result<Paginated<EventDto>> getEventsResult = await _eventService.GetEvents(eventFilter);
         
         EventListViewModel viewModel = new();
         
         viewModel.QueryState = query;

         viewModel.CityNames = getCityNamesResult.Value!;

         viewModel.Genres = getGenresResult.Value!;
         
         if (getEventsResult.IsSuccess)
         {
             viewModel.Events = getEventsResult.Value!.Value;

             viewModel.CurrentPageNum = getEventsResult.Value.CurrentPage;

             viewModel.NextPagesAmount = getEventsResult.Value.NextPagesAmount;
         }
         else
         {
             viewModel.CurrentPageNum = RequestDefaults.PageNum;

             viewModel.NextPagesAmount = RequestDefaults.NextPagesAmount;
         }
         
         return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        int eventId = id;

        Result<EventWithDetailsDto> getEventResult = await _eventService.GetEventWithDetails(eventId);

        if (!getEventResult.IsSuccess)
            throw new RequiredDataNotFoundException();
        
        EventDetailsViewModel viewModel = new();
        
        string? userIdRaw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdRaw is not null 
          && int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
        {
            Result<bool> isInFavourite = await _clientService.IsInFavourite(eventId, userId);

            if (isInFavourite.IsSuccess)
                viewModel.IsInFavourite = isInFavourite.Value;
        }
        
        viewModel.Event = getEventResult.Value!;
        
        Result<List<HostedEventDto>> getHostedEventsResult = 
            await _hostService.GetHostedEvents(getEventResult.Value!.HostId);

        if (getHostedEventsResult.IsSuccess)
        {
            viewModel.HostedEvents = getHostedEventsResult.Value;
        }
        
        return View(viewModel);
    }
}
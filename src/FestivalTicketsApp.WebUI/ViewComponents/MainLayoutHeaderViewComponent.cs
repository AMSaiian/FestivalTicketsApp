using FestivalTicketsApp.Application.EventsService;
using FestivalTicketsApp.Application.EventsService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.ViewComponents;

public class MainLayoutHeaderViewComponent(IEventsService eventsService) : ViewComponent
{
    private readonly IEventsService _eventsService = eventsService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<EventTypeDto> eventTypes = await _eventsService.GetEventTypes();

        return View(eventTypes);
    }
}
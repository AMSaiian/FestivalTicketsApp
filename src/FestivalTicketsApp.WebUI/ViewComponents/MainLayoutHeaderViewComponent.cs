using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.EventService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.ViewComponents;

public class MainLayoutHeaderViewComponent(IEventService eventService) : ViewComponent
{
    private readonly IEventService _eventService = eventService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<EventTypeDto> eventTypes = (await _eventService.GetEventTypes()).Value;

        return View(eventTypes);
    }
}
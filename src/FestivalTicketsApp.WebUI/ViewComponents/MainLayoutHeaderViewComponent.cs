using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.ViewComponents;

public class MainLayoutHeaderViewComponent(IEventService eventService) : ViewComponent
{
    private readonly IEventService _eventService = eventService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        Result<List<EventTypeDto>> getEventTypesResult = await _eventService.GetEventTypes();

        if (!getEventTypesResult.IsSuccess)
            throw new RequiredDataNotFoundException();
                
        List<EventTypeDto> eventTypes = getEventTypesResult.Value!;

        return View(eventTypes);
    }
}
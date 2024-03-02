using FestivalTicketsApp.Application.EventsService;
using FestivalTicketsApp.Application.EventsService.DTO;
using FestivalTicketsApp.Application.HostsService;
using FestivalTicketsApp.Application.HostsService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.ViewComponents;

public class SideNavigationViewComponent(IHostsService hostsService) : ViewComponent
{
    private readonly IHostsService _hostsService = hostsService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<HostTypeDto> hostTypes = await _hostsService.GetHostTypes();

        return View(hostTypes);
    }
}
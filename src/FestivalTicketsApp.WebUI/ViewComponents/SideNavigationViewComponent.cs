using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.HostService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.ViewComponents;

public class SideNavigationViewComponent(IHostService hostService) : ViewComponent
{
    private readonly IHostService _hostService = hostService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<HostTypeDto> hostTypes = (await _hostService.GetHostTypes()).Value;

        return View(hostTypes);
    }
}
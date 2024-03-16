using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.ViewComponents;

public class SideNavigationViewComponent(IHostService hostService) : ViewComponent
{
    private readonly IHostService _hostService = hostService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var getHostTypesResult = await _hostService.GetHostTypes();

        if (!getHostTypesResult.IsSuccess)
            throw new RequiredDataNotFoundException();
        
        List<HostTypeDto> hostTypes = getHostTypesResult.Value!;

        return View(hostTypes);
    }
}
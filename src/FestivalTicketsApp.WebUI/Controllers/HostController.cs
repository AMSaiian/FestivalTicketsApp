using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.HostService.Filters;
using FestivalTicketsApp.Shared;
using FestivalTicketsApp.WebUI.Models;
using FestivalTicketsApp.WebUI.Models.Host;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.Controllers;

public class HostController(IHostService service) : Controller
{
    private readonly IHostService _service = service;

    public async Task<IActionResult> List(int id, [FromQuery, Bind(Prefix = "QueryState")] HostListQuery query)
    {
        int hostTypeId = id;
        
        Result<List<string>> getCityNamesResult = await _service.GetCities();

        if (!getCityNamesResult.IsSuccess)
            throw new RequiredDataNotFoundException();
        
        HostListViewModel viewModel = new();

        viewModel.QueryState = query;
         
        viewModel.CityNames = getCityNamesResult.Value!;
        
        HostFilter hostFilter = new(
            new PagingFilter(query.PageNum, query.PageSize),
            hostTypeId,
            query.CityName);

        Result<Paginated<HostDto>> getHostsResult = await _service.GetHosts(hostFilter);
         
        if (getHostsResult.IsSuccess)
        {
            viewModel.Hosts = getHostsResult.Value!.Value;

            viewModel.CurrentPageNum = getHostsResult.Value.CurrentPage;

            viewModel.NextPagesAmount = getHostsResult.Value.NextPagesAmount;
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
        int hostId = id;

        var getHostResult = await _service.GetHostWithDetails(hostId);
            
        if (!getHostResult.IsSuccess)
            throw new RequiredDataNotFoundException();

        HostDetailsViewModel viewModel = new();

        viewModel.Host = getHostResult.Value!;

        viewModel.HostedEvents = (await _service.GetHostedEvents(hostId)).Value;

        return View(viewModel);
    }
}
using FestivalTicketsApp.Application.HostService;
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
        
        HostListViewModel viewModel = new();

        viewModel.QueryState = query;
         
        viewModel.CityNames = await _service.GetCities();
        
        HostFilter hostFilter = new(
            new PagingFilter(RequestDefaults.PageNum, RequestDefaults.PageSize),
            hostTypeId,
            query.CityName);
         
        viewModel.Hosts = await _service.GetHosts(hostFilter);
         
        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        int hostId = id;

        HostDetailsViewModel viewModel = new();

        viewModel.Host = await _service.GetHostWithDetails(hostId);

        viewModel.HostedEvents = await _service.GetHostedEvents(hostId);

        return View(viewModel);
    }
}
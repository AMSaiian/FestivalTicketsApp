using System.Security.Claims;
using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.EventService.DTO;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.HostService.DTO;
using FestivalTicketsApp.Application.TicketService;
using FestivalTicketsApp.Application.TicketService.DTO;
using FestivalTicketsApp.Shared;
using FestivalTicketsApp.WebUI.Models.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.Controllers;

public class TicketController(
    ITicketService ticketService, 
    IHostService hostService, 
    IEventService eventService) : Controller
{
    private readonly ITicketService _ticketService = ticketService;

    private readonly IHostService _hostService = hostService;

    private readonly IEventService _eventService = eventService;

    private static readonly string TicketAvailableStatus = "Available";
    private static readonly string TicketPurchasedStatus = "Purchased";
    private static readonly string TicketOutOfDateStatus = "Out of date";
    
    [Authorize]
    public async Task<IActionResult> Selection(int id)
    {
        int eventId = id;

        Result<List<TicketDto>> getTicketsResult = await _ticketService.GetEventTickets(eventId);
        Result<List<TicketTypeDto>> getTicketTypesResult = await _ticketService.GetEventTicketTypes(eventId);
        Result<HostHallDetailsDto> getHallDetailsResult = await _hostService.GetHostHallDetails(eventId);

        if (!getTicketsResult.IsSuccess 
         || !getTicketTypesResult.IsSuccess 
         || !getHallDetailsResult.IsSuccess)
            throw new RequiredDataNotFoundException();
        
        TicketSelectionViewModel viewModel = new();

        viewModel.Tickets = getTicketsResult.Value!;

        viewModel.TicketTypes = getTicketTypesResult.Value!;

        viewModel.HallDetails = getHallDetailsResult.Value!;

        return View(viewModel);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Confirmation(int id, List<int> tickets)
    {
        int eventId = id;

        Result<List<TicketWithPriceDto>> getTicketsResult = await _ticketService.GetTicketsWithPriceById(tickets);
        Result<EventDto> getEventResult = await _eventService.GetEventById(eventId);

        if (!getTicketsResult.IsSuccess || !getEventResult.IsSuccess)
            throw new RequiredDataNotFoundException();

        TicketConfirmationViewModel viewModel = new();

        viewModel.SelectedTickets = getTicketsResult.Value!;

        viewModel.Event = getEventResult.Value!;

        return View(viewModel);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Purchase(int id, TicketPurchaseRequestModel purchaseInfo)
    {
        int eventId = id;
        
        TicketPurchaseResultViewModel viewModel = new();
        
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);

        viewModel.EventId = eventId;

        viewModel.IsSucceeded = (await _ticketService.ChangeEventTicketsStatus(
            TicketPurchasedStatus,
            purchaseInfo.SelectedTicketsId,
            userId)).IsSuccess;

        return View(viewModel);
    }
}
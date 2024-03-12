using System.Security.Claims;
using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.TicketService;
using FestivalTicketsApp.WebUI.Models.Ticket;
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

    public async Task<IActionResult> Selection(int id)
    {
        int eventId = id;

        TicketSelectionViewModel viewModel = new();

        viewModel.Tickets = (await _ticketService.GetEventTickets(eventId)).Value;

        viewModel.TicketTypes = (await _ticketService.GetEventTicketTypes(eventId)).Value;

        viewModel.HallDetails = (await _hostService.GetHostHallDetails(eventId)).Value;

        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Confirmation(int id, List<int> tickets)
    {
        int eventId = id;

        TicketConfirmationViewModel viewModel = new();

        viewModel.SelectedTickets = (await _ticketService.GetTicketsWithPriceById(tickets)).Value;

        viewModel.Event = (await _eventService.GetEventById(eventId)).Value;

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Purchase(int id, TicketPurchaseRequestModel purchaseInfo)
    {
        int eventId = id;
        
        TicketPurchaseResultViewModel viewModel = new();

        int? userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        viewModel.EventId = eventId;

        viewModel.IsSucceeded = (await _ticketService.ChangeEventTicketsStatus(
            TicketPurchasedStatus,
            purchaseInfo.SelectedTicketsId,
            userId)).IsSuccess;

        return View(viewModel);
    }
}
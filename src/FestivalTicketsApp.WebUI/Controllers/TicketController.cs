using FestivalTicketsApp.Application.EventService;
using FestivalTicketsApp.Application.HostService;
using FestivalTicketsApp.Application.TicketService;
using FestivalTicketsApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FestivalTicketsApp.WebUI.Controllers;

public class TicketController(
    ITicketService ticketService, 
    IHostService hostService, 
    IEventService eventService) : Controller
{
    private readonly ITicketService _ticketService = ticketService;

    private readonly IHostService _hostService = hostService;

    private readonly IEventService _eventService = eventService;

    public async Task<IActionResult> Selection(int id)
    {
        int eventId = id;

        TicketSelectionViewModel viewModel = new();

        viewModel.Tickets = await _ticketService.GetEventTickets(eventId);

        viewModel.TicketTypes = await _ticketService.GetEventTicketTypes(eventId);

        viewModel.HallDetails = await _hostService.GetHostHallDetails(eventId);

        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Confirmation(int id, List<int> tickets)
    {
        int eventId = id;

        TicketConfirmationViewModel viewModel = new();

        viewModel.SelectedTickets = await _ticketService.GetTicketsWithPriceById(tickets);

        viewModel.Event = await _eventService.GetEventById(eventId);

        return View(viewModel);
    }
}
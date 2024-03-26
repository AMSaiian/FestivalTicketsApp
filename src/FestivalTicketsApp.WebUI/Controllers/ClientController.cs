using System.Security.Claims;
using FestivalTicketsApp.Application;
using FestivalTicketsApp.Application.ClientService;
using FestivalTicketsApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FestivalTicketsApp.WebUI.Controllers;

public class ClientController(IClientService clientService) : Controller
{
    private readonly IClientService _clientService = clientService;
    
    [Authorize]
    public async Task<IActionResult> ChangeFavouriteStatus(int id, bool newStatus)
    {
        int eventId = id;
        
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);

        Result<object> result = await _clientService.ChangeFavouriteStatus(eventId, userId, newStatus);

        switch (result.IsSuccess)
        {
            case true:
                return Ok();
            case false when result.Error == DomainErrors.RelatedEntityNotFound:
                return NotFound();
            case false when result.Error == DomainErrors.SameFavouriteStatusSet:
                return BadRequest();
            default:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
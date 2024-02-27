using Microsoft.AspNetCore.Identity;

namespace FestivalTicketsApp.Core.Entities;

public class Client : IdentityUser<int>
{
    public string Name { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public List<Ticket> PurchasedTickets { get; set; } = [];

    public List<Event> FavouriteEvents { get; set; } = [];
}
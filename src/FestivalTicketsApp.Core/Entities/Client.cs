using Microsoft.AspNetCore.Identity;

namespace FestivalTicketsApp.Core.Entities;

public class Client : BaseEntity
{
    public string Name { get; set; } = default!;

    public string Surname { get; set; } = default!;

    public string Email { get; set; } = default!;

    public List<Ticket> PurchasedTickets { get; set; } = [];

    public List<Event> FavouriteEvents { get; set; } = [];
}
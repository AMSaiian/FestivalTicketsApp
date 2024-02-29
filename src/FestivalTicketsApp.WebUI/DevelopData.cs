using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Host = FestivalTicketsApp.Core.Entities.Host;

namespace FestivalTicketsApp.WebUI;

public static class DevelopData
{
    public static readonly List<TicketStatus> TicketStatuses = 
    [
        new() { Status = "Available"},
        new() { Status = "Purchased"},
        new() { Status = "Out of date"} 
    ];

    public static readonly List<TicketType> TicketTypes =
    [
        new() { Name = "Economy", Price = 75, EventId = 1 },
        new() { Name = "Regular", Price = 125, EventId = 1 },
        new() { Name = "Comfort", Price = 250, EventId = 1 },
        
        new() { Name = "Fan zone", Price = 50, EventId = 2 }
    ];

    public static readonly List<EventStatus> EventStatuses = 
    [
        new() { Status = "Planned" },
        new() { Status = "Ended" }
    ];
    
    public static readonly List<EventType> EventTypes = 
    [
        new() { Name = "Performance" },
        new() { Name = "Concert" },
        new() { Name = "Stand-up" }
    ];
    
    public static readonly List<EventGenre> EventGenres = 
    [
        new() { Genre = "Drama", EventTypeId = 1 },
        new() { Genre = "Comedian", EventTypeId = 1 },
        new() { Genre = "Tragedy", EventTypeId = 1 },
        new() { Genre = "Opera", EventTypeId = 1 },
        new() { Genre = "Musicale", EventTypeId = 1 },
        new() { Genre = "Ballet", EventTypeId = 1},
        
        new() { Genre = "Rock", EventTypeId = 2 },
        new() { Genre = "Techno", EventTypeId = 2 },
        new() { Genre = "Hip-hop", EventTypeId = 2},
        new() { Genre = "Rap", EventTypeId = 2 },
        new() { Genre = "Orchestra",  EventTypeId = 2 },
        new() { Genre = "Pop", EventTypeId = 2 },
        
        new() { Genre = "Humor", EventTypeId = 3 },
        new() { Genre = "Improvisation", EventTypeId = 3 },
    ];
    
    public static readonly List<EventDetails> EventDetails = 
    [
        new() 
        { 
            Id = 1, Description = "Lorem ipsum event 1", Duration = 120,
            StartDate = new(2024, 3, 1, 17, 0, 0, DateTimeKind.Local)
        },
        new() 
        { 
            Id = 2, Description = "Lorem ipsum event 2", Duration = 110,
            StartDate = new(2024, 3, 2, 17, 0, 0, DateTimeKind.Local)
        }
    ];

    public static readonly List<Event> Events = 
    [
        new() { Title = "Drama_name_1" ,EventGenreId = 1, EventStatusId = 1, HostId = 1 },
        new() { Title = "Rock_name_1" , EventGenreId = 7, EventStatusId = 1, HostId = 2 }
    ];

    public static readonly List<Location> Locations = 
    [
        new()
        {
            CityName = "Kyiv", StreetName = "Bohdana Khmel'nyts'koho St", BuildingNumber = "5",
            Latitude = 50.44486678055781, Longitude = 30.518478985913852
        },
        new()
        {
            CityName = "Odesa", StreetName = "Marazliivska St", BuildingNumber = "0",
            Latitude = 46.47930732632679, Longitude = 30.75641379573275
        },
    ];

    public static readonly List<HostDetails> HostDetails = 
    [
        new() { Id = 1, Description = "Lorem ipsum host 1", RowAmount = 8, SeatsInRow = 12, IsDividedBySeats = true },
        new() { Id = 2, Description = "Lorem ipsum host 2", RowAmount = 1, SeatsInRow = 120, IsDividedBySeats = false }
    ];

    public static readonly List<HostType> HostTypes = 
    [
        new() { Name = "Theatre" },
        new() { Name = "Concert hall" },
        new() { Name = "Club" }
    ];

    public static readonly List<Host> Hosts = 
    [
        new() 
        { 
            Name = "National Academic Drama Theater named after Lesya Ukrainka", 
            LocationId = 1, HostTypeId = 1 
        },
        new() 
        { 
            Name = "Rakushka", 
            LocationId = 2, HostTypeId = 2 
        }
    ];

    public static readonly List<Client> Clients = 
    [
        new() 
        { 
            Name = "Client1", Surname = "SurClient1", 
            Email = "tempemail1@gmail.com", PhoneNumber = "+38095000001" 
        },
        new()
        {
            Name = "Client2", Surname = "SurClient2", 
            Email = "tempemail2@gmail.com", PhoneNumber = "+38095000002"
        },
    ];

    public static readonly List<Ticket> Tickets = [];

    static DevelopData()
    {
        FillTickets();
    }
    
    private static void FillTickets()
    {
        // Ticket creation with defining ticket type based on distance from scene
        for (int i = 1; i <= HostDetails[0].RowAmount; i++)
        {
            for (int j = 1; j <= HostDetails[0].SeatsInRow; j++)
            {
                int ticketType = j switch
                {
                    >= 1 and < 3 => 3, 
                    >= 3 and < 5 => 2,
                    >= 6 and <= 8 => 1,
                    _ => 1
                };
                Tickets.Add(new() { RowNum = i, SeatNum = j, TicketStatusId = 1, TicketTypeId = ticketType });
            }
        }

        for (int i = 1; i <= HostDetails[1].SeatsInRow; i++)
        {
            Tickets.Add(new() { RowNum = 1, SeatNum = i, TicketStatusId = 1, TicketTypeId = 3 });
        }
    }

    public static async Task<WebApplication> SeedData(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        await dbContext.Database.EnsureCreatedAsync();

        if (!await IsFilled(dbContext))
        {
            await PopulateDbContext(dbContext);
        }

        return app;
    }
    
    private static async Task<bool> IsFilled(AppDbContext dbContext)
    {
        bool result = await dbContext.Clients.AnyAsync() &&
                      await dbContext.Events.AnyAsync() &&
                      await dbContext.EventDetails.AnyAsync() &&
                      await dbContext.EventGenres.AnyAsync() &&
                      await dbContext.EventStatuses.AnyAsync() &&
                      await dbContext.EventTypes.AnyAsync() &&
                      await dbContext.Hosts.AnyAsync() &&
                      await dbContext.HostDetails.AnyAsync() &&
                      await dbContext.HostTypes.AnyAsync() &&
                      await dbContext.Locations.AnyAsync() &&
                      await dbContext.Tickets.AnyAsync() &&
                      await dbContext.TicketStatuses.AnyAsync() &&
                      await dbContext.TicketTypes.AnyAsync();

        return result;
    }

    private static async Task PopulateDbContext(AppDbContext dbContext)
    {
        await dbContext.EventStatuses.AddRangeAsync(DevelopData.EventStatuses);
        await dbContext.HostTypes.AddRangeAsync(DevelopData.HostTypes);
        await dbContext.Locations.AddRangeAsync(DevelopData.Locations);
        await dbContext.EventTypes.AddRangeAsync(DevelopData.EventTypes);
        await dbContext.TicketStatuses.AddRangeAsync(DevelopData.TicketStatuses);
        await dbContext.Clients.AddRangeAsync(DevelopData.Clients);
        await dbContext.SaveChangesAsync();
        
        await dbContext.Hosts.AddRangeAsync(DevelopData.Hosts);
        await dbContext.EventGenres.AddRangeAsync(DevelopData.EventGenres);
        await dbContext.SaveChangesAsync();
        
        await dbContext.HostDetails.AddRangeAsync(DevelopData.HostDetails);
        await dbContext.SaveChangesAsync();
        
        await dbContext.Events.AddRangeAsync(DevelopData.Events);
        await dbContext.SaveChangesAsync();
        
        await dbContext.EventDetails.AddRangeAsync(DevelopData.EventDetails);
        await dbContext.SaveChangesAsync();
        
        await dbContext.TicketTypes.AddRangeAsync(DevelopData.TicketTypes);
        await dbContext.SaveChangesAsync();
        
        await dbContext.Tickets.AddRangeAsync(DevelopData.Tickets);
        await dbContext.SaveChangesAsync();
    }
}

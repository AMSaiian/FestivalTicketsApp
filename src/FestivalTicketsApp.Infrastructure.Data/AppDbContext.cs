using System.Reflection;
using FestivalTicketsApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Client> Clients { get; set; } = default!;

    public DbSet<Host> Hosts { get; set; } = default!;

    public DbSet<HostDetails> HostDetails { get; set; } = default!;

    public DbSet<HostType> HostTypes { get; set; } = default!;

    public DbSet<Location> Locations { get; set; } = default!;

    public DbSet<Event> Events { get; set; } = default!;

    public DbSet<EventDetails> EventDetails { get; set; } = default!;

    public DbSet<EventType> EventTypes { get; set; } = default!;

    public DbSet<EventGenre> EventGenres { get; set; } = default!;

    public DbSet<EventStatus> EventStatuses { get; set; } = default!;

    public DbSet<Ticket> Tickets { get; set; } = default!;

    public DbSet<TicketType> TicketTypes { get; set; } = default!;

    public DbSet<TicketStatus> TicketStatuses { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
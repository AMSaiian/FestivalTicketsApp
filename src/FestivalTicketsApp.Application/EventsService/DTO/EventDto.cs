namespace FestivalTicketsApp.Application.EventsService.DTO;

public record EventDto(int Id, string Title, DateTime StartDate, string? HostName);
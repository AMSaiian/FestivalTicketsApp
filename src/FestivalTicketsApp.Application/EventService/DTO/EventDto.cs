namespace FestivalTicketsApp.Application.EventService.DTO;

public record EventDto(int Id, string Title, DateTime StartDate, string? HostName);
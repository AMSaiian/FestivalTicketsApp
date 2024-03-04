namespace FestivalTicketsApp.Application.EventService.DTO;

public record EventWithDetailsDto(
    int Id,
    string Title,
    DateTime StartDate,
    int HostId,
    string HostName,
    string Description,
    int Duration);
namespace FestivalTicketsApp.Application.HostService.DTO;

public record HostWithDetailsDto(
    int Id, 
    string Name, 
    string Description,
    LocationDto Location);
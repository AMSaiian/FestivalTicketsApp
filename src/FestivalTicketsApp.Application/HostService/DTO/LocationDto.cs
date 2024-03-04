namespace FestivalTicketsApp.Application.HostService.DTO;

public record LocationDto(
    int Id,
    string CityName,
    string StreetName,
    string BuildingNumber,
    double Latitude,
    double Longitude
);
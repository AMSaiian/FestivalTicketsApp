namespace FestivalTicketsApp.Core.Entities;

public class Location : BaseEntity
{
    public string CityName { get; set; } = default!;

    public string StreetName { get; set; } = default!;

    public string BuildingNumber { get; set; } = default!;
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }

    public List<Host> Hosts { get; set; } = [];
}
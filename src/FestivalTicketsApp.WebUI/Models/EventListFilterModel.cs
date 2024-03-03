namespace FestivalTicketsApp.WebUI.Models;

public class EventListFilterModel
{
    public DateTime? StartDate { get; set; } = default;
    
    public DateTime? EndDate { get; set; } = default;
    
    public int? GenreId { get; set; } = default;
    
    public string? CityName { get; set; } = "Kyiv";
}

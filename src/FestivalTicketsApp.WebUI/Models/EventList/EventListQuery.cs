namespace FestivalTicketsApp.WebUI.Models.EventList;

public class EventListQuery
{
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public int? GenreId { get; set; }
    
    public string? CityName { get; set; } = "Kyiv";
}

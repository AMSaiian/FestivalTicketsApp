namespace FestivalTicketsApp.WebUI.Models.Event;

public class EventListQuery
{
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public int? GenreId { get; set; }
    
    public string CityName { get; set; } = RequestDefaults.CityName;

    public int PageNum { get; set; } = RequestDefaults.PageNum;

    public int PageSize { get; set; } = RequestDefaults.PageSize;
}

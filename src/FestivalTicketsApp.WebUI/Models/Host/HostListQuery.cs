namespace FestivalTicketsApp.WebUI.Models.Host;

public class HostListQuery
{
    public string? CityName { get; set; } = RequestDefaults.CityName;
    
    public int PageNum { get; set; } = RequestDefaults.PageNum;

    public int PageSize { get; set; } = RequestDefaults.PageSize;
}
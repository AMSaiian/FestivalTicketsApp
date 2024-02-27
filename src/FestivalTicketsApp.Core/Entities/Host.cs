namespace FestivalTicketsApp.Core.Entities;

public class Host : BaseEntity
{
    public string Name { get; set; } = default!;
    
    public int HostTypeId { get; set; }
    
    public int LocationId { get; set; }

    public HostType HostType { get; set; } = default!;

    public Location Location { get; set; } = default!;
}
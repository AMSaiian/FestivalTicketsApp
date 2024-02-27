namespace FestivalTicketsApp.Core.Entities;

public class HostType : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<Host> Hosts { get; set; } = [];
}
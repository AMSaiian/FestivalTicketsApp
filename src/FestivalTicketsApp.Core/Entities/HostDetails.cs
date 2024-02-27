namespace FestivalTicketsApp.Core.Entities;

public class HostDetails : BaseEntity
{
    public string Description { get; set; } = default!;

    public int RowAmount { get; set; }

    public int SeatsInRow { get; set; }

    public bool IsDividedBySeats { get; set; }
}
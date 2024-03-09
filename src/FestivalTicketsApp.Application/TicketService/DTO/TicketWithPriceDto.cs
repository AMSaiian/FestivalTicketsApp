namespace FestivalTicketsApp.Application.TicketService;

public record TicketWithPriceDto(int Id, int RowNum, int SeatNum, string TypeName, decimal Price);
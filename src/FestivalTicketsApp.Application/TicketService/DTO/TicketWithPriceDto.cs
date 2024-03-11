namespace FestivalTicketsApp.Application.TicketService.DTO;

public record TicketWithPriceDto(int Id, int? RowNum, int? SeatNum, string TypeName, decimal Price);
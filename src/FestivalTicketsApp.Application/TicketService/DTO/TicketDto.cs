namespace FestivalTicketsApp.Application.TicketService;

public record TicketDto(int Id, int RowNum, int SeatNum, string Status, int TicketTypeId);
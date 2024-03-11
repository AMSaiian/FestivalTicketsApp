namespace FestivalTicketsApp.Application.TicketService.DTO;

public record TicketDto(int Id, int? RowNum, int? SeatNum, string Status, int TicketTypeId);
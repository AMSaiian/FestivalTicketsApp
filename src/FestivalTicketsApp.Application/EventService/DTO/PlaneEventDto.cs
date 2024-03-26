namespace FestivalTicketsApp.Application.EventService.DTO;

public record PlaneEventDto(string Title,
                             int GenreId,
                             int HostId,
                             string Description,
                             DateTime StartTime,
                             int Duration,
                             List<CreateTicketTypeDto> TicketTypes);

public record CreateTicketTypeDto(string Name,
                                  decimal Price,
                                  List<int> RowsWithTicketType);
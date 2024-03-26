using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application.ClientService;

public interface IClientService
{
    Task<Result<bool>> IsInFavourite(int eventId, int clientId);

    Task<Result<object>> ChangeFavouriteStatus(int eventId, int clientId, bool newStatus);
}
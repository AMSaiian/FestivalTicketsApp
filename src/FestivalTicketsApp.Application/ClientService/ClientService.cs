using FestivalTicketsApp.Core.Entities;
using FestivalTicketsApp.Infrastructure.Data;
using FestivalTicketsApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace FestivalTicketsApp.Application.ClientService;

public class ClientService(AppDbContext context) : IClientService
{
    private readonly AppDbContext _context = context;
    
    public async Task<Result<bool>> IsInFavourite(int eventId, int clientId)
    {
        Client? clientEntity = await _context.Clients
            .AsNoTracking()
            .Include(c => c.FavouriteEvents)
            .FirstOrDefaultAsync(c => c.Id == clientId);

        if (clientEntity is null)
            return Result<bool>.Failure(DomainErrors.RelatedEntityNotFound);

        bool result = clientEntity.FavouriteEvents.Exists(fe => fe.Id == eventId);

        return Result<bool>.Success(result);
    }

    public async Task<Result<object>> ChangeFavouriteStatus(int eventId, int clientId, bool newStatus)
    {
        Client? clientEntity = await _context.Clients
            .Include(c => c.FavouriteEvents)
            .FirstOrDefaultAsync(c => c.Id == clientId);

        Event? eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        
        if (clientEntity is null || eventEntity is null)
            return Result<object>.Failure(DomainErrors.RelatedEntityNotFound);

        bool isInFavourite = clientEntity.FavouriteEvents.Exists(fe => fe.Id == eventId);
        
        if (isInFavourite == newStatus)
            return Result<object>.Failure(DomainErrors.SameFavouriteStatusSet);

        if (newStatus)
            clientEntity.FavouriteEvents.Add(eventEntity);
        else
            clientEntity.FavouriteEvents.Remove(eventEntity);

        await _context.SaveChangesAsync();
        
        return Result<object>.Success(null);
    }
}
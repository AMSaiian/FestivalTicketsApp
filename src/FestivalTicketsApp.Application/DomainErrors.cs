using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application;

public static class DomainErrors
{
    public static readonly Error EntityNotFound = new(nameof(EntityNotFound));

    public static readonly Error RelatedEntityNotFound = new(nameof(RelatedEntityNotFound));

    public static readonly Error QueryEmptyResult = new(nameof(QueryEmptyResult));

    public static readonly Error SameTicketStatusSet = new(nameof(SameTicketStatusSet));

    public static readonly Error SameFavouriteStatusSet = new(nameof(SameFavouriteStatusSet));

    public static readonly Error OutOfDateTicketStatusCantBeChanged = new(nameof(OutOfDateTicketStatusCantBeChanged));

    public static readonly Error SoldTicketCantBeSoldAgain = new(nameof(SoldTicketCantBeSoldAgain));

    public static readonly Error AnonymousTicketBuy = new(nameof(AnonymousTicketBuy));

    public static readonly Error TicketTypeMappingMissmatchHall = new(nameof(TicketTypeMappingMissmatchHall));
}
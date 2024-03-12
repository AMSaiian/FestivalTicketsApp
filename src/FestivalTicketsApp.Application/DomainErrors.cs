using FestivalTicketsApp.Shared;

namespace FestivalTicketsApp.Application;

public static class DomainErrors
{
    public static readonly Error EntityNotFound = new(nameof(EntityNotFound));

    public static readonly Error RelatedEntityNotFound = new(nameof(RelatedEntityNotFound));

    public static readonly Error QueryEmptyResult = new(nameof(QueryEmptyResult));

    public static readonly Error SameTicketStatusSet = new(nameof(SameTicketStatusSet));
}
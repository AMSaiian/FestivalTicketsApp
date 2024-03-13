namespace FestivalTicketsApp.Shared;

public record Paginated<TValue>(List<TValue> Value, int CurrentPage, int NextPagesAmount);
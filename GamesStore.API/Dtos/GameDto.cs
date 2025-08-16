namespace GamesStore.API.Dtos;

public record class GameDto(
    int Id,
    string Name,
    string Genere,
    decimal Price,
    DateOnly ReleaseDate
);

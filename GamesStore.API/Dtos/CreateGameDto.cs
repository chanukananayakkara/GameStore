namespace GamesStore.API.Dtos;

public record class CreateGameDto(
    string Name,
    string Genere,
    decimal Price,
    DateOnly ReleaseDate
);

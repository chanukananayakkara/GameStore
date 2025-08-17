using System;
using GamesStore.API.Dtos;

namespace GamesStore.API.Endpoints;

public static class GamesEndpoints
{
    const String GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new(
        1,
        "Street Fiter II",
        "Fighting",
        19.99M,
        new DateOnly(1992,07,15)
    ),
    new(
        2,
        "Final Fantacy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010,09,30)
    ),
    new(
        3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022,09,27)
    )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();
            //GET /games
        group.MapGet("/", () => games);

        //GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });
        

        //PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto UpdatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            
            games[index] = new GameDto(
                id,
                UpdatedGame.Name,
                UpdatedGame.Genre,
                UpdatedGame.Price,
                UpdatedGame.ReleaseDate
            );

            return Results.NoContent();

        });

        //DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }

}

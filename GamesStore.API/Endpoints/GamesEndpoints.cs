using System;
using GamesStore.API.Data;
using GamesStore.API.Dtos;
using GamesStore.API.Entities;
using GamesStore.API.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GamesStore.API.Endpoints;

public static class GamesEndpoints
{
    const String GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();

        //GET /games
        group.MapGet("/", (GameStoreContext dbContext) =>
                                    dbContext.Games
                                                .Include(game => game.Genre)
                                                .Select(game => game.ToGameSummaryDto())
                                                .AsNoTracking());

        

        //GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);



        //POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });


        

        //PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto UpdatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(UpdatedGame.ToEntity(id));

            dbContext.SaveChanges();

            return Results.NoContent();

        });




        //DELETE /games/1
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDelete();

            return Results.NoContent();
        });

        return group;
    }

}

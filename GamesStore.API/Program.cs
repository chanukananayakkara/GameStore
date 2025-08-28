using GamesStore.API.Data;
using GamesStore.API.Dtos;
using GamesStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);


var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

//initial sample request
app.MapGet("/", () => "Hello World!");

app.Run();

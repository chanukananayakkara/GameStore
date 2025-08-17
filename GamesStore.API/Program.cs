using GamesStore.API.Data;
using GamesStore.API.Dtos;
using GamesStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);

app.MapGamesEndpoints();

//initial sample request
app.MapGet("/", () => "Hello World!");

app.Run();

using GamesStore.API.Dtos;
using GamesStore.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoints();

//initial sample request
app.MapGet("/", () => "Hello World!");

app.Run();

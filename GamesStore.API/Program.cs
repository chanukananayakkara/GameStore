using GamesStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
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

//GET /games
app.MapGet("games", () => games);

//GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));

//initial sample request
app.MapGet("/", () => "Hello World!");

app.Run();

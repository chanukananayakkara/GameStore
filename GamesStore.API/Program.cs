using GamesStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const String GetGameEndpointName = "GetGame";

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
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetGameEndpointName);

//POST /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genere,
        newGame.Price,
        newGame.ReleaseDate);

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

//initial sample request
app.MapGet("/", () => "Hello World!");

app.Run();

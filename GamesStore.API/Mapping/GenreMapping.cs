using System;
using GamesStore.API.Dtos;
using GamesStore.API.Entities;

namespace GamesStore.API.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }

}

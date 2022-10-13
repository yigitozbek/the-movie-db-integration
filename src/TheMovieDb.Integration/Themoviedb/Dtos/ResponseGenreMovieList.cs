using Newtonsoft.Json;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseGenreMovieList : ResponseBaseTheMovieDbModel
{
    [JsonProperty("genres")] 
    public List<ResponseGenreMovieListGenre> Genres { get; set; } = new();
}
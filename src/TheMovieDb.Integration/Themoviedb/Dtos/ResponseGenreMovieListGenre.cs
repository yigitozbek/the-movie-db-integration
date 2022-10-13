using Newtonsoft.Json;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseGenreMovieListGenre : ResponseBaseTheMovieDbModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")] 
    public string Name { get; set; } = string.Empty;
}
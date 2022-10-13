using Newtonsoft.Json;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseMovieGetDetailsGenre : ResponseBaseTheMovieDbModel
{

    [JsonProperty("id")]
    public int id { get; set; }

    [JsonProperty("Name")]
    public string name { get; set; }
}
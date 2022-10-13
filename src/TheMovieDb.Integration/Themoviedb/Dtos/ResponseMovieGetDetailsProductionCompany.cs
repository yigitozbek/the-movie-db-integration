using Newtonsoft.Json;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseMovieGetDetailsProductionCompany : ResponseBaseTheMovieDbModel
{

    [JsonProperty("id")]
    public int Id { get; set; }
    public string? logo_path { get; set; }
    public string name { get; set; }
    public string origin_country { get; set; }
}
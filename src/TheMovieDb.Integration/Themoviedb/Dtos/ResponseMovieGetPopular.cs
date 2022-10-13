using Newtonsoft.Json;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseMovieGetPopular : ResponseBaseTheMovieDbModel
{

    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("results")]
    public List<ResponseMovieGetPopularResult> Results { get; set; } = new();

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    [JsonProperty("total_results")]
    public int TotalResults { get; set; }

}
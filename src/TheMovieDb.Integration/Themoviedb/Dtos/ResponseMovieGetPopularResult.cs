using Newtonsoft.Json;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseMovieGetPopularResult : ResponseBaseTheMovieDbModel
{

    [JsonProperty("id")]
    public int Id { get; set; }


    [JsonProperty("adult")]
    public bool Adult { get; set; }


    [JsonProperty("backdrop_path")]
    public string? BackdropPath { get; set; }


    [JsonProperty("genre_ids")]
    public List<int> GenreIds { get; set; } = new();


    [JsonProperty("original_language")]
    public string OriginalLanguage { get; set; } = string.Empty;


    [JsonProperty("original_title")]
    public string OriginalTitle { get; set; } = string.Empty;


    [JsonProperty("overview")]
    public string Overview { get; set; } = string.Empty;


    [JsonProperty("popularity")]
    public double Popularity { get; set; }


    [JsonProperty("poster_path")]
    public string? PosterPath { get; set; }


    [JsonProperty("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;


    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;


    [JsonProperty("video")]
    public bool Video { get; set; }


    [JsonProperty("vote_average")]
    public double VoteAverage { get; set; }


    [JsonProperty("vote_count")]
    public int VoteCount { get; set; }
}
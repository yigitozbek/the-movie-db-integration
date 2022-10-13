using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseMovieGetDetails : ResponseBaseTheMovieDbModel
{

    [JsonProperty("id")]
    public int Id { get; set; }


    [JsonProperty("adult")]
    public bool Adult { get; set; }


    [JsonProperty("backdrop_path")]
    public string? BackdropPath { get; set; }


    [JsonProperty("belongs_to_collection")]
    public object? BelongsToCollection { get; set; }


    [JsonProperty("budget")]
    public int Budget { get; set; }


    [JsonProperty("genres")]
    public List<ResponseMovieGetDetailsGenre> Genres { get; set; }


    [JsonProperty("homepage")]
    public string? Homepage { get; set; }


    [JsonProperty("imdb_id")]
    [MinLength(9)]
    [MaxLength(9)]
    [RegularExpression(@"^tt[0-9]{7}")]
    public string? ImdbId { get; set; }


    [JsonProperty("original_language")]
    public string OriginalLanguage { get; set; } = string.Empty;


    [JsonProperty("original_title")]
    public string OriginalTitle { get; set; } = string.Empty;


    [JsonProperty("overview")]
    public string? Overview { get; set; }


    [JsonProperty("popularity")]
    public double Popularity { get; set; }


    [JsonProperty("poster_path")]
    public string? PosterPath { get; set; }


    [JsonProperty("production_companies")]
    public List<ResponseMovieGetDetailsProductionCompany> production_companies { get; set; }


    [JsonProperty("production_countries")]
    public List<ResponseMovieGetDetailsProductionCountry> production_countries { get; set; }


    [JsonProperty("release_date")]
    public DateTime ReleaseDate { get; set; }


    [JsonProperty("revenue")]
    public int revenue { get; set; }


    [JsonProperty("runtime")]
    public int? Runtime { get; set; }


    [JsonProperty("spoken_languages")]
    public List<ResponseMovieGetDetailsSpokenLanguage> SpokenLanguages { get; set; }


    [JsonProperty("status")]
    public string Status { get; set; }


    [JsonProperty("title")]
    public string? TagLine { get; set; }


    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;


    [JsonProperty("video")]
    public bool Video { get; set; }


    [JsonProperty("vote_average")]
    public double VoteAverage { get; set; }


    [JsonProperty("vote_count")]
    public int VoteCount { get; set; }
}
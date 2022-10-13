namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseMovieGetDetailsProductionCountry : ResponseBaseTheMovieDbModel
{
    public string iso_3166_1 { get; set; }
    public string name { get; set; }
}
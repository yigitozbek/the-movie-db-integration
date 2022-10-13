namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class ResponseMovieGetDetailsSpokenLanguage : ResponseBaseTheMovieDbModel
{
    public string english_name { get; set; }
    public string iso_639_1 { get; set; }
    public string name { get; set; }
}
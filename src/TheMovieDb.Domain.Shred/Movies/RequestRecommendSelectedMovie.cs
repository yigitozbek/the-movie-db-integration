using Yella.Domain.Dto;

namespace TheMovieDb.Domain.Shared.Movies;

public class RequestMovieRecommendSelectedMovie : EntityDto
{
    public RequestMovieRecommendSelectedMovie(Guid movieId, List<string> emails)
    {
        MovieId = movieId;
        Emails = emails;
    }

    public Guid MovieId { get; set; }

    public List<string> Emails { get; set; }
}

public class RequestMovieRateMovie : EntityDto
{
    public RequestMovieRateMovie(Guid movieId, short star, string text)
    {
        MovieId = movieId;
        Star = star;
        Text = text;
    }

    public Guid MovieId { get; set; }

    public short Star { get; set; }
    public string Text { get; set; }

}
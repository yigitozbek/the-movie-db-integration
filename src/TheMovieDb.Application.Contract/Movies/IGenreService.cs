using Yella.Utilities.Results;

namespace TheMovieDb.Application.Contract.Movies;

public interface IGenreService
{
    Task<IResult> AddGenreFromTheMovieDbAsync();
}
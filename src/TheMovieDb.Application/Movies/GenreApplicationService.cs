using TheMovieDb.Application.Contract.Movies;
using TheMovieDb.Domain.Modules.Movies.Managers;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Movies;

public class GenreApplicationService : IGenreService
{

    private readonly GenreManager _genreManager;

    public GenreApplicationService(GenreManager genreManager)
    {
        _genreManager = genreManager;
    }

    public async Task<IResult> AddGenreFromTheMovieDbAsync()
    {
        var result = await _genreManager.CreateOrUpdateGenreFromTheMovieDbAsync();
        return result;
    }

}
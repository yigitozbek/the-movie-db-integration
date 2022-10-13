using TheMovieDb.Integration.Themoviedb;
using TheMovieDb.Integration.Themoviedb.Dtos;
using Yella.Aspect.PostSharp.Transactions;
using Yella.EntityFrameworkCore;
using Yella.EntityFrameworkCore.Extensions;
using Yella.Utilities.Results;

namespace TheMovieDb.Domain.Modules.Movies.Managers;

public class GenreManager : DomainService<GenreManager>
{
    private readonly TheMovieDbIntegrator _theMovieDbIntegrator;
    private readonly IRepository<Genre> _genreRepository;

    public GenreManager(TheMovieDbIntegrator theMovieDbIntegrator, IRepository<Genre> genreRepository)
    {
        _theMovieDbIntegrator = theMovieDbIntegrator;
        _genreRepository = genreRepository;
    }

    /// <summary>
    /// Themoviedb entegrasyonundaki /genre/movie/list servisindeki gelen response'u veri tabanına kaydeder.
    /// </summary>
    /// <returns></returns>
    [TransactionAspect(AspectPriority = 1)]
    public async Task<IResult> CreateOrUpdateGenreFromTheMovieDbAsync()
    {
        var resultGenreMovie = await _theMovieDbIntegrator.GenreMovieListAsync(new());
        var genres = new List<Genre>();

        if (!resultGenreMovie.Success)
        {
            return new ErrorResult(resultGenreMovie.Message);
        }

        foreach (var dataGenre in resultGenreMovie.Data.Genres)
        {

            var query = await _genreRepository.FirstOrDefaultAsync(x => x.IntegrationId == dataGenre.Id);

            genres.Add(query != null
                ? UpdateForCreateOrUpdateGenreFromTheMovieDb(query, dataGenre)
                : AddForCreateOrUpdateGenreFromTheMovieDb(dataGenre));
        }

        var insert = genres.Where(x => x.Id == Guid.Empty);

        var update = genres.Where(x => x.Id != Guid.Empty);

        if (insert.Any())
        {
            var resultInsert = await _genreRepository.AddRangeAsync(insert);
        }

        if (update.Any())
        {
            var resultUpdate = await _genreRepository.UpdateRangeAsync(update);
        }

        return new SuccessResult();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Genre AddForCreateOrUpdateGenreFromTheMovieDb(ResponseGenreMovieListGenre input) => new(name: input.Name, integrationId: input.Id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    private Genre UpdateForCreateOrUpdateGenreFromTheMovieDb(Genre model, ResponseGenreMovieListGenre input)
    {
        model.Name = input.Name;
        return model;
    }

}


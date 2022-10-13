using System.Globalization;
using TheMovieDb.Domain.Shared.Mails.Smtp;
using TheMovieDb.Domain.Shared.Movies;
using TheMovieDb.Integration.Themoviedb;
using TheMovieDb.Integration.Themoviedb.Dtos;
using Yella.Aspect.PostSharp.Transactions;
using Yella.AutoMapper.Extensions;
using Yella.EntityFrameworkCore;
using Yella.EntityFrameworkCore.Extensions;
using Yella.Utilities.Results;

namespace TheMovieDb.Domain.Modules.Movies.Managers;

public class MovieManager : DomainService<MovieManager>
{
    private readonly TheMovieDbIntegrator _theMovieDbIntegrator;
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRepository<MovieRate> _movieRateRepository;
    private readonly IRepository<Genre> _genreRepository;
    private readonly IMailService _mailService;

    public MovieManager(TheMovieDbIntegrator theMovieDbIntegrator, IRepository<Movie> movieRepository, IRepository<Genre> genreRepository, IMailService mailService, IRepository<MovieRate> movieRateRepository)
    {
        _theMovieDbIntegrator = theMovieDbIntegrator;
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
        _mailService = mailService;
        _movieRateRepository = movieRateRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [TransactionAspect(AspectPriority = 1)]
    public async Task<IResult> CreateOrUpdateMoviePopularListFromTheMovieDbAsync()
    {
        var resultMoviePopular = await _theMovieDbIntegrator.MoviePopularAsync(new RequestMovieGetPopular(page: 1));

        var genres = await _genreRepository.GetListAsync();

        var movies = new List<Movie>();

        if (!resultMoviePopular.Success)
        {
            return new ErrorResult(resultMoviePopular.Message);
        }

        foreach (var dataResult in resultMoviePopular.Data.Results)
        {
            var query = await _movieRepository.FirstOrDefaultAsync(x => x.IntegrationId == dataResult.Id);

            movies.Add(query != null
                ? UpdateForMoviePopularListFromTheMovieDb(query, dataResult, genres)
                : AddForMoviePopularListFromTheMovieDb(dataResult, genres));
        }

        var insert = movies.Where(x => x.Id == Guid.Empty).ToList();

        var update = movies.Where(x => x.Id != Guid.Empty).ToList();

        if (insert.Any())
        {
            var resultInsert = await _movieRepository.AddRangeAsync(insert);
        }

        if (update.Any())
        {
            var resultUpdate = await _movieRepository.UpdateRangeAsync(update);
        }

        return new SuccessResult();
    }

    /// <summary>
    /// CreateOrUpdateMoviePopularListFromTheMovieDbAsync methodu için yazılmıştır. Gelen Listedeki kayıtı eklemek için kullanılır.
    /// </summary>
    /// <returns></returns>
    private Movie AddForMoviePopularListFromTheMovieDb(ResponseMovieGetPopularResult input, IEnumerable<Genre> genres)
    {
        var culture = new CultureInfo("tr-TR");

        var movieGenre = input.GenreIds.Select(inputGenreId => new MovieGenre() { GenreId = genres.FirstOrDefault(x => x.IntegrationId == inputGenreId)!.Id }).ToList();

        var movie = new Movie
        {
            Adult = input.Adult,
            BackdropPath = input.BackdropPath,
            Genres = movieGenre,
            IntegrationId = input.Id,
            OriginalLanguage = input.OriginalLanguage,
            OriginalTitle = input.OriginalTitle,
            Overview = input.Overview,
            Popularity = input.Popularity,
            PosterPath = input.PosterPath,
            ReleaseDate = Convert.ToDateTime(input.ReleaseDate, culture),
            Title = input.Title,
            Video = input.Video,
            VoteAverage = input.VoteAverage,
            VoteCount = input.VoteCount
        };

        return movie;
    }

    /// <summary>
    /// CreateOrUpdateMoviePopularListFromTheMovieDbAsync methodu için yazılmıştır. Gelen Listedeki kayıtı güncellemek için kullanılır.
    /// </summary>
    /// <returns></returns>
    private Movie UpdateForMoviePopularListFromTheMovieDb(Movie movie, ResponseMovieGetPopularResult input, IEnumerable<Genre> genres)
    {
        var culture = new CultureInfo("tr-TR");

        var movieGenre = input.GenreIds.Select(inputGenreId => new MovieGenre() { GenreId = genres.FirstOrDefault(x => x.IntegrationId == inputGenreId)!.Id }).ToList();

        movie.Adult = input.Adult;
        movie.BackdropPath = input.BackdropPath;
        movie.Genres = movieGenre;
        movie.IntegrationId = input.Id;
        movie.OriginalLanguage = input.OriginalLanguage;
        movie.OriginalTitle = input.OriginalTitle;
        movie.Overview = input.Overview;
        movie.Popularity = input.Popularity;
        movie.PosterPath = input.PosterPath;
        movie.ReleaseDate = Convert.ToDateTime(input.ReleaseDate, culture);
        movie.Title = input.Title;
        movie.Video = input.Video;
        movie.VoteAverage = input.VoteAverage;
        movie.VoteCount = input.VoteCount;

        return movie;
    }


    public async Task<IResult> RecommendSelectedMovieAsync(RequestMovieRecommendSelectedMovie input)
    {

        var query = await _movieRepository.GetAsync(input.MovieId);

        var result = _mailService.SendMail(new($"{query.Title} ....", "Recommended", input.Emails));

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult();
    }

    public async Task<IResult> RateMovieAsync(RequestMovieRateMovie input)
    {

        var map = input.ToMapper<MovieRate>();

        var result = await _movieRateRepository.AddAsync(map);

        if (!result.Success)
        {
            return new ErrorResult(result.Message);
        }

        return new SuccessResult();
    }

}


using Microsoft.Extensions.Hosting;
using TheMovieDb.Domain.Modules.Movies.Managers;

namespace TheMovieDb.BackgroundWorker;

public class RepaitingService : BackgroundService
{

    private readonly MovieManager _movieManager;
    private readonly GenreManager _genreManager;

    public RepaitingService(MovieManager movieManager, GenreManager genreManager)
    {
        _movieManager = movieManager;
        _genreManager = genreManager;
    }

    private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromMinutes(60));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _periodicTimer.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested)
        {
            await _genreManager.CreateOrUpdateGenreFromTheMovieDbAsync();
            await _movieManager.CreateOrUpdateMoviePopularListFromTheMovieDbAsync();
        }
    }

}

using TheMovieDb.Integration.Models;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class RequestBaseTheMovieDbModel
{
    public RequestBaseTheMovieDbModel()
    {
        api_key = IntegrationConfiguration.GetSection("themoviedb:ApiKey");
    }

    public string api_key { get; set; }

}
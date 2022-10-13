using TheMovieDb.Integration.Models;
using TheMovieDb.Integration.Services;
using RestSharp;

namespace TheMovieDb.Integration.Themoviedb.Base;

public abstract class BaseTheMovieDbIntegrator<TTheMovieDbManager> : IntegrationService<TTheMovieDbManager>, IIntegrationService
    where TTheMovieDbManager : IntegrationService<TTheMovieDbManager>
{


    protected BaseTheMovieDbIntegrator()
    {
        BaseURL = IntegrationConfiguration.GetSection("themoviedb:BaseURL");
    }

    protected string BaseURL { get; set; }

    /// <summary>
    /// TheMovieDb servis entegrasyonu için kullanılan base Headları tekrar tekrar yazılmaması için.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="method"></param>
    /// <returns></returns>
    protected RestRequest GetRestRequest(string url, Method method)
    {
        var request = new RestRequest(url, method);
        request.AddHeader("Accept", "application/json;charset=utf-8");
        request.AddHeader("Content-Type", "application/json");
        return request;
    }

 








}


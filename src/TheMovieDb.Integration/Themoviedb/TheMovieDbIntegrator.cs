using TheMovieDb.Integration.Extensions;
using TheMovieDb.Integration.Themoviedb.Dtos;
using Yella.Utilities.Results;
using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TheMovieDb.Integration.Themoviedb.Base;
using RestSharp;

namespace TheMovieDb.Integration.Themoviedb;

/// <summary>
/// TheMovieDb ile entegrasyon sağlar. 
/// </summary>
public class TheMovieDbIntegrator : BaseTheMovieDbIntegrator<TheMovieDbIntegrator>
{

    /// <summary>
    /// TMDB'deki güncel popüler filmlerin bir listesini alır. Bu liste günlük olarak güncellenir.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<IDataResult<ResponseMovieGetPopular>> MoviePopularAsync(RequestMovieGetPopular request)
    {

        if (request == null) throw new ArgumentNullException(nameof(request));

        var url = BaseURL + "movie/popular";

        var response = await RestClient.ExecuteAsync(GetRestRequest(url.ModelToQueryString(request), Method.Get));

        if (response.StatusCode != HttpStatusCode.OK)
        {
            Logger.Log(LogLevel.Error, response.StatusCode.ToString());
            return new ErrorDataResult<ResponseMovieGetPopular>("Bir hata oluştu.");
        }

        var responseBody = response.Content!;

        var result = JsonConvert.DeserializeObject<ResponseMovieGetPopular>(responseBody)!;

        return new SuccessDataResult<ResponseMovieGetPopular>(result);
    }

    /// <summary>
    /// TMDB'deki güncel popüler filmlerin bir listesini alır.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<IDataResult<ResponseMovieGetDetails>> MovieDetailsAsync(RequestMovieGetDetails request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var url = BaseURL + $"movie/{request.Id}";

        var response = await RestClient.ExecuteAsync(GetRestRequest(url.ModelToQueryString(request), Method.Get));

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return new ErrorDataResult<ResponseMovieGetDetails>("Bir hata oluştu.");
        }

        var responseBody = response.Content!;

        var result = JsonConvert.DeserializeObject<ResponseMovieGetDetails>(responseBody)!;

        return new SuccessDataResult<ResponseMovieGetDetails>(result);
    }

    /// <summary>
    /// TMDB'deki /genre/movie/list entegrasyonu. Filmler için resmi türlerin listesini alır.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<IDataResult<ResponseGenreMovieList>> GenreMovieListAsync(RequestGenreMovieList request)
    {

        if (request == null) throw new ArgumentNullException(nameof(request));

        var url = BaseURL + $"genre/movie/list";

        var response = await RestClient.ExecuteAsync(GetRestRequest(url.ModelToQueryString(request), Method.Get));

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return new ErrorDataResult<ResponseGenreMovieList>("Bir hata oluştu.");
        }

        var responseBody = response.Content!;

        var result = JsonConvert.DeserializeObject<ResponseGenreMovieList>(responseBody)!;

        return new SuccessDataResult<ResponseGenreMovieList>(result);
    }

}
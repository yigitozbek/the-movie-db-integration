using System.Web;

namespace TheMovieDb.Integration.Extensions;

internal static class TheMovieDbIntegrationExtension
{

    /// <summary>
    /// Parametrede verilen modeli gönderilen url'e QueryString olarak ekler
    /// </summary>
    /// <param name="url">İstekte bulunacağımız url</param>
    /// <param name="val">İstek atılacak url'e query string olarak eklenmesi istenen Model</param>
    /// <returns></returns>
    internal static string ModelToQueryString<T>(this string url, T val)
        where T : class
    {
        var properties = val.GetType()
            .GetProperties()
            .Where(p => p.GetValue(val, null) != null)
            .Select(p => p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(val, null)?.ToString()));

        var queryString = string.Join("&", properties.ToArray());

        return url + "?" + queryString;
    }

}
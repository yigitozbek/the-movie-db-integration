using Microsoft.Extensions.Configuration;

namespace TheMovieDb.Integration.Models;


/// <summary>
/// Integration kütüphanesi için hazırlandı.
/// </summary>
internal class IntegrationConfiguration
{

    private static IConfiguration? _configuration = null;

    /// <summary>
    /// Yollanılan Key'i integrations.json'ın içerisindeki değeri getirir.
    /// </summary>
    /// <param name="key">integrations.json'ın içerisindeki key</param>
    /// <returns></returns>
    internal static string GetSection(string key)
    {
        if (_configuration != null) return _configuration[key];

        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Directory where the json files are located
            .AddJsonFile("integrations.json", optional: false, reloadOnChange: true)
            .Build();

        return _configuration[key];
    }


}
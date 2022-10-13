using Microsoft.Extensions.Logging;
using RestSharp;
using Yella.Utilities.Extensions;

namespace TheMovieDb.Integration.Services;

public class IntegrationService<TIntegrationService>
    where TIntegrationService : IntegrationService<TIntegrationService>
{
    protected readonly ILogger<TIntegrationService> Logger = (ServiceActivator.GetScope()?.ServiceProvider?.GetService(typeof(ILogger<TIntegrationService>)) as ILogger<TIntegrationService>)!;

    protected RestClient RestClient = new();

}
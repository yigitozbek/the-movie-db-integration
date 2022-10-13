using Microsoft.Extensions.Logging;
using Yella.Utilities.Extensions;

namespace TheMovieDb.Domain;

public class DomainService<TDomainService>
    where TDomainService : DomainService<TDomainService>
{
    protected readonly ILogger<TDomainService> Logger = (ServiceActivator.GetScope()?.ServiceProvider.GetService(typeof(ILogger<TDomainService>))! as ILogger<TDomainService>)!;
}


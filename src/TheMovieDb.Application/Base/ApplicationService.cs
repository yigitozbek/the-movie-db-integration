using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Yella.Utilities.Extensions;

namespace TheMovieDb.Application.Base;

public class ApplicationService<TApplicationService>
    where TApplicationService : ApplicationService<TApplicationService>
{
    protected readonly ILogger<TApplicationService> Logger = (ServiceActivator.GetScope()!.ServiceProvider.GetService(typeof(ILogger<TApplicationService>)) as ILogger<TApplicationService>)!;
    protected readonly IConfiguration Configuration = (ServiceActivator.GetScope()!.ServiceProvider.GetService(typeof(IConfiguration)) as IConfiguration)!;
}
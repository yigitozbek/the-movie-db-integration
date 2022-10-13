using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class ProductionCountry : Entity<Guid>
{

    public string Iso { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int IntegrationId { get; set; }

}
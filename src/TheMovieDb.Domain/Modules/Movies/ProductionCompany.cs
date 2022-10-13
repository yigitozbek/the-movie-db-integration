using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class ProductionCompany : FullAuditedEntity<Guid>
{

    public string Name { get; set; } = string.Empty;

    public string LogoPath { get; set; } = string.Empty;

    public string OriginCountry { get; set; } = string.Empty;

    public int IntegrationId { get; set; }
}
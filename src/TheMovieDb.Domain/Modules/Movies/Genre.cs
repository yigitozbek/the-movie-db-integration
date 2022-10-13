using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class Genre : FullAuditedEntity<Guid>
{


    public Genre(string name, int integrationId)
    {
        Name = name;
        IntegrationId = integrationId;
    }

    public Genre()
    {

    }

    public string Name { get; set; } = string.Empty;

    public int IntegrationId { get; set; }

}
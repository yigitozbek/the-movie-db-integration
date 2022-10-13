using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class MovieProductionCompany : FullAuditedEntity<Guid>
{
    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie Movie { get; set; } = null!;

    public Guid ProductionCompanyId { get; set; }

    [ForeignKey(nameof(ProductionCompanyId))]
    public virtual ProductionCompany ProductionCompany { get; set; } = null!;

}
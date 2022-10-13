using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class MovieProductionCountry : FullAuditedEntity<Guid>
{

    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie Movie { get; set; } = null!;

    public Guid ProductionCountryId { get; set; }

    [ForeignKey(nameof(ProductionCountryId))]
    public virtual ProductionCountry ProductionCountry { get; set; } = null!;


}
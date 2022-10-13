using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class BelongsToCollection : FullAuditedEntity<Guid>
{

    public string Name { get; set; } = string.Empty;

    public string? PosterPath { get; set; }

    public string BackdropPath { get; set; } = string.Empty;

    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie Movie { get; set; } = null!;
}
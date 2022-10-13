using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class MovieSpokenLanguage : FullAuditedEntity<Guid>
{
    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie Movie { get; set; } = null!;


    public Guid SpokenLanguageId { get; set; }

    [ForeignKey(nameof(SpokenLanguageId))]
    public virtual SpokenLanguage SpokenLanguage { get; set; } = null!;

}
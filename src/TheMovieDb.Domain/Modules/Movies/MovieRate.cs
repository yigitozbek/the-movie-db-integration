using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class MovieRate : FullAuditedEntity<Guid>
{
    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie Movie { get; set; } = null!;


    [Range(0, 10)]
    public short Rate { get; set; }

    [MaxLength(250)]
    public string Text { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Movies;

public class MovieGenre : FullAuditedEntity<Guid>
{
    public MovieGenre(Guid movieId, Guid genreId)
    {
        MovieId = movieId;
        GenreId = genreId;
    }

    public MovieGenre(Guid genreId) => GenreId = genreId;

    public MovieGenre()
    {

    }

    public Guid MovieId { get; set; }

    [ForeignKey(nameof(MovieId))]
    public virtual Movie Movie { get; set; } = null!;


    public Guid GenreId { get; set; }

    [ForeignKey(nameof(GenreId))]
    public virtual Genre Genre { get; set; } = null!;
}
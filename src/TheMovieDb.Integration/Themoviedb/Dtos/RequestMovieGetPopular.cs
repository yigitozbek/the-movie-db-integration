using System.ComponentModel.DataAnnotations;

namespace TheMovieDb.Integration.Themoviedb.Dtos;

public class RequestMovieGetPopular : RequestBaseTheMovieDbModel
{

    public RequestMovieGetPopular(short page)
    {
        this.page = page;
    }

    [Range(1, 1000, ErrorMessage = "The field {0} must be greater than {1}.")]
    public short page { get; set; }
}
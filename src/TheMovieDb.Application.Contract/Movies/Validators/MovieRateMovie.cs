using FluentValidation;
using TheMovieDb.Domain.Shared.Movies;

namespace TheMovieDb.Application.Contract.Movies.Validators;

public class MovieRateMovie : AbstractValidator<RequestMovieRateMovie>
{
    public MovieRateMovie()
    {

    }

}
using FluentValidation;
using TheMovieDb.Domain.Shared.Movies;

namespace TheMovieDb.Application.Contract.Movies.Validators;

public class MovieRateMovieValidator : AbstractValidator<RequestMovieRateMovie>
{
    public MovieRateMovieValidator()
    {

    }

}
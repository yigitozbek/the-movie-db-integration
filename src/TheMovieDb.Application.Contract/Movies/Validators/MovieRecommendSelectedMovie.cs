using FluentValidation;
using TheMovieDb.Domain.Shared.Movies;

namespace TheMovieDb.Application.Contract.Movies.Validators;

public class MovieRecommendSelectedMovieValidator : AbstractValidator<RequestMovieRecommendSelectedMovie>
{
    public MovieRecommendSelectedMovieValidator()
    {

    }

}

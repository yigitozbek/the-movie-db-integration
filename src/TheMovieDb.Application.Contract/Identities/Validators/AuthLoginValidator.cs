using FluentValidation;
using TheMovieDb.Domain.Shared.Identities;

namespace TheMovieDb.Application.Contract.Identities.Validators;

public class AuthLoginValidator : AbstractValidator<RequestLogin>
{
    public AuthLoginValidator()
    {
        
    }

}
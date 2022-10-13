using FluentValidation;
using TheMovieDb.Domain.Shared.Identities;

namespace TheMovieDb.Application.Contract.Identities.Validators;

public class AuthChangePasswordValidator : AbstractValidator<RequestChangePasswordAsync>
{
    public AuthChangePasswordValidator()
    {
        
    }

}
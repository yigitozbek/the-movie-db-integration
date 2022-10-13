using FluentValidation;
using TheMovieDb.Application.Contract.Identities.Dtos;

namespace TheMovieDb.Application.Contract.Identities.Validators;

public class RoleUpdateValidator : AbstractValidator<RequestRoleUpdate>
{
    public RoleUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().NotNull().MinimumLength(3).MaximumLength(250);
    }

}

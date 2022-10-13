using FluentValidation;
using TheMovieDb.Application.Contract.Identities.Dtos;

namespace TheMovieDb.Application.Contract.Identities.Validators;

public class RoleDeleteValidator : AbstractValidator<RequestRoleDelete>
{
    public RoleDeleteValidator()
    {


    }

}

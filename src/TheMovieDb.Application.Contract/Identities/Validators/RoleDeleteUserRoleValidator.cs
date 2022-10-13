using FluentValidation;
using TheMovieDb.Application.Contract.Identities.Dtos;

namespace TheMovieDb.Application.Contract.Identities.Validators;

public class RoleDeleteUserRoleValidator : AbstractValidator<RequestDeleteUserRole>
{
    public RoleDeleteUserRoleValidator()
    {


    }

}

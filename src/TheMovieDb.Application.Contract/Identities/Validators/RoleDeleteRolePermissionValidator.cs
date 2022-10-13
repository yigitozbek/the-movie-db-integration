using FluentValidation;
using TheMovieDb.Application.Contract.Identities.Dtos;

namespace TheMovieDb.Application.Contract.Identities.Validators;

public class RoleDeleteRolePermissionValidator : AbstractValidator<RequestDeleteRolePermission>
{
    public RoleDeleteRolePermissionValidator()
    {


    }

}
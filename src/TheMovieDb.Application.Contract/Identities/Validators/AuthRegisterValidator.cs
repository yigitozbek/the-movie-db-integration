using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovieDb.Domain.Shared.Identities;

namespace TheMovieDb.Application.Contract.Identities.Validators;

public class AuthRegisterValidator : AbstractValidator<RequestRegister>
{
    public AuthRegisterValidator()
    {
        
    }

}

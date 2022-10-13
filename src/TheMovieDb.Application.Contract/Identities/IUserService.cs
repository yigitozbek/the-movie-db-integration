using TheMovieDb.Application.Contract.Identities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Utilities.Results;

namespace TheMovieDb.Application.Contract.Identities;

public interface IUserService
{
    Task<IDataResult<ResponseUserGetList>> GetListAsync();
}

public interface IPermissionService
{
    Task<IDataResult<List<ResponsePermissionGetList>>> GetListAsync();
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMovieDb.Application.Contract;
using TheMovieDb.Application.Contract.Identities;
using TheMovieDb.Application.Contract.Identities.Dtos;
using System.Net.Mime;
using Yella.Utilities.Results;

namespace TheMovieDb.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<IDataResult<List<ResponsePermissionGetList>>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
        public async Task<IActionResult> RateMovie()
        {
            var result = await _permissionService.GetListAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}

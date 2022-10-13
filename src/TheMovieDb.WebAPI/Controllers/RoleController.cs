using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheMovieDb.Application.Contract.Identities;
using TheMovieDb.Application.Contract.Identities.Dtos;
using System.Net.Mime;
using Yella.Utilities.Results;

namespace TheMovieDb.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<ResponseRoleGetList>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> GetList()
    {
        var result = await _roleService.GetListAsync();

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }


    [HttpPost]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RequestRoleAdd>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> Add(RequestRoleAdd input)
    {
        var result = await _roleService.AddAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }


    [HttpPut]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RequestRoleUpdate>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> Update(RequestRoleUpdate input)
    {
        var result = await _roleService.UpdateAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }



    [HttpDelete]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RequestRoleDelete>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> Delete(RequestRoleDelete input)
    {
        var result = await _roleService.DeleteAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }







    [HttpPost]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RequestRoleAddUserRole>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> AddUserRole(RequestRoleAddUserRole input)
    {
        var result = await _roleService.AddUserRoleAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }


    [HttpDelete]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RequestDeleteUserRole>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> DeleteUserRole(RequestDeleteUserRole input)
    {
        var result = await _roleService.DeleteUserRoleAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }



    [HttpPost]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RequestAddRolePermission>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> AddRolePermission(RequestAddRolePermission input)
    {
        var result = await _roleService.AddRolePermissionAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }




    [HttpDelete]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDataResult<RequestDeleteRolePermission>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResult))]
    public async Task<IActionResult> DeleteRolePermission(RequestDeleteRolePermission input)
    {
        var result = await _roleService.DeleteRolePermissionAsync(input);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

}

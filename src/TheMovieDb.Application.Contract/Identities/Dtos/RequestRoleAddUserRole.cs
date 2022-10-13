using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class RequestRoleAddUserRole : EntityDto
{
    public RequestRoleAddUserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
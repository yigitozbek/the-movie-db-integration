using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Identities;

public class IdentityPermissionRole : FullAuditedEntity<Guid>
{

    [ForeignKey(nameof(IdentityPermissionId))]
    public virtual IdentityPermission IdentityPermission { get; set; }
    public short IdentityPermissionId { get; set; }

    [ForeignKey(nameof(IdentityRoleId))]
    public virtual IdentityRole IdentityRole { get; set; }
    public Guid IdentityRoleId { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using Yella.Domain.Entities;

namespace TheMovieDb.Domain.Modules.Identities;

public class IdentityUserRole : FullAuditedEntity<Guid>
{

    public IdentityUserRole(Guid identityUserId, Guid identityRoleId)
    {
        IdentityUserId = identityUserId;
        IdentityRoleId = identityRoleId;
    }

    [ForeignKey(nameof(IdentityUserId))]
    public virtual IdentityUser IdentityUser { get; set; }
    public Guid IdentityUserId { get; set; }


    [ForeignKey(nameof(IdentityRoleId))]
    public virtual IdentityRole IdentityRole { get; set; }
    public Guid IdentityRoleId { get; set; }

}
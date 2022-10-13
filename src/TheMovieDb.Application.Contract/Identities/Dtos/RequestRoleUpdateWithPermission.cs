using Yella.Domain.Dto;

namespace TheMovieDb.Application.Contract.Identities.Dtos;

public class RequestRoleUpdateWithPermission : EntityDto<Guid>
{
    protected RequestRoleUpdateWithPermission(string name, string description, string code, PermissionModel[] permissions)
    {
        Name = name;
        Description = description;
        Code = code;
        Permissions = permissions;
    }

    protected RequestRoleUpdateWithPermission(Guid id, string name, string description, string code, PermissionModel[] permissions) : base(id)
    {
        Name = name;
        Description = description;
        Code = code;
        Permissions = permissions;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Code { get; set; }
    public PermissionModel[] Permissions { get; set; }

    public class PermissionModel : EntityDto<short>
    {
        protected PermissionModel(string name, string description, string code, string tag, bool isChecked)
        {
            Name = name;
            Description = description;
            Code = code;
            Tag = tag;
            IsChecked = isChecked;
        }

        protected PermissionModel(short id, string name, string description, string code, string tag, bool isChecked) : base(id)
        {
            Name = name;
            Description = description;
            Code = code;
            Tag = tag;
            IsChecked = isChecked;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Tag { get; set; }
        public bool IsChecked { get; set; }
    }
}
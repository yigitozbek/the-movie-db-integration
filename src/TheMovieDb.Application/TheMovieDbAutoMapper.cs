using AutoMapper;
using TheMovieDb.Application.Contract.Identities.Dtos;
using TheMovieDb.Application.Contract.Movies.Dtos;
using TheMovieDb.Domain.Modules.Identities;
using TheMovieDb.Domain.Modules.Movies;
using TheMovieDb.Domain.Shared.Movies;

namespace TheMovieDb.Application;

public class TheMovieDbAutoMapper : Profile
{
    public TheMovieDbAutoMapper()
    {
        #region Movis


        CreateMap<Domain.Modules.Movies.Movie, ResponseMovieGetList>();
        CreateMap<Domain.Modules.Movies.Movie, ResponseMovieGetById>();


        CreateMap<MovieRate, RequestMovieRateMovie>();



        #endregion

        #region Identities

        CreateMap<IdentityPermission, ResponsePermissionGetList>();


        #region Roles

        CreateMap<IdentityRole, ResponseRoleGetList>();
        CreateMap<IdentityRole, RequestRoleAdd>();
        CreateMap<IdentityRole, RequestRoleUpdate>();

        CreateMap<IdentityPermissionRole, RequestAddRolePermission>()
            .ForMember(i => i.RoleId, o => o.MapFrom(x => x.IdentityRoleId))
            .ForMember(i => i.PermissionId, o => o.MapFrom(x => x.IdentityPermissionId));

        CreateMap<IdentityPermissionRole, RequestAddRolePermission>()
            .ForMember(i => i.RoleId, o => o.MapFrom(x => x.IdentityRoleId))
            .ForMember(i => i.PermissionId, o => o.MapFrom(x => x.IdentityPermissionId)).ReverseMap();

        CreateMap<IdentityUserRole, RequestRoleAddUserRole>()
            .ForMember(i => i.UserId, o => o.MapFrom(x => x.IdentityUserId))
            .ForMember(i => i.RoleId, o => o.MapFrom(x => x.IdentityRoleId));

        CreateMap<IdentityUserRole, RequestRoleAddUserRole>()
            .ForMember(i => i.UserId, o => o.MapFrom(x => x.IdentityUserId))
            .ForMember(i => i.RoleId, o => o.MapFrom(x => x.IdentityRoleId)).ReverseMap();

        #endregion


        #endregion
    }
}
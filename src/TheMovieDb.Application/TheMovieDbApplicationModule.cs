using Autofac;
using TheMovieDb.Application.Contract.Identities;
using TheMovieDb.Application.Contract.Movies;
using TheMovieDb.Application.Identities;
using TheMovieDb.Application.Movies;
using TheMovieDb.Domain.Helpers.Security.JWT;
using TheMovieDb.Domain.Modules.Identities.Managers;
using TheMovieDb.Domain.Modules.Movies.Managers;
using TheMovieDb.Domain.Shared.Mails;
using TheMovieDb.Domain.Shared.Mails.Smtp;
using TheMovieDb.Integration.Themoviedb;
using Yella.Utilities.Security.Hashing;

namespace TheMovieDb.Application;

public class TheMovieDbApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TheMovieDbIntegrator>();
        builder.RegisterType<MovieManager>();
        builder.RegisterType<GenreManager>();
        builder.RegisterType<AuthManager>();
        builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        builder.RegisterType<MovieApplicationService>().As<IMovieService>();
        builder.RegisterType<GenreApplicationService>().As<IGenreService>();

        builder.RegisterType<AuthApplicationService>().As<IAuthService>();

        builder.RegisterType<SmtpClientApplicationService>().As<IMailService>();

        builder.RegisterType<RoleApplicationService>().As<IRoleService>();
        builder.RegisterType<PermissionApplicationService>().As<IPermissionService>();



        base.Load(builder);
    }

}
using TheMovieDb.Domain.Modules.Identities;

namespace TheMovieDb.Application;

public class TheMovieDbPermission
{
    public class Movies
    {
        public const string RateMovie = $"Service.{nameof(Movie)}.{nameof(RateMovie)}";
        public const string RecommendSelectedMovie = $"Service.{nameof(Movie)}.{nameof(RecommendSelectedMovie)}";
        public const string Get = $"Service.{nameof(Movie)}.{nameof(Get)}";
    }

    public class Identities
    {
        public class Permissions
        {
            public const string GetList = $"Service.{nameof(IdentityPermission)}.{nameof(GetList)}";
            public const string Add = $"Service.{nameof(IdentityPermission)}.{nameof(Add)}";
            public const string Delete = $"Service.{nameof(IdentityPermission)}.{nameof(Delete)}";

        }

        public class Roles
        {
            public const string Get = $"Service.{nameof(IdentityRole)}.{nameof(Get)}";
            public const string Add = $"Service.{nameof(IdentityRole)}.{nameof(Add)}";
            public const string Update = $"Service.{nameof(IdentityRole)}.{nameof(Update)}";
            public const string Delete = $"Service.{nameof(IdentityRole)}.{nameof(Delete)}";
        }

    }
}
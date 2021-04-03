namespace Broker.System.Covers.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Cover
        {
            public const string GetAll = Base + "/covers";
            public const string Get = Base + "/cover/{coverId}";
            public const string Update = Base + "/cover/{coverId}";
            public const string Create = Base + "/cover";
            public const string Delete = Base + "/cover/{coverId}";
        }

        public static class LoginComponentApi
        {
            public const string JwtTokenCookieKey = "jwt_token_key";
            public const string RefreshTokenCookieKey = "refresh_token_key";
            public const string Base = "http://localhost:5000/api/";
            public const string Register = Base + "register";
            public const string Login = Base + "login";
            public const string Refresh = Base + "refresh";
        }
    }
}
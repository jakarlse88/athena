namespace Athena.Infrastructure.Config
{
    public class SwaggerAuthorizationOptions
    {
        public static string Swagger => "Swagger";
        public static string Authorization => "Authorization";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
    }
}
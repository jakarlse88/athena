namespace Athena.Infrastructure.Config
{
    public class Auth0Options
    {
        public static string Auth0 => "Auth0";
        
        public string Authority { get; set; }
        public string ApiIdentifier { get; set; }
    }
}
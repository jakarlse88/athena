namespace Athena.Infrastructure.Config
{
    internal class RabbitMqOptions
    {
        internal const string RabbitMQ = "RabbitMQ";
        
        internal string HostName { get; set; }
        internal string User { get; set; }
        internal string Password { get; set; }
    }
}
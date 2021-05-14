namespace Dynasoft.Security.Api
{
    public class ApplicationSettings
    {
        public EventStreamOptions EventStreamOptions { get; set; }
        public AppSettingsConnectionStrings ConnectionStrings { get; set; }
        public RedisOptions RedisOptions { get; set; }
        public AuthServiceOptions AuthServiceOptions { get; set; }
        public TokenValidationOptions TokenValidationOptions { get; set; }
    }

    public class TokenValidationOptions
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }

    public class AuthServiceOptions
    {
        public string Endpoint { get; set; }
    }

    public class RedisOptions
    {
        public string Server { get; set; }
    }

    public class EventStreamOptions
    {
        public string ProducerTransactionalId { get; set; }
        public string Servers { get; set; }
        public string ConsumerGroup { get; set; }
    }

    public class AppSettingsConnectionStrings
    {
        public string Postgres { get; set; }
        public string Sqlite { get; set; }
    }
}

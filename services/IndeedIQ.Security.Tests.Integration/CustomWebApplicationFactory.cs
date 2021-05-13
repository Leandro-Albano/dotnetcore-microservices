using IndeedIQ.Common.Infrastructure.Messaging.PubSub;
using IndeedIQ.Common.Infrastructure.Messaging.PubSub.InMemory;
using IndeedIQ.Security.Infrastructure.Repositories;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        private SqliteConnection connection;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var servicesToRemove = services.Where(
                d => d.ServiceType == typeof(DbContextOptions<SecurityDataContext>)
                  || d.ServiceType == typeof(IPublisher)
                  || d.ServiceType == typeof(IConsumer)
                  || d.ServiceType == typeof(ConsumerBackgroundService)).ToList();

                servicesToRemove.ForEach(descriptor => services.Remove(descriptor));

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                this.connection = new SqliteConnection("Filename=:memory:");
                this.connection.Open();
                services.AddDbContext<SecurityDataContext>(opt => opt.UseSqlite(this.connection));

                services.AddPubSub(builder => builder.UseInMemory());
            });
        }

        protected override void Dispose(bool disposing)
        {
            this.connection.Close();
            this.connection.Dispose();
            base.Dispose(disposing);

        }
    }

    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }


}


using IndeedIQ.Common.Api.Extensions;
using IndeedIQ.Common.Infrastructure.Cache;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Common.Infrastructure.Messaging.PubSub;
using IndeedIQ.Common.Infrastructure.Messaging.PubSub.InMemory;
using IndeedIQ.Common.Infrastructure.Repositories;
using IndeedIQ.Common.Util.AutoMapper;
using IndeedIQ.Security.Api.Extensions;
using IndeedIQ.Security.Api.GrpcServices;
using IndeedIQ.Security.Domain.Entities;
using IndeedIQ.Security.Infrastructure.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NSwag;

using System.Linq;
using System.Net;

namespace IndeedIQ.Security.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationSettings appSettings = this.Configuration.Get<ApplicationSettings>();
            services.AddSingleton(appSettings);

            // This is the only service that requires set up for the authentication using the 'AddAuthentication'
            // all other services in the application should only call 'AddAuth'.
            #region Token validation
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = appSettings.TokenValidationOptions.Authority;
                options.Audience = appSettings.TokenValidationOptions.Audience;
                options.RequireHttpsMetadata = appSettings.TokenValidationOptions.RequireHttpsMetadata;
            });
            #endregion

            services.AddControllers(options => options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()))).AddDefaultJsonSerializerOptions();
            services.AddGrpc(); // Gprc services 

            services.AddAutoMapperMappings(cfg => AutoMapperHelper.AddMappings(cfg));

            services.AddApplicationDataContext<ISecurityDataContext, SecurityDataContext>(opt
                => opt.UseSqlite(appSettings.ConnectionStrings.Sqlite));

            // Commands handled by this application.
            services.AddMediator(this.GetType().Assembly);

            //services.AddPubSub(opt
            //    => opt.UseKafka(kfk
            //        => kfk.SetTransationId(appSettings.EventStreamOptions.ProducerTransactionalId)
            //              .AddServers(appSettings.EventStreamOptions.Servers)
            //              .SetConsumerGroup(appSettings.EventStreamOptions.ConsumerGroup)
            //              .Subscribe("teste")));

            services.AddPubSub(opt => opt.UseInMemory());

            services.AddRadisCache(appSettings.RedisOptions.Server);
            services.AddSwaggerDocument(options =>
            {
                options.AddSecurity("Bearer", Enumerable.Empty<string>(),
                    new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Name = nameof(HttpRequestHeader.Authorization),
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Description = "Copy this into the value field: Bearer {token}"
                    }
                );
            });
            services.AddHttpContextAccessor();

            //#if DEBUG

            //#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            //            using (var scope = services.BuildServiceProvider().CreateScope())
            //#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            //            {
            //                var context = scope.ServiceProvider.GetRequiredService<ISecurityDataContext>();
            //                ((DbContext)context).Database.EnsureDeleted();
            //                ((DbContext)context).Database.EnsureCreated();
            //                var databasePopulator = new SecurityDatabaseSeeder(context);
            //                databasePopulator.Seed();
            //            }
            //#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionMiddleware();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AuthService>();
                endpoints.MapControllers();
            });
        }
    }
}

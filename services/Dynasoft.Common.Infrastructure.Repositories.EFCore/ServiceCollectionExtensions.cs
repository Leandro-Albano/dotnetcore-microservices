using Dynasoft.Common.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace Dynasoft.Common.Infrastructure.Repositories.EFCore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationDataContext<TDomainDataContext, TImpl>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
            where TDomainDataContext : class, IDomainDataContext
            where TImpl : ApplicationDataContext, TDomainDataContext
        {
            services.AddDbContext<TImpl>(optionsAction);
            services.AddScoped<TDomainDataContext, TImpl>();
            services.AddScoped<DbContext>(ctx => ctx.GetRequiredService<TImpl>());
        }
    }
}

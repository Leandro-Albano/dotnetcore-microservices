using AutoMapper;
using AutoMapper.QueryableExtensions;

using IndeedIQ.Common.Domain.Entities.CommonQueryExtensions;
using IndeedIQ.Common.Infrastructure.Messaging.Mediator;
using IndeedIQ.Security.Application.Contracts.ApplicationResource;
using IndeedIQ.Security.Application.Contracts.DTOs;
using IndeedIQ.Security.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.ResourceAggregate;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndeedIQ.Security.Api.Handlers.ApplicationResource
{
    public class SearchApplicationResourcesApplicationCommandHandler
        : IMessageHandler<SearchApplicationResourcesApplicationCommand, IEnumerable<ApplicationResourceDto>>
    {
        private readonly ISecurityDataContext context;
        private readonly IConfigurationProvider configurationProvider;

        public SearchApplicationResourcesApplicationCommandHandler(ISecurityDataContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }

        public Task<IEnumerable<ApplicationResourceDto>> HandleAsync(SearchApplicationResourcesApplicationCommand message)
        {
            var query = this.context.ApplicationResources.AsReadOnly();

            if (message.IncludeActions)
                query = query.Include(c => c.AvailableActions);

            var result = query
                .WhereIdIn(message.Ids)
                .WhereNameIn(message.Names)
                .WhereApplicationLevelIn(message.Levels);

            IEnumerable<ApplicationResourceDto> mapped = result.ProjectTo<ApplicationResourceDto>(this.configurationProvider);
            return Task.FromResult(mapped);
        }
    }
}

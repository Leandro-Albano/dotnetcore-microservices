using IndeedIQ.Common.Domain.Entities.CommonQueryExtensions;
using IndeedIQ.Security.Domain.Contracts.Common;

using System;
using System.Collections.Generic;
using System.Linq;

namespace IndeedIQ.Security.Domain.Entities.ResourceAggregate
{
    public static class ApplicationResourceQueryableExtensions
    {
        public static IQueryable<ApplicationResource> WhereNameIn(this IQueryable<ApplicationResource> query, IEnumerable<string> names)
           => names.IsNullOrEmpty() ? query : query.Where(r => names.Contains(r.Name));

        public static IQueryable<ApplicationResource> WhereNameIs(this IQueryable<ApplicationResource> query, string name)
            => query.Where(r => r.Name == name);

        public static IQueryable<ApplicationResource> WhereApplicationLevelIn(this IQueryable<ApplicationResource> query, IEnumerable<ApplicationLevel> levels)
            => levels.IsNullOrEmpty() ? query : query.Where(r => levels.Contains(r.ApplicationLevel));

        public static IEnumerable<ResourceAction> WhereNameIs(this IEnumerable<ResourceAction> query, string name)
            => query.Where(r => r.Name == name);
    }
}

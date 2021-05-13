using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace IndeedIQ.Security.Application.Contracts.DTOs.Comparison
{
    public class ApplicationResourceDtoEqualityComparer : IEqualityComparer<Domain.Entities.ResourceAggregate.ApplicationResource>
    {
        public bool Equals([AllowNull] Domain.Entities.ResourceAggregate.ApplicationResource x, [AllowNull] Domain.Entities.ResourceAggregate.ApplicationResource y)
        {
            return !(x is null ^ y is null)
                && (x is null || this.AreEqual(x, y));
        }

        public int GetHashCode([DisallowNull] Domain.Entities.ResourceAggregate.ApplicationResource obj)
            => HashCode.Combine(obj.Id, obj.Name, obj.ApplicationLevel);

        private bool AreEqual(Domain.Entities.ResourceAggregate.ApplicationResource x, Domain.Entities.ResourceAggregate.ApplicationResource y)
        {
            return x.Id == y.Id
                && x.Name == y.Name
                && x.ApplicationLevel == y.ApplicationLevel;
        }
    }
}

using Dynasoft.Common.Domain.Entities;
using Dynasoft.Security.Domain.Contracts.Common;
using Dynasoft.Security.Domain.Entities.ResourceAggregate;

using System;

namespace Dynasoft.Security.Domain.Entities.RoleAggregate
{
    public class RoleActionPermission : Entity<RoleActionPermission>
    {
        public long ActionId { get; set; }
        public ResourceAction Action { get; set; }

        public Permission Permission { get; set; }

        public long RoleId { get; set; }
        public Role Role { get; set; }

        #region Overrides
        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.RoleId, this.ActionId);

        /// <inheritdoc/>
        public override string ToString()
            => $"{nameof(RoleActionPermission)}: {this.Action.Id}-{this.Action.Name} {nameof(this.Permission)}: {this.Permission}";
        #endregion
    }
}

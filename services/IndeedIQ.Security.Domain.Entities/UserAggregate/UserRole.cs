using IndeedIQ.Common.Domain.Entities;
using IndeedIQ.Security.Domain.Entities.RoleAggregate;

using System;
using System.Collections.Generic;

namespace IndeedIQ.Security.Domain.Entities.UserAggregate
{
    public class UserRole : Entity<UserRole>
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<long> Accounts { get; set; }
        public ICollection<long> Organisations { get; set; }

        #region Overrides
        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(this.UserId, this.RoleId);

        /// <inheritdoc/>
        public override string ToString()
            => $"{nameof(UserRole)}: {this.Id} {nameof(this.User)}: {this.User.Id} {nameof(this.Role)}: {this.Role.Name}";
        #endregion
    }
}

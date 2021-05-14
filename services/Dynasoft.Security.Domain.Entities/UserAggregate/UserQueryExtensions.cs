using Dynasoft.Common.Domain.Contracts.Exceptions;

using System.Linq;

namespace Dynasoft.Security.Domain.Entities.UserAggregate
{
    public static class UserQueryExtensions
    {

        public static User FindByIdentityServerId(this IQueryable<User> users, string identityServerId, bool throws = false)
        {
            var user = users.SingleOrDefault(u => u.IdentityServerId == identityServerId);
            return user == null && throws
                ? throw new EntityNotFoundException(nameof(User), $"{nameof(User.IdentityServerId)}: {identityServerId}")
                : user;
        }
    }
}

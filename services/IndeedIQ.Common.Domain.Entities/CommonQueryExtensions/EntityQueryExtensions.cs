using IndeedIQ.Common.Domain.Contracts.Exceptions;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IndeedIQ.Common.Domain.Entities.CommonQueryExtensions
{
    public static class EntityQueryExtensions
    {
        /// <summary>
        /// Checks if <see cref="IEnumerable"/> is null or empty.
        /// </summary>
        /// <param name="IEnumerable">The IEnumerable been checked.</param>
        /// <returns>True if the collect is null or has no elements, otherwise false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();

        public static T FindById<T>(this IEnumerable<T> items, long id, bool throws = false) where T : IEntity
        {
            var item = items.SingleOrDefault(c => c.Id == id);

            return throws && item == null
                ? throw new EntityNotFoundException(typeof(T).Name, id)
                : item;
        }

        public static IQueryable<T> WhereIdIn<T>(this IQueryable<T> query, long[] ids) where T : IEntity
            => ids.IsNullOrEmpty() ? query : query.Where(c => ids.Contains(c.Id));
    }
}

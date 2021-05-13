using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IndeedIQ.Common.Domain.Entities
{
    public interface IQueryableRepository<T> : IQueryable<T> where T : class, IAggregateRoot
    {
        /// <summary>
        /// Finds an entity by id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <param name="throws">Indicates whether to throw an <see cref="Contracts.Exceptions.EntityNotFoundException"/> when the entity was not found.</param>
        /// <returns>The entity found or null if there is no entity with the provided id.</returns>
        /// <exception cref="Contracts.Exceptions.EntityNotFoundException">When <paramref name="throws"/> is true and the entity was not found.</exception>
        Task<T> FindByIdAsync(long id, bool throws = false);

        /// <summary>
        /// Finds an entity by id allowign to specify navigations property to be included.
        /// </summary>
        /// <typeparam name="TProperty">The aggregation property type to be included.</typeparam>
        /// <param name="id">The entity's id.</param>
        /// <param name="throws">Indicates whether to throw an <see cref="Contracts.Exceptions.EntityNotFoundException"/> when the entity was not found.</param>
        /// <param name="includes">Navigaion properties to be included.</param>
        /// <returns>The entity found or null if there is no entity with the provided id.</returns>
        Task<T> FindByIdAsync<TProperty>(long id, bool throws = false, params Expression<Func<T, TProperty>>[] includes);

        IIncludableQueryableRepository<T, TProperty> Include<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> navigationPropertyPath)
            where TProperty : class, IEntity;

        IIncludableQueryableRepository<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
            where TProperty : class, IEntity;
    }
}

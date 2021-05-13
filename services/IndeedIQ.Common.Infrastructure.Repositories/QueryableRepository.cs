using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Repositories
{
    public class QueryableRepository<T> : IQueryableRepository<T> where T : class, IAggregateRoot
    {
        private readonly IQueryable<T> queryable;

        internal QueryableRepository(IQueryable<T> queryable) => this.queryable = queryable;

        /// <inheritdoc />
        public Type ElementType => this.queryable.ElementType;

        /// <inheritdoc />
        public Expression Expression => this.queryable.Expression;

        /// <inheritdoc />
        public IQueryProvider Provider => this.queryable.Provider;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => this.queryable.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.queryable.GetEnumerator();

        /// <inheritdoc />
        public virtual Task<T> FindByIdAsync(long id, bool throws = false) => this.FindByIdAsync<object>(id, throws);

        /// <inheritdoc />
        public Task<T> FindByIdAsync<TProperty>(long id, bool throws = false, params Expression<Func<T, TProperty>>[] includes)
        {
            IQueryable<T> query = this.queryable;
            foreach (var include in includes)
                query = query.Include(include);

            var entity = query.SingleOrDefaultAsync(c => c.Id == id);
            return throws && entity == null
               ? throw new EntityNotFoundException(typeof(T).Name, id)
               : entity;
        }

        /// <inheritdoc />
        public IIncludableQueryableRepository<T, TProperty> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
            where TProperty : class, IEntity
            => new IncludableQueryableRepository<T, TProperty>(this.queryable.Include(navigationPropertyPath));

        public IIncludableQueryableRepository<T, TProperty> Include<TProperty>(Expression<Func<T, IEnumerable<TProperty>>> navigationPropertyPath)
            where TProperty : class, IEntity
            => new IncludableCollectionQueryableRepository<T, TProperty>(this.queryable.Include(navigationPropertyPath));
    }
}

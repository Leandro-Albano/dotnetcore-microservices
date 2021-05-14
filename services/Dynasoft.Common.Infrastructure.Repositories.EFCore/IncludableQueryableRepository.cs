using Dynasoft.Common.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dynasoft.Common.Infrastructure.Repositories.EFCore
{
    public class IncludableQueryableRepository<T, TProperty> : QueryableRepository<T>, IIncludableQueryableRepository<T, TProperty>
        where T : class, IAggregateRoot
    {
        private readonly IIncludableQueryable<T, TProperty> includableQueryable;

        public IncludableQueryableRepository(IIncludableQueryable<T, TProperty> includableQueryable) : base(includableQueryable)
            => this.includableQueryable = includableQueryable;

        /// <inheritdoc/>
        public IIncludableQueryableRepository<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, TNextProperty>> navigationPropertyPath)
            where TNextProperty : class, IEntity
            => new IncludableQueryableRepository<T, TNextProperty>(this.includableQueryable.ThenInclude(navigationPropertyPath));

        public IIncludableQueryableRepository<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, IEnumerable<TNextProperty>>> navigationPropertyPath)
            where TNextProperty : class, IEntity
            => new IncludableCollectionQueryableRepository<T, TNextProperty>(this.includableQueryable.ThenInclude(navigationPropertyPath));
    }

    public class IncludableCollectionQueryableRepository<T, TProperty> : QueryableRepository<T>, IIncludableQueryableRepository<T, TProperty>
        where T : class, IAggregateRoot
    {
        private readonly IIncludableQueryable<T, IEnumerable<TProperty>> includableQueryable;

        public IncludableCollectionQueryableRepository(IIncludableQueryable<T, IEnumerable<TProperty>> includableQueryable) : base(includableQueryable)
            => this.includableQueryable = includableQueryable;

        /// <inheritdoc/>
        public IIncludableQueryableRepository<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, TNextProperty>> navigationPropertyPath)
            where TNextProperty : class, IEntity
            => new IncludableQueryableRepository<T, TNextProperty>(this.includableQueryable.ThenInclude(navigationPropertyPath));

        public IIncludableQueryableRepository<T, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, IEnumerable<TNextProperty>>> navigationPropertyPath)
            where TNextProperty : class, IEntity
            => new IncludableCollectionQueryableRepository<T, TNextProperty>(this.includableQueryable.ThenInclude(navigationPropertyPath));
    }
}

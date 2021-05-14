
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dynasoft.Common.Domain.Entities
{
    public interface IIncludableQueryableRepository<TEntity, TProperty>
        : IQueryableRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        IIncludableQueryableRepository<TEntity, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, TNextProperty>> navigationPropertyPath)
            where TNextProperty : class, IEntity;

        IIncludableQueryableRepository<TEntity, TNextProperty> ThenInclude<TNextProperty>(Expression<Func<TProperty, IEnumerable<TNextProperty>>> navigationPropertyPath)
            where TNextProperty : class, IEntity;

    }

}

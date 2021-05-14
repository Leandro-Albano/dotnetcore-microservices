using System.Linq;
using System.Threading.Tasks;

namespace Dynasoft.Common.Domain.Entities
{
    /// <summary>
    /// Represents a data repository that interacts with de external storage to get and persist data.
    /// </summary>
    /// <typeparam name="T">The entity type managed by this repository. The entity must be an <see cref="IAggregateRoot"/>.</typeparam>
    public interface IRepository<T> : IQueryableRepository<T> where T : class, IAggregateRoot
    {
        /// <summary>
        /// Adds a new entity to the repository so it's persisted when <see cref="IDomainDataContext.PersistChangesAsync"/> is called.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <remarks>
        /// It's important to make sure you don't add an entity that is already tracked.
        /// </remarks>
        void Add(T entity);

        /// <summary>
        /// Adds a new entity to the repository and immediately call <see cref="IDomainDataContext.PersistChangesAsync"/>.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <remarks>
        /// It's important to make sure you don't add an entity that is already tracked.
        /// </remarks>
        Task AddAndPersistAsync(T entity);

        /// <summary>
        /// Sets an entity to be deleted or archived when <see cref="IDomainDataContext.PersistChangesAsync"/> is called.
        /// </summary>
        /// <param name="entity">The entity to be deleted/archived.</param>
        /// <remarks>
        /// The entity must implement <see cref="IDeletableEntity"/> or <see cref="IArchivableEntity"/>.
        /// The Entity must be already persited to the database and have an id.
        /// </remarks>
        void Delete(T entity);

        /// <summary>
        /// Sets an entity to be deleted or archived and immediately call <see cref="IDomainDataContext.PersistChangesAsync"/>.
        /// </summary>
        /// <param name="entity">The entity to be deleted/archived.</param>
        /// <remarks>
        /// The entity must implement <see cref="IDeletableEntity"/> or <see cref="IArchivableEntity"/>.
        /// The Entity must be already persited to the database and have an id.
        /// </remarks>
        Task DeleteAndPersistAsync(T entity);

        /// <summary>
        /// Returns a read only <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <remarks>
        /// Entities queried throw a this <see cref="IQueryable{T}"/> are not tracked which makes the queries faster, but changes to those entities cannot be persisted.
        /// </remarks>
        /// <returns>An <see cref="IQueryable{T}"/> to query the entities on this repository.</returns>
        IQueryableRepository<T> AsReadOnly();

    }
}

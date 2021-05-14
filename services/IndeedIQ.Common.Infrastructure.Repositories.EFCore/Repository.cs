using IndeedIQ.Common.Domain.Contracts.Exceptions;
using IndeedIQ.Common.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace IndeedIQ.Common.Infrastructure.Repositories.EFCore
{
    public class Repository<T> : QueryableRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        private readonly DbSet<T> set;
        public Repository(ApplicationDataContext context) : base(context.Set<T>())
        {
            this.Context = context;
            this.set = this.Context.Set<T>();
        }

        public ApplicationDataContext Context { get; }


        #region Mutation Methods
        /// <inheritdoc/>
        public void Add(T entity) => this.set.Add(entity);

        /// <inheritdoc/>
        public async Task AddAndPersistAsync(T entity)
        {
            this.Add(entity);
            await this.Context.PersistChangesAsync();
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            if (entity is IArchivableEntity archivable)
                archivable.Archive();
            else if (entity is IDeletableEntity deletable)
            {
                deletable.Delete();
                this.set.Remove(entity);
            }
            else
                throw new NonDeletableOrArchivableEntityException(typeof(T).Name);
        }

        /// <inheritdoc/>
        public async Task DeleteAndPersistAsync(T entity)
        {
            this.Delete(entity);
            await this.Context.PersistChangesAsync();
        }

        #endregion

        /// <inheritdoc/>
        public IQueryableRepository<T> AsReadOnly()
            => new QueryableRepository<T>(this.set.AsNoTracking());
    }
}

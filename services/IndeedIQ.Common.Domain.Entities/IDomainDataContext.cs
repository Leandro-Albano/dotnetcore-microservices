using System.Threading.Tasks;

namespace IndeedIQ.Common.Domain.Entities
{
    public interface IDomainDataContext
    {
        /// <summary>
        /// Persist changes to the storage.
        /// </summary>
        Task PersistChangesAsync();
    }
}

using System.Threading.Tasks;

namespace Dynasoft.Common.Domain.Entities
{
    public interface IDomainDataContext
    {
        /// <summary>
        /// Persist changes to the storage.
        /// </summary>
        Task PersistChangesAsync();
    }
}

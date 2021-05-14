namespace Dynasoft.Common.Domain.Entities
{
    /// <summary>
    /// Represents an entity with the ability to be removed for good from it's repository.
    /// </summary>
    /// <remarks>
    /// A deletable entity must not ofer an undo action once persisted. If the ability to restore the entity state is required, <see cref="IArchivableEntity"/> should be implemented instead.
    /// </remarks>
    public interface IDeletableEntity : IEntity
    {
        /// <summary>
        /// Perform required actions to definetily exclude the entity.
        /// </summary>
        void Delete();
    }
}
